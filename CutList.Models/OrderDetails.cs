using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CutList.Models
{
    public class OrderDetails
    {
        //ASP.NET with auto make this be the Primary Key
        public int Id { get; set; }

        [Required]
        public int OrderHeaderId { get; set; }

        [ForeignKey("OrderHeaderId")]
        public OrderHeader OrderHeader { get; set; }


        [Required]
        public int ServiceId { get; set; }

        [ForeignKey("ServiceId")]
        public Service Service { get; set; }


        //important details that we don't want to change after the services are in the order
        //EXAMPLE: don't want price to update after order is placed, even if we change on website
        [Required]
        public string ServiceName { get; set; }

        [Required]
        public double Price { get; set; }


    }
}
