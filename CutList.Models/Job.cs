using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CutList.Models
{
    class Job
    {
        [Key]
        public int Id { get; set; }
        public int Won { get; set; }                //Primary key from another database??????????? don't want auto increment

        public string Prod_Range { get; set; }      //enum

        [DataType(DataType.Date)]
        public DateTime Rqd_Ship_Date { get; set; }
        [DataType(DataType.Date)]
        public DateTime Orig_Rqd_Ship_Date { get; set; }
        [DataType(DataType.Date)]
        public DateTime P_Manu_F { get; set; }
        [Display(Name ="Approval Status")]
        public bool Status { get; set; }

        public string Project_Id { get; set; }      //from another database
        public string Project_Name { get; set; }    //from another database
        public int BB_Run { get; set; }
        public int Batch_Details { get; set; }
        public string Job_Notes { get; set; }
        public string Engineer { get; set; }        //list of employee names populated from where?
        public string Checked_By { get; set; }      //list of approvers names
        public string Joint_Pack_Check { get; set; }    //mostly null not sure what for
        public string Ip { get; set; }                  //usually IP55... not sure what this is?
        public bool Heat_Sink { get; set; }             //yes or No
        public DateTime Date_Modified { get; set; }     //is this a typed date or can I get it from system? Does it record when required date changed
        public string Label { get; set; }               //is there a list here too or is it free text?
        public bool Tinner_Option { get; set; }         //yes or no for Tinned or not. Will this ever change?
        public bool Date_Changed_Flag { get; set; }     //notification that there is a date change
        public bool Specials_Included { get; set; }     //yes or no for notification of specials
        public bool Label_Ka { get; set; }              //what is KA???
        public bool Label_Neutral { get; set; }         //yes no if Neutral
        public bool Label_Earth { get; set; }           //yes no if Earth


        //------ foreign key dependency ------
        public int JobPartsId { get; set; }

        //--------- navigation links in Tables -----------

        public ICollection<JobPart> JobParts { get; set; }
        

    }
}
