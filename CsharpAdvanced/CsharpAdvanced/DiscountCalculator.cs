using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpAdvanced
{
    //EX. Constraint where T : Product
    public class DiscountCalculator<TProduct> where TProduct : Product
    {
        public float CalculateDiscount(TProduct product)
        {
            return product.Price;
        }
    }
}