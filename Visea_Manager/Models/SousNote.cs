using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Visea_Expense_Manager.Models
{
    public class SousNote
    {
        public int Id { get; set; }
        public string Objet { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [DataType(DataType.Currency)]
        [Range(0, 99999.99)]
        public decimal Totale_Euros { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [DataType(DataType.Currency)]
        [Range(0, 99999.99)]
        public decimal Totale_Devises { get; set; }
        public string Devises { get; set; }

        public bool Refacturation { get; set; }
        public int Taux_de_remboursement { get; set; }
        public int TVA { get; set; }
        public string Type { get; set; }
        public int TypeId { get; set; }

        public int Fiche_Mission { get; set; }
    }
}
