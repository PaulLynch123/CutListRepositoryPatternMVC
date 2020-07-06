using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace CutList.Models.ViewModels
{
    public class ServiceViewModel
    {
        public Service Service { get; set; }

        public IEnumerable<SelectListItem> JobList { get; set; }
        public IEnumerable<SelectListItem> FrequencyList { get; set; }

    }
}
