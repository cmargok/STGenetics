using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGenetics.Application.Models.Order
{
    public class OrderIn
    {
        public string ClientName { get; set; } = String.Empty;
        public List<int> AnimalsIds { get; set; } = new List<int>();

    }
}
