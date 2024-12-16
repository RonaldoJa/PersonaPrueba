using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Personas.DAC.Helper
{
    public class ConnectionDataAccess
    {
        private readonly string _dataBase;

        public ConnectionDataAccess(IConfiguration configuration, string database)
        {
            _dataBase = configuration.GetConnectionString(database);
        }

        public int ExecuteSPQuery(string procedureName, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(_dataBase))
            {
                using (SqlCommand command = new SqlCommand(procedureName, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    foreach(SqlParameter sqlParameter in parameters)
                    {
                        command.Parameters.Add(sqlParameter);
                    }
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
            
        }

        public DataSet ExecuteFillSp(string procedureName, params SqlParameter[] parameters)
        {
            DataSet dataSet = new DataSet();
            using (SqlConnection connection = new SqlConnection(_dataBase))
            {
                using (SqlCommand command = new SqlCommand(procedureName, connection))
                {
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        if (parameters != null && parameters.Length > 0)
                        {
                            foreach (SqlParameter sqlParameter in parameters)
                            {
                                command.Parameters.Add(sqlParameter);
                            }
                        }

                        sqlDataAdapter.Fill(dataSet);
                        return dataSet;
                    }
                }
            }
        }

    }


}

