using CutList.DataAccess.Data.Repository.IRepository;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CutList.DataAccess.Data.Repository
{
    public class SP_Call : ISP_Call
    {
        //we are using SQL Connection NOT Entity Framework. We need connection string

        private readonly ApplicationDbContext _db;
        private static string ConnectionString = "";

        public SP_Call(ApplicationDbContext db)
        {
            _db = db;
            //get connection string from db object (Using entity framework core)
            ConnectionString = db.Database.GetDbConnection().ConnectionString;
        }



        public T ExecuteReturnScaler<T>(string procedureName, DynamicParameters param = null)
        {
            using SqlConnection sqlCon = new SqlConnection(ConnectionString);
            sqlCon.Open();
            //defining as stored procedure call that selects a signle value
            //sql stored procedure output is converted to type T and used as return type for method
            return (T)Convert.ChangeType(sqlCon.ExecuteScalar<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure), typeof(T));
        }

        public void ExecuteWithoutReturn(string procedureName, DynamicParameters param = null)
        {
            using SqlConnection sqlCon = new SqlConnection(ConnectionString);
            sqlCon.Open();
            //defining as stored procedure call
            sqlCon.Execute(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);
        }


        public IEnumerable<T> ReturnList<T>(string procedureName, DynamicParameters param = null)
        {
            //using the connection string from static variable above 'get'
            using SqlConnection sqlCon = new SqlConnection(ConnectionString);
            //open connection to SQL
            sqlCon.Open();
            //defining as stored procedure call
            return sqlCon.Query<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);
        }


        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
