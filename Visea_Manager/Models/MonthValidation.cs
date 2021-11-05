using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Visea_Expense_Manager.Models
{
    public class MonthValidation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Userstr { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

    }
}
