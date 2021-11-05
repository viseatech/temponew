using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Visea_Expense_Manager.Data;

namespace Visea_Expense_Manager.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; }
        public string Director { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date_of_creation { get; set; }
        
        [Phone]
        public string Phone { get; set; }
        public int CopierId { get; set; }

    }
}
