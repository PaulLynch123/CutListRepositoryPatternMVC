using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CutList.Models
{
    class JobParts
    {
        [Key]
        public int Order_No { get; set; }       //from another database
        public int Won { get; set; }            //FORGIGN KEY


    }
}
