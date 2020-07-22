using VictorLimaoTesteCarglass.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VictorLimaoTesteCarglassContext.ViewModels;

namespace Interbox.ViewModels
{
    public class HomeViewModel
    {
        public HomeViewModel() { 
        EmpresaModel = new Empresa();
        EmpresasList = new List<EmpresaViewModel>();
        }
        public Empresa EmpresaModel { get; set; }
        public List<EmpresaViewModel> EmpresasList { get; set; }

    }
}