using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Visea_Expense_Manager.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Nature { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date_of_creation { get; set; }
        public string Type { get; set; }
        public int Type_Id { get; set; }
        public string Classe_str { get; set; }
        public int Classe_Id { get; set; }
        public string Classe2_str { get; set; }
        public int Classe2_Id { get; set; }
        public string Classe3_str { get; set; }
        public int Classe3_Id { get; set; }
        public string Classe4_str { get; set; }
        public int Classe4_Id { get; set; }
        public int UserId { get; set; }
        public string User { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [Range(typeof(DateTime), "00:01", "23:59")]
        public DateTime Heures { get; set; }
        public string commente { get; set; }
    }
}
