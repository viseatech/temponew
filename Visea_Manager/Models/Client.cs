using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Visea_Expense_Manager.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Type_Id { get; set; }
    }
}
