using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Park.Victor.Grein.Benner.Models
{
    public class Preco
    {
        public int PrecoID { get; set; }
        
        [Display(Name = "Vigência Inicial")]
        public DateTime VigenciaInicial { get; set; }
        [Display(Name = "Vigência Final")]
        public DateTime? VigenciaFinal { get; set; }
        [Display(Name = "Valor por hora")]
        public double ValorHora { get; set; }
        [Display(Name = "Valor por hora Adicional")]
        public double ValorHoraAdicional { get; set; }


        [ForeignKey("PrecoID")]
        public virtual Preco Precos { get; set; }

    }
}

    
