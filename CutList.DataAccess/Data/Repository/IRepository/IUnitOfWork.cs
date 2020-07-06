using System;
using System.Collections.Generic;
using System.Text;

namespace CutList.DataAccess.Data.Repository.IRepository
{
    //Access to all repositories.
    //Single batch (TRANSACTION) to push all to database
    //allows to release
    public interface IUnitOfWork : IDisposable
    {
        IJobRepository Job { get; }         //get only

        //allowing us to get the IFrequencyRepository
        IFrequencyRepository Frequency { get; }

        IServiceRepository Service { get; }


        void Save();
    }
}
