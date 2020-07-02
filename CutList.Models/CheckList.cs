using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CutList.Models
{
    public class CheckList
    {
        [Key]
        public int Id { get; set; }
        //primary key and auto increment
        public int Order_No { get; set; }           //Foreign Key

        //public int Won { get; set; }                //optional to have to create a composite primary key

        public bool Cu_Check { get; set; }          //tick box for work done

        [DataType(DataType.Date)]
        public DateTime Cu_Date { get; set; }           //ensure you understand this date. What exactly is the CU?

        public int Cu_Emp_No { get; set; }          //what is the CU exactly?


        public bool CuBend_Check { get; set; }          //tick box for work done

        [DataType(DataType.Date)]
        public DateTime CuBend_Date { get; set; }           //ensure you understand this date. What exactly is the CU?

        public int CuBend_Emp_No { get; set; }          //what is the CU exactly?


        //------ foreign key dependency ------
        //public int JobPartId { get; set; }        //convention name of Table and Id

        //----- navigation ---------

        //public JobPart JobPart { get; set; }


    }
}
