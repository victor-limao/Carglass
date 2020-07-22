using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VictorLimaoTesteCarglass.Models
{
    
    public class Empresa
    {
        [Key]
        public int Id { get; set; }

      
        public string UF { get; set; }

        
        public string NomeFantasia { get; set; }

        
        public string CNPJ { get; set; }

        public DateTime DataCadastro { get; set; }
        public int Maioridade { get; set; }

    }
}