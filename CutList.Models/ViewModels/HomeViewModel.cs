using System;
using System.Collections.Generic;
using System.Text;

namespace CutList.Models.ViewModels
{
    public class HomeViewModel
    {

        //dropdown info needed
        public IEnumerable<Job> JobList { get; set; }
        public IEnumerable<Service> ServiceList { get; set; }

    }
}
