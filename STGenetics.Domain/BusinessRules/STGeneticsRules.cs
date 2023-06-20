using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGenetics.Domain.BusinessRules
{
    public class STGeneticsRules
    {

        public static decimal ApplyQuantityDiscount(decimal price) 
        {
            int Discount = 5;

            price = (price / 100) * 100 - Discount;

            return price;
        }



        public static decimal Apply200AnimalsDiscount(decimal total)
        {
            int Discount = 3;

            total = (total / 100) * 100 - Discount;

            return total;
        }

        public static decimal Apply3000yDiscount()
        {
            return 1000;

        }

       


    }
}
