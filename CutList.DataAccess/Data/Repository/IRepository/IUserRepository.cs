using CutList.Models;
using Microsoft.AspNetCore.Mvc.Rendering;       //selectList
using System;
using System.Collections.Generic;
using System.Text;

namespace CutList.DataAccess.Data.Repository.IRepository
{
    //putting my Job model in the IRepository
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        //lock a user (string as it is a Grid)
        void LockUser(string userId);

        void UnLockUser(string userId);

    }
}
