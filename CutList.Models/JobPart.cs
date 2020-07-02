using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CutList.Models
{
    class JobPart
    {
        [Key]
        public int Id { get; set; }
        //primary key and auto increment
        public int Order_No { get; set; }       //Primary key from another database??????????? don't want auto increment
        public int Won { get; set; }            //FORGIGN KEY

        public string Prod_Range { get; set; }

        public string Part_Id { get; set; }     //small number that must be extensively broken down and validated

        public string Tag_No { get; set; }      //need to know what this numbe is for

        public string Part_Code { get; set; }   //this will be defined list - broken down from Part_Id

        public string Rating { get; set; }      //this will be defined list - broken down from Part_Id

        public string Configuration { get; set; }       //this will be defined list - broken down from Part_Id

        public int Conductor_Size { get; set; }         //this might be defined list of values- broken down from Part_Id

        public int Aluminium_Size { get; set; }         //calculated

        public string Material { get; set; }            //this will be defined list - broken down from Part_Id

        public string Stack { get; set; }           //this will be defined list - broken down from Part_Id (Signle/double)

        public int Multiplier { get; set; }         //assume related to the stack

        [Range(1, 100, ErrorMessage = "Value for {0} should be between {1} and {2}")]   //estimated number of max parts per order
        public int Qty { get; set; }                //number of parts ???

        public string Earth_Warning { get; set; }   //how is this defined?

        public int Earth_Size { get; set; }         //related to conductor sie but for earth how is it calculated? or is it input?

        public string JC_Covers_Colour { get; set; }        //optional list of colours

        public string JSP_Cover_Colour { get; set; }        //optional list of colours

        public int Mylar_Lenght { get; set; }           //input or calculate?

        //many more checks then....
        // chekcs in other orders that look like supervisor checklist ticks

        public bool LidA_Check { get; set; }
        public bool LidB_Check { get; set; }
        public bool BaseA_Check { get; set; }
        public bool BaseB_Check { get; set; }


        //now we have checks in another order
        public bool L1A1_Check_1 { get; set; }
        public bool L1A1_Check_2 { get; set; }
        public bool L1A1_Check_3 { get; set; }
        public bool L1A1_Check_4 { get; set; }
        public bool L1A2_Check_1 { get; set; }
        public bool L1A3_Check_1 { get; set; }
        public bool L1A4_Check_1 { get; set; }


        public bool L1B1_Check_1 { get; set; }
        public bool L1B1_Check_2 { get; set; }
        public bool L1B1_Check_3 { get; set; }
        public bool L1B1_Check_4 { get; set; }
        public bool L1B2_Check_1 { get; set; }
        public bool L1B3_Check_1 { get; set; }
        public bool L1B4_Check_1 { get; set; }


        //------ foreign key dependency ------
        public int CheckListId { get; set; }        //convention name of Table and Id


        //------- navigation ------

        public Job Job { get; set; }            

        public CheckList CheckList { get; set; }

    }
}
