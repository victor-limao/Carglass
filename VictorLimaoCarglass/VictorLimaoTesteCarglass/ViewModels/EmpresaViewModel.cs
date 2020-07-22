using VictorLimaoTesteCarglass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VictorLimaoTesteCarglassContext.ViewModels
{
    public class EmpresaViewModel
    {
        public int Id { get; set; }

        public string UF { get; set; }
        public string NomeFantasia { get; set; }
        public string CNPJ { get; set; }
        public DateTime DataCadastro { get; set; }

    }
}