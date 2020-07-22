using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace VictorLimaoTesteCarglass.Models
{

    public class Fornecedor
    {
        [Key]
        public int Id { get; set; }


        public string Empresa { get; set; }

        public string NomeFornecedor { get; set; }

        public string CPFCNPJ { get; set; }


        public DateTime DataCadastro { get; set; }


        public string Telefone { get; set; }

        public string SecurityHash { get; set; }

       
    }
    
}