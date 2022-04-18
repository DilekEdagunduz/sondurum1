using System;
using System.Collections.Generic;

namespace HelloCSharp
{
    //Alışveriş sepetin
    public class Cart
    {
        public List<CartItem> CartItems { get; set; }

        public DateTime OrderDate { get; set; }
    }

}
