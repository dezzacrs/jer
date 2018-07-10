using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LojaRoupasMVCv6.Models.POJO;
using LojaRoupasMVCv6.Models.DAO;
using PagedList;

namespace LojaRoupasMVCv6.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            if (Session["idAdmin"] != null)
            {
                return RedirectToAction("painel");
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }



        //exibe a view de login
        public ActionResult Login()
        {

            //session start
            if (Session["idAdmin"] != null)
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

            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public ActionResult Login(Admin a)
        {
            AdminDAO objAdm = new AdminDAO();

            if (objAdm.verificaAdmin(a) == "1")
            {
                Session["idAdmin"] = a.idAdmin.ToString();

                return View("painel");
            }

            else
            {
                ViewBag.retorno = objAdm.verificaAdmin(a);
                // Response.Write("Login ou Senha incorretos");
                return View();
            }
        }


        


        public ActionResult AdminGrid()
        {
            if (Session["idAdmin"] != null)
            {
                

                AdminDAO objAdm = new AdminDAO();
                List<Admin> ListAdm = new List<Admin>();
                ListAdm = objAdm.AdminList();
                return View(ListAdm);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }


        public ActionResult painel()
        {
            if (Session["idAdmin"] != null)
            {
            return View();
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }




        // VENDEDORES

        public ActionResult VendedoresGrid(int? page)
        {
            if (Session["idAdmin"] != null)
            {
                int pageSize = 10;
                int pageIndex = 1;
                pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

                VendedoresDAO objVen = new VendedoresDAO();    
                IPagedList<Vendedores> VendPage = null;

                VendPage = objVen.orderVend(pageIndex, pageSize);
                return View(VendPage);
            
            /*List<Vendedores> ListVen = new List<Vendedores>();
            ListVen = objVen.listarVendedores();
            return View(VendPage);*/
            
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }



        [HttpPost]
        public ActionResult VendedoresGrid(Vendedores v, int? page)
        {
            if (Session["idAdmin"] != null)
            {
                string buscar = v.nomeVendedor;

                int pageSize = 10;
                int pageIndex = 1;
                pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

                IPagedList<Vendedores> VendPage = null;
                VendedoresDAO objVen = new VendedoresDAO();

                
                //VendPage = objVen.BuscarVendedores(buscar);
               // orderVend(pageIndex, pageSize);

                VendPage = objVen.orderVendBuscar(buscar,pageIndex, pageSize);

                return View(VendPage);
                                    

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }




        public ActionResult VendedoresInsert()
        {
            if (Session["idAdmin"] != null)
            {

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
           
        }

        [HttpPost]
        public ActionResult cadastrarVendedor(Vendedores v)
        {
            if (Session["idAdmin"] != null)
            {


            VendedoresDAO objVe = new VendedoresDAO();
            if (objVe.cadastrarVendedor(v) == "1")
            {
                TempData["Msg"] = "Vendedor cadastrado com sucesso!";
                return RedirectToAction("VendedoresGrid");
            }
            else
            {
                ViewBag.retorno = "Erro ao cadastrar vendedor";
                return View("VendedoresInsert");
            }


            
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
           
        }       


        [HttpGet]
        public ActionResult VendedoresDelete(int id)
        {
            if (Session["idAdmin"] != null)
            {

            VendedoresDAO objVD = new VendedoresDAO();
            Vendedores objV = objVD.GetVendedores(id);
            return View(objV);

           

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }



        [HttpPost]
        public ActionResult VendedoresDelete(Vendedores objV)
        {

            if (Session["idAdmin"] != null)
            {
            


            VendedoresDAO objVDAO = new VendedoresDAO();
            String Msg = objVDAO.VendedoresDelete(objV.idVendedor);

            TempData["Msg"] = Msg;
            return RedirectToAction("VendedoresGrid");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        

        [HttpGet]
        public ActionResult VendedoresEdit(int id)
        {
            if (Session["idAdmin"] != null)
            {

            VendedoresDAO objVD = new VendedoresDAO();
            Vendedores objV = objVD.GetVendedores(id);
            return View(objV);

            
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }



        [HttpPost]
        public ActionResult VendedoresEdit(Vendedores v)
        {
            if (Session["idAdmin"] != null)
            {
            VendedoresDAO objVD = new VendedoresDAO();
            String Msg = objVD.VendedoresEdit(v);

            TempData["Msg"] = Msg;
            return RedirectToAction("VendedoresGrid");

           
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }



        //CLIENTES

        public ActionResult ClientesGrid(int? page)
        {
            if (Session["idAdmin"] != null)
            {

                int pageSize = 10;
                int pageIndex = 1;
                pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

                ClientesDAO objCli = new ClientesDAO();
                IPagedList<Clientes> CliPage = null;

                CliPage = objCli.orderCli(pageIndex, pageSize);
                return View(CliPage);

            
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }


        [HttpPost]
        public ActionResult ClientesGrid(Clientes c, int? page)
        {
            if (Session["idAdmin"] != null)
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
                return RedirectToAction("Index", "Home");
            }
        }


        public ActionResult ClientesInsert()
        {

            return View();
        }



        [HttpPost]
        public ActionResult cadastrarCliente(Clientes c)
        {
            if (Session["idAdmin"] != null)
            {

            ClientesDAO objCli = new ClientesDAO();
            if (objCli.cadastrarCliente(c) == "1")
            {
                TempData["Msg"] = "Cliente cadastrado com sucesso!";
                return RedirectToAction("ClientesGrid");
            }
            else
            {
                ViewBag.retorno = "Erro ao cadastrar cliente";
                return View("ClientesInsert");
            }

            
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }


        [HttpGet]
        public ActionResult ClientesDelete(int id)
        {
            if (Session["idAdmin"] != null)
            {

            ClientesDAO objCD = new ClientesDAO();
            Clientes objC = objCD.GetClientes(id);
            return View(objC);
            
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }


        [HttpPost]
        public ActionResult ClientesDelete(Clientes c)
        {
            if (Session["idAdmin"] != null)
            {

            ClientesDAO objCDAO = new ClientesDAO();
            String Msg = objCDAO.ClientesDelete(c.idCliente);

            TempData["Msg"] = Msg;
            return RedirectToAction("ClientesGrid");
            
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpGet]
        public ActionResult ClientesEdit(int id)
        {
            if (Session["idAdmin"] != null)
            {
            ClientesDAO objCD = new ClientesDAO();
            Clientes objC = objCD.GetClientes(id);
            return View(objC);

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

    


        [HttpPost]
        public ActionResult ClientesEdit(Clientes c)
        {
            if (Session["idAdmin"] != null)
            {
            ClientesDAO objCD = new ClientesDAO();
            String Msg = objCD.ClientesEdit(c);

            TempData["Msg"] = Msg;
            return RedirectToAction("ClientesGrid");
          
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }





        //PRODUTOS

        public ActionResult ProdutosGrid(int? page)
        {
            if (Session["idAdmin"] != null)
            {

                int pageSize = 10;
                int pageIndex = 1;
                pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

                ProdutosDAO objProd = new ProdutosDAO();
                IPagedList<Produtos> ProdPage = null;

                ProdPage = objProd.orderProd(pageIndex, pageSize);
                return View(ProdPage);

           
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }


        [HttpPost]
        public ActionResult ProdutosGrid(Produtos p, int? page)
        {
            if (Session["idAdmin"] != null)
            {

                string buscar = p.descricaoProduto;

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
                return RedirectToAction("Index", "Home");
            }
        }



        public ActionResult ProdutosInsert()
        {
            if (Session["idAdmin"] != null)
            {

            List<Categorias> ListCat = new List<Categorias>();
            CategoriasDAO objCDAO = new CategoriasDAO();

            ListCat = objCDAO.listarCategorias();
            List<SelectListItem> CatSel = new List<SelectListItem>();
            //ListCat.Add(new SelectListItem { Text = "Select", Value = "0" });

            foreach (Categorias cat in ListCat)
            {
                CatSel.Add(new SelectListItem { Text = cat.nomeCategoria, Value = Convert.ToString(cat.idCategoria) });
            }
            ViewData["Categorias"] = CatSel;

            return View();
            
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        public ActionResult cadastrarProduto(Produtos p)
        {
            if (Session["idAdmin"] != null)
            {

            ProdutosDAO objPro = new ProdutosDAO();
            if (objPro.cadastrarProduto(p) == "1")
            {
                TempData["Msg"] = "Produto cadastrado com sucesso!";
                return RedirectToAction("ProdutosGrid");
            }
            else
            {
                ViewBag.retorno = "Erro ao cadastrar produto";
                return View("ProdutosInsert");
            }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpGet]
        public ActionResult ProdutosDelete(string id)
        {
            if (Session["idAdmin"] != null)
            {
                            
            ProdutosDAO objPD = new ProdutosDAO();
            Produtos objP = objPD.GetProdutos(id);
            return View(objP);

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        public ActionResult ProdutosDelete(Produtos p)
        {
            if (Session["idAdmin"] != null)
            {

            ProdutosDAO objPD = new ProdutosDAO();
            String Msg = objPD.ProdutosDelete(p.idProduto);

            TempData["Msg"] = Msg;
            return RedirectToAction("ProdutosGrid");
            
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpGet]
        public ActionResult ProdutosEdit(string id)
        {
            if (Session["idAdmin"] != null)
            {
                            
            ProdutosDAO objPD = new ProdutosDAO();
            Produtos objP = objPD.GetProdutos(id);
            return View(objP);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        public ActionResult ProdutosEdit(Produtos p)
        {
            if (Session["idAdmin"] != null)
            {

            ProdutosDAO objPD = new ProdutosDAO();
            String Msg = objPD.ProdutosEdit(p);

            TempData["Msg"] = Msg;
            return RedirectToAction("ProdutosGrid");
            
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }









        //CATEGORIAS

        public ActionResult CategoriasGrid()
        {
            if (Session["idAdmin"] != null)
            {

            CategoriasDAO objCli = new CategoriasDAO();
            List<Categorias> ListCat = new List<Categorias>();
            ListCat = objCli.listarCategorias();
            return View(ListCat);
            
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }


        public ActionResult CategoriasInsert()
        {
            if (Session["idAdmin"] != null)
            {

                return View(); 
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        public ActionResult cadastrarCategoria(Categorias c)
        {
            if (Session["idAdmin"] != null)
            {

            CategoriasDAO objCat = new CategoriasDAO();
            if (objCat.cadastrarCategoria(c) == "1")
            {
                TempData["Msg"] = "Categoria cadastrado com sucesso!";
                return RedirectToAction("CategoriasGrid");
            }
            else
            {
                ViewBag.retorno = "Erro ao cadastrar categoria";
                return View("CategoriasInsert");
            }


            }
            else
            {
                return RedirectToAction("Index", "Home");
            }


        }

        [HttpGet]
        public ActionResult CategoriasDelete(int id)
        {
            if (Session["idAdmin"] != null)
            {

            
            CategoriasDAO objCD = new CategoriasDAO();
            Categorias objC = objCD.GetCategorias(id);
            return View(objC);

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        public ActionResult CategoriasDelete(Categorias c)
        {
            if (Session["idAdmin"] != null)
            {
            CategoriasDAO objCDAO = new CategoriasDAO();
            String Msg = objCDAO.CategoriasDelete(c.idCategoria);

            TempData["Msg"] = Msg;
            return RedirectToAction("CategoriasGrid");
            

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }


        [HttpGet]
        public ActionResult CategoriasEdit(int id)
        {
            if (Session["idAdmin"] != null)
            {
                            
            CategoriasDAO objCD = new CategoriasDAO();
            Categorias objC = objCD.GetCategorias(id);
            return View(objC);

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        public ActionResult CategoriasEdit(Categorias c)
        {
            if (Session["idAdmin"] != null)
            {

            CategoriasDAO objCD = new CategoriasDAO();
            String Msg = objCD.CategoriasEdit(c);

            TempData["Msg"] = Msg;
            return RedirectToAction("CategoriasGrid");
            
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }










    }
}
