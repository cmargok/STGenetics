using STGenetics.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace STGenetics.Domain.BusinessRules
{
    public class STGeneticsRules
    {
        public static TransactionToOrder ApplyQuantityDiscount(TransactionToOrder toOrder) 
        {
            decimal BullTotalPrice = 0;            

            var BullsCount = toOrder.Animals!.Count(c => c.Sex == "MALE");

            BullTotalPrice = toOrder.Animals!.Where(c => c.Sex == "MALE").Sum(c => c.Price);

            if (BullsCount > 50)
            {  
                BullTotalPrice = ApplyDiscount(BullTotalPrice, 5);
                toOrder.Discounts.Add("BullDiscount>50, 5%;");
            }            

            decimal CowTotalPrice = 0;

            var CowsCount = toOrder.Animals!.Count(c => c.Sex == "FEMALE");

            CowTotalPrice = toOrder.Animals!.Where(c => c.Sex == "FEMALE").Sum(c => c.Price);

            if (CowsCount > 50)
            {
                CowTotalPrice = ApplyDiscount(CowTotalPrice, 5);
                toOrder.Discounts.Add("CowDiscount>50, 5%;");
            }            

            toOrder.Total = BullTotalPrice + CowTotalPrice;

            return toOrder;
        }

        public static TransactionToOrder Apply200AnimalsDiscount(TransactionToOrder toOrder)
        {
            if (toOrder.Animals!.Count > 200) 
            {
                toOrder.Total = ApplyDiscount(toOrder.Total, 3);

                toOrder.Discounts.Add("Buy>200, 3%;");
            }
            return toOrder;
        }

        public static bool Apply300AnimalsDiscount(TransactionToOrder toOrder)
        {
            return toOrder.Animals!.Count > 300; 
        }


        private static decimal ApplyDiscount(decimal total, int DiscountPorcent) => (total / 100) * (100 - DiscountPorcent);

    }
}
