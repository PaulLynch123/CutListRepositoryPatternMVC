﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CutList.Utility
{
    public static class StaticDetails
    {
        //list of static strings I will reference this instead
        public const string SessionCart = "Cart";

        //for use in sessions, service order submission
        public const string StatusSubmitted = "Submitted";
        public const string StatusApproved = "Approved";
        public const string StatusRejected = "Rejected";

        //user roles
        public const string Admin = "Admin";
        public const string Management = "Management";

        //stored procedure
        public const string cutStoredProcedure_GetAllJob = "cutStoredProcedure_GetAllJob";
    }
}
