using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Microsoft.Practices.EnterpriseLibrary.Data
{
    /// <summary>
    /// Class containing extension methods for Microsoft.Practices.EnterpriseLibrary.Data.Database
    /// </summary>
    /// <remarks>
    ///    Date            Developer   Description
    ///    07/04/2013      Dwarika     Created   
    /// </remarks>
    public static class DatabaseExtensions
    {
        /// <summary>
        /// Adds the in parameter to the command if the condition is true.
        /// </summary>
        /// <param name="database">The database.</param>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="dbType">Type of the db.</param>
        /// <param name="value">The value.</param>
        /// <param name="condition">if set to <c>true</c> [condition].</param>
        public static void AddConditionalInParameter(this Database database, DbCommand command, string name, DbType dbType, object value, bool condition)
        {
            if (condition)
            {
                database.AddInParameter(command, name, dbType, value);
            }
        }
        
        /// <summary>
        /// Adds the in parameter.
        /// </summary>
        /// <param name="database">The database.</param>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public static void AddInParameter(this Database database, DbCommand command, string name, int value)
        {
            database.AddInParameter(command, name, DbType.Int32, value);
        }
        
        /// <summary>
        /// Adds the in parameter.
        /// </summary>
        /// <param name="database">The database.</param>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public static void AddInParameter(this Database database, DbCommand command, string name, string value)
        {
            database.AddInParameter(command, name, DbType.String, value);
        }
        
        /// <summary>
        /// Adds the in parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="database">The database.</param>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="enumeration">The enumeration.</param>
        public static void AddInParameter<T>(this Database database, DbCommand command, string name, T enumeration) where T : struct, IConvertible
        {
            if (!enumeration.GetType().IsEnum)
                throw new ArgumentException("This should only be called with an enumeration.");

            var inValue = enumeration.ToInt32(null);
            database.AddInParameter(command, name, DbType.Int32, inValue);
        }

		/// <summary>
		/// Creates the open connection.
		/// </summary>
		/// <param name="db">The db.</param>
		/// <returns>An open db connection</returns>
		public static DbConnection CreateOpenConnection(this Database db)
		{
			var connection = db.CreateConnection();
			
			if(connection.State != ConnectionState.Connecting || connection.State != ConnectionState.Open)
			{
				connection.Open();
			}

			return connection;
		}

        /// <summary>
        /// Executes the conditionally transactional non query.
        /// Abstracts away dealing with null transactions so calling code doesn't have to worry about it.
        /// </summary>
        /// <param name="database">The database.</param>
        /// <param name="command">The command.</param>
        /// <param name="transaction">The transaction.</param>
        public static void ExecuteConditionallyTransactionalNonQuery(this Database database, DbCommand command, DbTransaction transaction)
        {
            if (transaction != null)
            {
                database.ExecuteNonQuery(command, transaction);
            }
            else
            {
                database.ExecuteNonQuery(command);
            }
        }
        
        /// <summary>
        /// Executes the conditionally transactional scalar.
        /// </summary>
        /// <typeparam name="T">The expected return type.  This is an assumed known value by the caller.</typeparam>
        /// <param name="database">The database.</param>
        /// <param name="command">The command.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        public static T ExecuteConditionallyTransactionalScalar<T>(this Database database, DbCommand command, DbTransaction transaction)
        {
            if (transaction != null)
                return (T)database.ExecuteScalar(command, transaction);

            return (T)database.ExecuteScalar(command);
        }
        
        /// <summary>
        /// Executes the conditionally transactional DataSet.
        /// </summary>
        /// <param name="database">The database.</param>
        /// <param name="command">The command.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        public static DataSet ExecuteConditionallyTransactionalDataSet(this Database database, DbCommand command, DbTransaction transaction)
        {
            DataSet ds = new DataSet();
            if (transaction != null)
            {
                ds = database.ExecuteDataSet(command, transaction);
            }
            else
            {
                ds = database.ExecuteDataSet(command);
            }

            return ds;
        }
        
        /// <summary>
        /// Executes the conditionally transactional DataTable.
        /// </summary>
        /// <param name="database">The database.</param>
        /// <param name="command">The command.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>the first recordset that was returned from the database call</returns>
        public static DataTable ExecuteConditionallyTransactionalDataTable(this Database database, DbCommand command, DbTransaction transaction)
        {
            DataTable dt = new DataTable();
            if (transaction != null)
            {
                dt = database.ExecuteDataSet(command, transaction).Tables[0];
            }
            else
            {
                dt = database.ExecuteDataSet(command, transaction).Tables[0];
            }

            return dt;
        }
        
        /// <summary>
        /// Gets a SqlDataReader object.
        /// </summary>
        /// <param name="database">Database object.</param>
        /// <param name="command">DbCommand object.</param>
        public static SqlDataReader ExecuteSqlDataReader(this Database database, DbCommand command)
        {
            var reader = (RefCountingDataReader)database.ExecuteReader(command);
            return (SqlDataReader)reader.InnerReader;
        }
    }
}
