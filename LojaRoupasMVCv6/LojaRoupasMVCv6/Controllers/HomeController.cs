using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LojaRoupasMVCv6.Models.POJO;
using LojaRoupasMVCv6.Models.DAO;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using PagedList;

namespace LojaRoupasMVCv6.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Faça login para ter acesso ao sistema";

            return View();
        }

        public ActionResult painel()
        {

            if (Session["idVendedor"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }


        public ActionResult Clientes(int? page)
        {
            if (Session["idVendedor"] != null)
            {

                int pageSize = 10;
                int pageIndex = 1;
                pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

                ClientesDAO objCli = new ClientesDAO();
                IPagedList<Clientes> CliPage = null;

                CliPage = objCli.orderCli(pageIndex, pageSize);
                return View(CliPage);

                /*List<Clientes> Retorno = new List<Clientes>();
                ClientesDAO OV = new ClientesDAO();
                Retorno = OV.MostrarCliente();


                return View(Retorno);*/
            }
            else
            {
                return RedirectToAction("Index");
            }
           
        }


        [HttpPost]
        public ActionResult Clientes(Clientes c, int? page)
        {
            if (Session["idVendedor"] != null)
            {
                string buscar = c.nomeCliente;

                int pageSize = 10;
                int pageIndex = 1;
                pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

                IPagedList<Clientes> CliPage = null;
                ClientesDAO objCli = new ClientesDAO();


                //VendPage = objVen.BuscarVendedores(buscar);
                // orderVend(pageIndex, pageSize);

                CliPage = objCli.orderCliBuscar(buscar, pageIndex, pageSize);

                return View(CliPage);

            }
            else
            {
                return RedirectToAction("Index");
            }
        }




        public ActionResult Vendedores(int? page)
        {
            if (Session["idVendedor"] != null)
            {


                int pageSize = 10;
                int pageIndex = 1;
                pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

                VendedoresDAO objVen = new VendedoresDAO();
                IPagedList<Vendedores> VendPage = null;

                VendPage = objVen.orderVend(pageIndex, pageSize);
                return View(VendPage);

                /*List<Vendedores> Retorno = new List<Vendedores>();
                VendedoresDAO OV = new VendedoresDAO();
                Retorno = OV.listarVendedores();


                return View(Retorno);*/
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public ActionResult Vendedores(Vendedores v, int? page)
        {
            if (Session["idVendedor"] != null)
            {
                string buscar = v.nomeVendedor;

                int pageSize = 10;
                int pageIndex = 1;
                pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

                IPagedList<Vendedores> VendPage = null;
                VendedoresDAO objVen = new VendedoresDAO();


                //VendPage = objVen.BuscarVendedores(buscar);
                // orderVend(pageIndex, pageSize);

                VendPage = objVen.orderVendBuscar(buscar, pageIndex, pageSize);

                return View(VendPage);

            }
            else
            {
                return RedirectToAction("Index");
            }
        }




        public ActionResult vendas()
        {

            if (Session["idVendedor"] != null)
            { 
                PedidosDAO OV = new PedidosDAO();
                string somar = OV.somarPedido();
                if (somar == "0")
                {
                    string retorno = "Favor adicionar pelo menos um produto para concluir a venda.";
                    ViewBag.retorno2 = retorno;
                    return RedirectToAction("pedidos");
                }

                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index");
            }

        }


        public ActionResult vendaaprazo()
        {
            if (Session["idVendedor"] != null)
            {

                PedidosDAO OV = new PedidosDAO();
                string somar = OV.somarPedido();
                ViewBag.total = somar;

                ClientesDAO objCl = new ClientesDAO();
                string idCliente = Request["idCliente"].ToString();
                string nomeCliente = objCl.selecionanomeCliente(idCliente);
                ViewBag.nomeCliente = nomeCliente;
                

                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public ActionResult realizarVendaaPrazo()
        {

            if (Session["idVendedor"] != null)
            {
            VendasDAO objVDAO = new VendasDAO();
            VendaaPrazoDAO objVpDAO = new VendaaPrazoDAO();
            PedidosDAO objPe = new PedidosDAO();
            ProdutosDAO objPr = new ProdutosDAO();
            ParcelasDAO objPaD = new ParcelasDAO();
            Parcelas objPa = new Parcelas();
            Vendas objV = new Vendas();
            ClientesDAO objCl = new ClientesDAO();


            List<Pedidos> objP = new List<Pedidos>();
            objP = objPe.MostrarPedidos();
            for (int i = 0; i < objP.Count; i++)
            {
                string idProduto;
                int qtdProduto;
                idProduto = objP[i].idProduto;
                qtdProduto = objP[i].qtdProduto;
                objPr.atualizaEstoque(qtdProduto, idProduto);
            }



            //pegando do form o idCliente
            string idCliente = Request.Form["idCliente"];
            objV.idCliente = Int32.Parse(idCliente);

            //pegando o nome do cliente
            TempData["nomeCliente"] = objCl.selecionanomeCliente(Int32.Parse(idCliente));
            

            int parcelas = Convert.ToInt32(Request.Form["parcelas"]);
          

            //realiza a venda (a prazo) retorna o idVenda
            string retorno = objVDAO.vendaAprazo(objV);

            //realiza a vendaaprazo e retorna o idVendaaprazo
            int idVendaaprazo = Int32.Parse(objVpDAO.vendaaprazo(retorno,parcelas));


            //método que atualiza o pedido com o idVenda e o status do pedido
            objPe.atualizaStatus(retorno);

            DateTime today = DateTime.Now;
            
            
            //pegar o valorTotal da venda
            decimal total = objVDAO.pegartotalVenda(retorno);
            decimal valorParcela = total / parcelas;
            TempData["parcelas"] = parcelas;
            TempData["valorParcela"] = valorParcela;

            //método que faz as parcelas da venda a prazo
            for (int i = 0; i < parcelas; i++)
            {

                DateTime dtvenc = today.AddDays((30*(i+1)));
                string dtvencParcela = dtvenc.ToString("dd/MM/yyyy");

                objPa.valorParcela = valorParcela;
                objPa.idVendaaPrazo = idVendaaprazo;
                objPa.numParcela = i+1;
                objPa.statusParcela = 0;
                objPa.dtvencParcela = dtvencParcela;
                objPaD.criarParcela(objPa);

            }


            ViewBag.retorno = retorno;

            return RedirectToAction("sucessoap/" + retorno + "");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }




        public ActionResult Relatorios()
        {

            if (Session["idVendedor"] != null)
            {
             
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }

        }


        public ActionResult RelatorioDiario()
        {

            if (Session["idVendedor"] != null)
            {

                List<Vendas> Retorno = new List<Vendas>();
                VendasDAO OV = new VendasDAO();
                Retorno = OV.VendasDiarias();

                //Response.Write("ss" + Session["id"]);

                return View(Retorno);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        public ActionResult ERRelatorioDiario()
        {
            if (Session["idVendedor"] != null)
            {
            string data = DateTime.Now.ToString("dd-MM-yyyy");
            List<Vendas> Retorno = new List<Vendas>();
            VendasDAO OV = new VendasDAO();
            Retorno = OV.VendasDiarias();

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "RelatorioDiario.rpt"));
            rd.SetDataSource(Retorno);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "JeR_Relatorio"+data+".pdf");
            }
            catch (Exception e)
            {
                throw;
            }

            }
            else
            {
                return RedirectToAction("Index");
            }


        }



        public ActionResult ERRelatorioDiarioAPrazo()
        {
            if (Session["idVendedor"] != null)
            {
                string data = DateTime.Now.ToString("dd-MM-yyyy");
                List<Vendas> Retorno = new List<Vendas>();
                VendasDAO OV = new VendasDAO();
                Retorno = OV.VendasDiariasAPrazo();

                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/Reports"), "RelatorioDiarioAPrazo.rpt"));
                rd.SetDataSource(Retorno);

                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                try
                {
                    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/pdf", "JeR_RelatorioAPrazo_" + data + ".pdf");
                }
                catch (Exception e)
                {
                    throw;
                }

            }
            else
            {
                return RedirectToAction("Index");
            }


        }




        public ActionResult ERRelatorioDiarioAVista()
        {
            if (Session["idVendedor"] != null)
            {
                string data = DateTime.Now.ToString("dd-MM-yyyy");
                List<Vendas> Retorno = new List<Vendas>();
                VendasDAO OV = new VendasDAO();
                Retorno = OV.VendasDiariasAVista();

                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/Reports"), "RelatorioDiarioAVista.rpt"));
                rd.SetDataSource(Retorno);

                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                try
                {
                    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/pdf", "JeR_RelatorioAVista_" + data + ".pdf");
                }
                catch (Exception e)
                {
                    throw;
                }

            }
            else
            {
                return RedirectToAction("Index");
            }


        }



        public ActionResult ERRelatorioVencimentos()
        {
            if (Session["idVendedor"] != null)
            {
                string data = DateTime.Now.ToString("dd-MM-yyyy");
                List<Parcelas> Retorno = new List<Parcelas>();
                ParcelasDAO OP = new ParcelasDAO();
                Retorno = OP.Vencimentos();

                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/Reports"), "RelatorioVencimentos.rpt"));
                rd.SetDataSource(Retorno);

                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                try
                {
                    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/pdf", "JeR_RelatorioVencimentos_" + data + ".pdf");
                }
                catch (Exception e)
                {
                    throw;
                }

            }
            else
            {
                return RedirectToAction("Index");
            }


        }



        [HttpGet]
        public ActionResult Recebimentos()
        {

            if (Session["idVendedor"] != null)
            {
                List<Clientes> Retorno = new List<Clientes>();
                ClientesDAO OV = new ClientesDAO();
                Retorno = OV.MostrarCliente();

                //Response.Write("ss" + Session["id"]);

                return View(Retorno);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public ActionResult Recebimentos(Clientes c)
        {
            if (Session["idVendedor"] != null)
            {
            string user = c.nomeCliente;

            List<Clientes> listaClientes = new List<Clientes>();
            ClientesDAO objCl = new ClientesDAO();
            listaClientes = objCl.BuscarCliente(user);

            return View(listaClientes);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        
        public ActionResult verDebitos(string id)
        {
            if (Session["idVendedor"] != null)
            {
            ClientesDAO objClDAO = new ClientesDAO();
            string nomeCliente = objClDAO.selecionanomeCliente(id);

            Session["Msg"] = nomeCliente;

            VendasDAO objVeDao = new VendasDAO();
            VendaaPrazoDAO objVPD = new VendaaPrazoDAO();
            List<Vendas> listaVendas = new List<Vendas>();
            listaVendas = objVeDao.listarVendasaprazo(id);

                //Lista apenas as vendas com parcelas em aberto
            List<Vendas> listaVendasAbertas = new List<Vendas>();

            for (int i = 0; i < listaVendas.Count; i++)
            {
                string idvendaaprazo = objVPD.pegaridVendaaprazo2(listaVendas[i].idVenda);
                if(objVPD.verificarParcelas(idvendaaprazo) == 0)
                {
                     listaVendasAbertas.Add(listaVendas[i]);
                }
            }

            return View(listaVendasAbertas);
             }
            else
            {
                return RedirectToAction("Index");
            }
        }


        public ActionResult verParcelas(string id)
        {

             if (Session["idVendedor"] != null)
            {
            /*ClientesDAO objClDAO = new ClientesDAO();
            string nomeCliente = objClDAO.selecionanomeCliente(id);

            TempData["Msg"] = nomeCliente;*/


            VendaaPrazoDAO objVAPDAO = new VendaaPrazoDAO();
            string idVendaaPrazo = objVAPDAO.pegaridVendaaprazo(id);

            ParcelasDAO objPaDAO = new ParcelasDAO();
            List<Parcelas> listaParcelas = new List<Parcelas>();
            listaParcelas = objPaDAO.listarParcelas(idVendaaPrazo);

            return View(listaParcelas);
            }
             else
             {
                 return RedirectToAction("Index");
             }
        }


        public ActionResult Details(string id)
        {
            if (Session["idVendedor"] != null)
            {
            VendasDAO objVD = new VendasDAO();
            Vendas objV = objVD.GetVendas(id);
            return View(objV);
             }
            else
            {
                return RedirectToAction("Index");
            }
        }




        public ActionResult cancelarPedido(Pedidos p)
        {

            if (Session["idVendedor"] != null)
            {
                PedidosDAO objPe = new PedidosDAO();
                if (objPe.cancelarPedido(p) == "1")
                {
                    return View("painel");
                }
                else
                {
                    return View("painel");
                }

            }
            else
            {
                return RedirectToAction("Login");
            }




        }





        public ActionResult deletarPedido(int id)
        {

            if (Session["idVendedor"] != null)
            {
                PedidosDAO objPe = new PedidosDAO();
                if (objPe.deletarProduto(id) == "1")
                {
                    return RedirectToAction("pedidos", "Home");
                }
                else
                {
                    return View("painel");
                }

            }
            else
            {
                return RedirectToAction("Login");
            }




        }




      


        





        //mostra todos os clientes
        [HttpGet]
        public ActionResult selecionarCliente()
        {
            if (Session["idVendedor"] != null)
            {
            List<Clientes> Retorno = new List<Clientes>();
            ClientesDAO OV = new ClientesDAO();
            Retorno = OV.MostrarCliente();

            //Response.Write("ss" + Session["id"]);

            return View(Retorno);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // procura os clientes
        [HttpPost]
        public ActionResult selecionarCliente(Clientes c)
        {
            if (Session["idVendedor"] != null)
            {
            Response.Write(" <br> Nome " + c.nomeCliente);
            string user = c.nomeCliente;

            List<Clientes> listaClientes = new List<Clientes>();
            ClientesDAO objCl = new ClientesDAO();
            listaClientes = objCl.BuscarCliente(user);

            return View(listaClientes);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }


        
       
        //exibe produtos
        [HttpGet]
        public ActionResult Produtos(int? page)
        {
            if (Session["idVendedor"] != null)
            {

                int pageSize = 10;
                int pageIndex = 1;
                pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

                ProdutosDAO objProd = new ProdutosDAO();
                IPagedList<Produtos> ProdPage = null;

                ProdPage = objProd.orderProd(pageIndex, pageSize);
                return View(ProdPage);

                /*List<Produtos> Retorno = new List<Produtos>();
                ProdutosDAO OV = new ProdutosDAO();
                Retorno = OV.MostrarProdutos();

                return View(Retorno);*/
            }
            else
            {
                return RedirectToAction("Login");
            }
        }


        //procurar os produtos
        [HttpPost]
        public ActionResult Produtos(Produtos c, int? page)
        {
             if (Session["idVendedor"] != null)
            {

            /*string descricaoProduto = c.descricaoProduto;

            List<Produtos> listaProdutos = new List<Produtos>();
            ProdutosDAO objPr = new ProdutosDAO();
            listaProdutos = objPr.BuscarProduto(descricaoProduto);

            return View(listaProdutos);*/

                string buscar = c.descricaoProduto;

                int pageSize = 10;
                int pageIndex = 1;
                pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

                IPagedList<Produtos> ProdPage = null;
                ProdutosDAO objProd = new ProdutosDAO();


                //VendPage = objVen.BuscarVendedores(buscar);
                // orderVend(pageIndex, pageSize);

                ProdPage = objProd.orderProdBuscar(buscar, pageIndex, pageSize);

                return View(ProdPage);

            }
             else
             {
                 return RedirectToAction("Index");
             }
        }


        public ActionResult pagarparcela(int id)
        {
            if (Session["idVendedor"] != null)
            {
                ParcelasDAO objPD = new ParcelasDAO();
                PagamentoDAO objPgD = new PagamentoDAO();
                Parcelas objP = new Parcelas();
                VendaaPrazoDAO objVPD = new VendaaPrazoDAO();

                //retorna a idPagamento
               int idPagamento = objPgD.realizaPagamento(id);
               objP.idPagamento = idPagamento;
               objP.idParcela = id;
               objPD.pagarParcela(objP);

               TempData["idPag"] = idPagamento;
              // string retorno = Request["idPagamento"].ToString();
               //ViewBag.retorno = idPagamento.ToString(); 


                //atualizar as parcelas restantes da vendaaprazo 
               int idvendaaprazo = objPD.selecionaidVendaaPrazo(id);
               objVPD.atualizaVendaaprazo(idvendaaprazo);

               return RedirectToAction("sucessopg/"+ idPagamento+"");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        //se o pagamento for bem sucedido
        public ActionResult sucessopg(int id)
        {
            if (Session["idVendedor"] != null)
            {
            PagamentoDAO objPD = new PagamentoDAO();
            Pagamento objP = objPD.GetPagamento(id);

            ParcelasDAO objPrD = new ParcelasDAO();
            Parcelas objPr = objPrD.GetParcelas(id);

            return View(Tuple.Create(objPr, objP));
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        /*public ActionResult Comprovantepg(int id)
        {
            
            Pagamento Retorno = new Pagamento();
            PagamentoDAO OP = new PagamentoDAO();
            Retorno = OP.dadosPagamento(id);

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "Comprovantepg.rpt"));
            rd.SetDataSource(Retorno);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Comprovantepg" + id + ".pdf");
            }
            catch (Exception e)
            {
                throw;
            }

        }*/





        public ActionResult vendaavista()
        {
            if (Session["idVendedor"] != null)
            {

                PedidosDAO OV = new PedidosDAO();
                string somar = OV.somarPedido();
                ViewBag.total = somar;


                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public ActionResult realizarVendaaVista()
        {
            if (Session["idVendedor"] != null)
            {
            VendasDAO objVDAO = new VendasDAO();
            VendaaVistaDAO objVvDAO = new VendaaVistaDAO();
            PedidosDAO objPe = new PedidosDAO();
            ProdutosDAO objPr = new ProdutosDAO();

            Vendas objV = new Vendas();


            List<Pedidos> objP = new List<Pedidos>();
            objP = objPe.MostrarPedidos();
            for (int i = 0; i < objP.Count; i++)
            {
                string idProduto;
                int qtdProduto;
                idProduto = objP[i].idProduto;
                qtdProduto = objP[i].qtdProduto;
                objPr.atualizaEstoque(qtdProduto, idProduto);
            }


            //método que salva na tabela vendas e retorna o idVenda
            string retorno = objVDAO.vendaAvista(objV); //retorno = idVenda
            ViewBag.retorno = retorno; //manda para a view
            string formapagVenda = Request.Form["formapagVenda"]; //pega a forma de pagamento do form

            
            //método que salva na tabela vendaavista e retorna o idVendaavista
            string idVendaavista = objVvDAO.vendaavista(retorno, formapagVenda);

            
           
            //método que atualiza o status dos pedidos com o idVenda e o status
            objPe.atualizaStatus(retorno);
            switch(Request.Form["formapagVenda"])
            {
                case "dinheiro":
                    TempData["formapag"] = "Dinheiro";
                    break;
                case "mastercredito":
                    TempData["formapag"] = "Cartão de Crédito MasterCard";
                    break;
                case "maestrodebito":
                    TempData["formapag"] = "Cartão de Débito MasterCard";
                    break;
                case "visacredito":
                    TempData["formapag"] = "Cartão de Crédito Visa";
                    break;
                case "visadebito":
                    TempData["formapag"] = "Cartão de Débito Visa";
                    break;
            }
           
            



            return RedirectToAction("sucesso/" + retorno + "");

            }
            else
            {
                return RedirectToAction("Index");
            }
        }





          //se a venda for bem sucedida
        public ActionResult sucessoap(string id)
        {
            if (Session["idVendedor"] != null)
            {
            PedidosDAO objPD = new PedidosDAO();
            List<Pedidos> objP = new List<Pedidos>();
            objP = objPD.GetPedidos(id);

            VendasDAO objVD = new VendasDAO();
            Vendas objV = objVD.GetVendas(id);

            // var tuple = new Tuple<Vendas, List<Pedidos>> (objV, objP);

            return View(Tuple.Create(objV, objP));

            }
            else
            {
                return RedirectToAction("Index");
            }
        }
       
        



        //se a venda a vista for bem sucedida
        public ActionResult sucesso(string id)
        {
             if (Session["idVendedor"] != null)
            {
            PedidosDAO objPD = new PedidosDAO();
            List<Pedidos> objP = new List<Pedidos>();
                objP = objPD.GetPedidos(id);

            VendasDAO objVD = new VendasDAO();
            Vendas objV = objVD.GetVendas(id);

           // var tuple = new Tuple<Vendas, List<Pedidos>> (objV, objP);

            return View(Tuple.Create(objV,objP));
            }
             else
             {
                 return RedirectToAction("Index");
             }

        }


        //exibe a pagina pedidos apenas com o id e nome do cliente
        [HttpGet]
        public ActionResult pedidos()
        {

         
            ViewBag.Status = "g";

            if (Session["idVendedor"] != null)
            {


                List<Pedidos> Retorno = new List<Pedidos>();
                PedidosDAO OV = new PedidosDAO();
                
                Retorno = OV.MostrarPedidos();

                string somar = OV.somarPedido();
                ViewBag.total = somar;

                return View(Retorno);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //adiciona os produto
        [HttpPost]
        public ActionResult pedidos(Pedidos p)
        {

            if (Session["idVendedor"] != null)
            {

            ViewBag.Status = "p";

            ProdutosDAO ObjPro = new ProdutosDAO();
            Produtos objpro = new Produtos();
            objpro.idProduto = p.idProduto;
            if (ObjPro.verificaProduto(objpro) == "1")
            {




                PedidosDAO objPr = new PedidosDAO();
                string idsessao = "";
                if (!(string.IsNullOrEmpty(Convert.ToString(Session["idVendedor"]))))
                {
                    idsessao = Session["idVendedor"].ToString();
                }

                p.idVendedor = Int32.Parse(idsessao);


                if (objPr.adicionarPedido(p) == "1")
                {
                    string retorno = "Produto adicionado";
                    ViewBag.retorno2 = retorno;
                }
                else
                {
                    string retorno = "Erro em adicionar produto";
                    ViewBag.retorno2 = retorno;
                }
                



                List<Pedidos> objP = new List<Pedidos>();
                objP = objPr.MostrarPedidos();

                string somar = objPr.somarPedido();
                ViewBag.total = somar;

                /*inicio soma preço total
                decimal somar = 0;
                decimal totalItens = 0;
                for (int i = 0; i < objP.Count; i++)
                {
                    totalItens = objP[i].qtdProduto * objP[i].produtos_precoProduto;
                    somar = totalItens + somar;
                }
                ViewBag.total = somar;
                Session["totalVenda"] = somar;*/
                //fim soma preço total
                

                return View(objP);

            }
            else
            {
                PedidosDAO objPr = new PedidosDAO();
                List<Pedidos> objP = new List<Pedidos>();
                objP = objPr.MostrarPedidos();

                /*inicio soma preço total
                decimal somar = 0;
                decimal totalItens = 0;
                for (int i = 0; i < objP.Count; i++)
                {
                    totalItens = objP[i].qtdProduto * objP[i].produtos_precoProduto;
                    somar = totalItens + somar;
                }
                ViewBag.total = somar;*/
                //fim soma preço total

                ViewBag.retorno = ObjPro.verificaProduto(objpro);

                return View(objP);
            }


            }
            else
            {
                return RedirectToAction("Index");
            }


        }



        //exibe a view de login
        public ActionResult Login()
        {

            //session start
            if (Session["idVendedor"] != null)
            {
                return RedirectToAction("painel");
            }
            else
            {
                string sessao = Session.SessionID;
                return View();
            }

            
        }


        public ActionResult Logout()
        {
            string sessao = Session.SessionID;
            Session.Abandon();
            
            return RedirectToAction("Login");
        }



        [HttpPost]
        public ActionResult Login(Vendedores v)
        {
            VendedoresDAO objVe = new VendedoresDAO();

            if (objVe.verificaVendedor(v) == "1")
            {
                Session["idVendedor"] = v.idVendedor.ToString();
                Session["nomeVendedor"] = objVe.selecionanomeVendedor(v);
                
                return View("painel");
            }

            else
            {
                ViewBag.retorno = objVe.verificaVendedor(v);
               // Response.Write("Login ou Senha incorretos");
                return View();
            }
        }
       


    }
}
