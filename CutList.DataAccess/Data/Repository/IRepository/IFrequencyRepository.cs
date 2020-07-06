using System;
using CutList.Models;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CutList.DataAccess.Data.Repository.IRepository
{
    public interface IFrequencyRepository : IRepository<Frequency>
    {
        //update
        void Update(Frequency frequency);

        //select list
        IEnumerable<SelectListItem> GetFrequencyForDropdown();
    };

}
