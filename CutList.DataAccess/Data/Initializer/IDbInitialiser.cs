using System;
using System.Collections.Generic;
using System.Text;

namespace CutList.DataAccess.Data.Initializer
{
    public interface IDbInitialiser
    {
        //only method to seed data
        void Initialise();
    }
}
