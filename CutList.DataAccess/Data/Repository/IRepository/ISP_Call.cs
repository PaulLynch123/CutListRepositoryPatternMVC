using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CutList.DataAccess.Data.Repository.IRepository
{
    public interface ISP_Call : IDisposable
    {
        //retrieve the stored procedure of a List
        IEnumerable<T> ReturnList<T>(string procedureName, DynamicParameters param = null);


        //stored procedure examples
        //execute without returning anything
        void ExecuteWithoutReturn(string procedureName, DynamicParameters param = null);

        //return number of rows updated or something like that
        T ExecuteReturnScaler<T>(string procedureName, DynamicParameters param = null);

    }
}
