using CutList.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CutList.DataAccess.Data.Repository.IRepository
{
    //putting my model in the IRepository
    public interface IServiceRepository : IRepository<Service>
    {
        //put in some extra methods (Update)
        void Update(Service service);


    }
}
