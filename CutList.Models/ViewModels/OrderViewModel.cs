using System;
using System.Collections.Generic;
using System.Text;

namespace CutList.Models.ViewModels
{
    public class OrderViewModel
    {

        public OrderHeader OrderHeader { get; set; }

        //list of OrderDetails inside the OrderHeader
        public IEnumerable<OrderDetails> OrderDetails { get; set; }

    }
}
