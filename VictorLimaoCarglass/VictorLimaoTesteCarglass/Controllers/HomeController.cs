using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VictorLimaoTesteCarglass.Models;
using VictorLimaoTesteCarglass.DAO;
using EstruturarRepository.Repository;

namespace VictorLimaoTesteCarglass.Controllers
{   public class HomeController : Controller
    {
        private Repository<Empresa> _empresa;
        private Repository<Fornecedor> _fornecedor;
        private Repository<FornecedorPF> _fornecedorpf;
        public HomeController()
        {
            _empresa = new Repository<Empresa>();
            _fornecedor = new Repository<Fornecedor>();
            _fornecedorpf = new Repository<FornecedorPF>();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult CadastroEmpresa()
        {
            ViewBag.Message = "Cadastro Empresa";
            return View();
        }
        public ActionResult CadastroFornecedor()
        {
            ViewBag.Message = "Cadastro Fornecedor";
            ViewBag.GetEmpresas = _empresa.GetAll();
            
            return View();
        }
        public static List<Fornecedor> GetFornecedores(string filtro = "")
      {
            var ret = new List<Fornecedor>();
            string conn = "Data Source=azuresqllimao.database.windows.net;User ID=victor.limao;Password=Sql@12345;Initial Catalog=Carglass;timeout=12000;";

            using (SqlConnection connection = new SqlConnection(conn))
            {
                var filtroWhere = string.IsNullOrEmpty(filtro) ? "" : string.Format(" where lower(NomeFornecedor) like '%{0}%' or lower(CPFCNPJ)  like '%{0}%' or lower(DataCadastro) like '%{0}%' ", filtro.ToLower());

                connection.Open();
                var sql = @"select * from Fornecedors" + filtroWhere + " order by NomeFornecedor";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();
                Fornecedor fornecedor = new Fornecedor();

                while (reader.Read())

                {
                    ret.Add(new Fornecedor
                    {
                        Empresa = (string)reader["Empresa"],
                        NomeFornecedor = (string)reader["NomeFornecedor"],
                        CPFCNPJ = (string)reader["CPFCNPJ"],
                        DataCadastro = (DateTime)reader["DataCadastro"],
                        Telefone = (string)reader["Telefone"],
                    });
                }
                return ret.ToList();
            }
        }
        public ActionResult ListaEmpresas()
        {
            ViewBag.Message = "Lista de Empresas";
            ViewBag.Empresas = _empresa.GetAll();
            return View();
        }
        public ActionResult ListaFornecedores(string filtro)
        {
            ViewBag.Message = "Lista de Fornecedores";
            ViewBag.Fornecedores = _fornecedor.GetAll();
            return View();
        }

        public JsonResult JsonLista (string filtro)
        {
            var lista = GetFornecedores(filtro);
            return Json(new { success = lista }, JsonRequestBehavior.AllowGet);
        }

        public object InsertEmpresa(string uf, string nomefantasia, string cnpj)
        {

            if(uf == "" || uf == null || nomefantasia == "" || nomefantasia == null || cnpj == "" || cnpj == null)
            {
                ModelState.AddModelError("", "Por favor, preencha o(s) seguinte(s) campo(s):");
                if (uf == "" || uf == null)
                {
                    ModelState.AddModelError("", "UF");

                }
                if (nomefantasia == "" || nomefantasia == null)
                {
                    ModelState.AddModelError("", "Nome Fantasia.");

                }
                if (cnpj == "" || cnpj == null)
                {
                    ModelState.AddModelError("", "CNPJ.");

                }
                return View("CadastroEmpresa");
            }

            Empresa empresa = new Empresa();
            empresa.Maioridade = 0;
            if (uf == "RJ" || uf == "MS" || uf == "MG")
            {
                empresa.Maioridade = 1;
            }


            empresa.UF = uf;
            empresa.NomeFantasia = nomefantasia;
            empresa.CNPJ = cnpj;
            empresa.DataCadastro = DateTime.Now;
            _empresa.Add(empresa);


            //var data = DateTime.Now;
            //using (SqlConnection connection = new SqlConnection("Data Source=azuresqllimao.database.windows.net;Initial Catalog=Carglass;User ID=victor.limao;Password=Sql@12345;timeout=12000;"))
            //{

            //    string sql = @"[dbo].[InsertEmpresa]";

            //    connection.Open();
            //    SqlCommand command = new SqlCommand(sql, connection);
            //    command.CommandType = CommandType.StoredProcedure;
            //    command.CommandText = sql;

            //    command.Parameters.Add(new SqlParameter("@UF", uf));
            //    command.Parameters.Add(new SqlParameter("@NomeFantasia", nomefantasia));
            //    command.Parameters.Add(new SqlParameter("@CNPJ", cnpj));
            //    command.Parameters.Add(new SqlParameter("@DataCadastro", data));
            //    //command.Parameters.Add(new SqlParameter("@Status", senha));

            //    command.ExecuteNonQuery();
            //}
            return RedirectToAction("CadastroEmpresa", "Home");
        }
        public object InsertFornecedor(string empresa, string nomefornecedor, string cpf, string telefone, string cnpj, string rg, DateTime? datanascimento)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[20];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var securityhash = stringChars[0].ToString() + stringChars[1].ToString() + stringChars[2].ToString() + stringChars[3].ToString() + stringChars[4].ToString() + stringChars[5].ToString() + stringChars[6].ToString() + stringChars[7].ToString() + stringChars[8].ToString() + stringChars[9].ToString() + stringChars[10].ToString() + stringChars[11].ToString() + stringChars[12].ToString() + stringChars[13].ToString() + stringChars[14].ToString() + stringChars[15].ToString() + stringChars[16].ToString() + stringChars[17].ToString() + stringChars[18].ToString() + stringChars[19].ToString();
            
            var data = DateTime.Now;
            var datamin = data.AddYears(-18);
            var maioridade = 0;
            if(datanascimento < datamin)
            {
                maioridade = 1;
            }
            ViewBag.GetEmpresas = _empresa.GetAll();

            
            if(empresa == null)
            {
                empresa = " , , ";
            }
          
            var empresaformat = empresa.Split(',');
            var separaempresa = empresaformat[0];
            int separauf = 2;
            if (empresaformat[1] == null || empresaformat[1] == " ")
            {
                separauf = Convert.ToInt32(separauf);
            }
            else
            {
                separauf = Convert.ToInt32(empresaformat[0].Trim(' '));
            }
            
            
            


            if (separaempresa == "" || separaempresa == null || nomefornecedor == "" || empresaformat[1] == null || empresaformat[1] == " " || nomefornecedor == null || ((cpf == "" || cpf == null) && (cnpj == "" || cnpj == null)) || telefone == "" || telefone == null || (maioridade < 1 && separauf == 1) || separauf == 2)
            {
                ModelState.AddModelError("", "Por favor, verifique o(s) seguinte(s) campo(s):");
                if (separaempresa == "" || separaempresa == null)
                {
                    ModelState.AddModelError("", "UF");

                }
                if(empresaformat[1] == null || empresaformat[1] == " ")
                {
                    empresaformat[1] = " 0";
                }
                if (empresaformat[0] == null || empresaformat[1] == "")
                {
                    empresaformat[0] = " ";
                }
                if (separauf == 2)
                {
                    ModelState.AddModelError("", "Empresa");

                }
                if (nomefornecedor == "" || nomefornecedor == null)
                {
                    ModelState.AddModelError("", "Nome Fantasia.");

                }
              
                if (telefone == "" || telefone == null)
                {
                    ModelState.AddModelError("", "Telefone.");

                }
                if (empresaformat[2] == null || empresaformat[2] == "")
                {
                    empresaformat[2] = "este estado.";
                }
                if(datanascimento > datamin)
                {
                    ModelState.AddModelError("", "Não é possível cadastrar menor de idade em "+ empresaformat[2]+ " como pessoa física.");
                }
                
                
                return View("CadastroFornecedor", ViewBag.GetEmpresas);
            }
            

            Fornecedor fornecedor = new Fornecedor();
            fornecedor.CPFCNPJ = cnpj;
            if(cpf != null && cnpj == "")
            {
                FornecedorPF fornecedorPFs = new FornecedorPF();
                if(datanascimento == null)
                {
                    fornecedorPFs.DataNascimento = datamin.AddMonths(1);
                }
                else if(datanascimento != null)
                {
                fornecedorPFs.DataNascimento = datanascimento;
                }
                fornecedorPFs.RG = rg;
                fornecedorPFs.CPF = cpf;
                fornecedorPFs.SecurityHash = securityhash;
                _fornecedorpf.Add(fornecedorPFs);
                fornecedor.CPFCNPJ = cpf;
            }


            fornecedor.Empresa = empresaformat[1];
            fornecedor.NomeFornecedor = nomefornecedor;
            fornecedor.DataCadastro = DateTime.Now;
            fornecedor.Telefone = telefone;
            fornecedor.SecurityHash = securityhash;
            _fornecedor.Add(fornecedor);

            return RedirectToAction("CadastroFornecedor", "Home");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}