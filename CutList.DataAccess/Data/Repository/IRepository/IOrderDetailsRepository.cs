﻿using CutList.Models;
using Microsoft.AspNetCore.Mvc.Rendering;       //selectList
using System;
using System.Collections.Generic;
using System.Text;

namespace CutList.DataAccess.Data.Repository.IRepository
{
    //putting my Job model in the IRepository
    public interface IOrderDetailsRepository : IRepository<OrderDetails>
    {
       
    }
}
