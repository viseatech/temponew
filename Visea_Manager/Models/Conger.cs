using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Visea_Expense_Manager.Models
{
    public class Conger
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date_Debut { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime Date_Fin { get; set; }
        public int Demijourne_Debut { get; set; }
        public int Demijourne_Fin { get; set; }
        public float time { get; set; }
        public string Type { get; set; }
        public string User { get; set; }
        public int UserId { get; set; }
        public string Director { get; set; }
        public string State { get; set; }
        public string commente { get; set; }

    }
}
