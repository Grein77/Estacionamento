using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Park.Victor.Grein.Benner.Models
{
    public class Movimentacao
    {    
        [Key]
        public int MovimentacaoID { get; set; }
        [Required(ErrorMessage = "Se faz nescessário inserir uma Placa")]
        public string Placa { get; set; }
        [Display(Name = "Tempo Cobrado")]
        public int? TempoCobrado { get; set; }
        [Display(Name = "Valor Total")]
        public double? ValorTotal { get; set; }
        public DateTime Entrada { get; set; }
        public DateTime? Saida { get; set;}

        public TimeSpan? Duracao { get { return Saida != null ? Entrada.Subtract(Saida.Value) : (TimeSpan?)null; } }

        public virtual ICollection<Preco> Preco { get; set; }
    }
}