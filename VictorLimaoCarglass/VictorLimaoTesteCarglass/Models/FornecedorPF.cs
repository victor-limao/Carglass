using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VictorLimaoTesteCarglass.Models
{

    public class FornecedorPF
    {
        [Key]
        public int Id { get; set; }


        public string RG { get; set; }
        public string SecurityHash { get; set; }

        public DateTime? DataNascimento { get; set; }
        public string CPF { get; set; }
    }
}