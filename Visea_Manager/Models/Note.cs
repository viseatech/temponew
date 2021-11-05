using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Visea_Expense_Manager.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date_Debut { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date_Fin { get; set; }
        
        public string Type { get; set; }
        public string User { get; set; }
        public int UserId { get; set; }
        public string State { get; set; }
        public string Code { get; set; }
        public string Client { get; set; }
        public int ClientId { get; set; }
        public string Mission { get; set; }
        public int MissionId { get; set; }
        public string commente { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [DataType(DataType.Currency)]
        [Range(0, 99999.99)]
        public decimal Totale_Euros { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [DataType(DataType.Currency)]
        [Range(0, 99999.99)]
        public decimal Totale_Devises { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [DataType(DataType.Currency)]
        [Range(0, 99999.99)]
        public decimal Montant_Rembourser { get; set; }


        [Required(ErrorMessage = "Price is required")]
        [DataType(DataType.Currency)]
        [Range(0, 99999.99)]
        public decimal sous_deplacement { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [DataType(DataType.Currency)]
        [Range(0, 99999.99)]
        public decimal sous_frais_kilometrique { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [DataType(DataType.Currency)]
        [Range(0, 99999.99)]
        public decimal sous_voiture { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [DataType(DataType.Currency)]
        [Range(0, 99999.99)]
        public decimal sous_hotel_resto { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [DataType(DataType.Currency)]
        [Range(0, 99999.99)]
        public decimal sous_autres { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [DataType(DataType.Currency)]
        [Range(0, 99999.99)]
        public decimal Price { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public float? Prix { get; set; }

        [NotMapped]
        public List<IFormFile> Files { get; set; }

    }
}
