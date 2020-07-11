using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CutList.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Service Name")]
        public string Name { get; set; }

        
        public double Price { get; set; }

        [Display(Name="Description")]
        public string LongDescription { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        //------ foreign key -----

        [Required]
        public int JobId { get; set; }

        public int FrequencyId { get; set; }

        //------ navigation -----
        [ForeignKey("JobId")]
        public Job Job { get; set; }

        [ForeignKey("FrequencyId")]
        public Frequency Frequency { get; set; }


    }
}
