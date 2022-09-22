using System.Collections.Generic;

namespace System.Data
{
    /// <summary>
    ///     Class containing extension methods for System.Data.DataSet
    /// </summary>
    /// <remarks>
    ///    Date            Developer   Description
    ///    07/04/2013      Dwarika     Created   
    /// </remarks>
    public static class DataSetExtensions
    {
        /// <summary>
        /// Verifies the dataset has at least one table with data.
        /// </summary>
        /// <param name="dataSet">The dataset.</param>
        /// <returns>True if the dataset contains a populated table.</returns>
        public static bool DataExists(this DataSet dataSet)
        {
            return (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0);
        }
        /// <summary>
        /// Converts this DataSet into a Dictionary with the specified key,value types.  
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dataSet">The data set.</param>
        /// <param name="tableOrdinal">The table ordinal.</param>
        /// <param name="keyColumnName">Name of the key column.</param>
        /// <param name="valueFormatter">The value formatter.</param>
        /// <param name="valueColumnNames">The value column names.</param>
        /// <returns></returns>
        public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(this DataSet dataSet, int tableOrdinal, string keyColumnName, string valueFormatter, params string[] valueColumnNames) where TValue : class
        {
            if (dataSet.Tables.Count < tableOrdinal)
                throw new ArgumentException(string.Format("The DataSet doesn't contain enough tables.  There are {0} DataTables in the DataSet and you want the DataTable at the {1} position.", dataSet.Tables.Count, tableOrdinal));

            var dataTable = dataSet.Tables[tableOrdinal];
            return dataTable.ToDictionary<TKey, TValue>(keyColumnName, valueFormatter, valueColumnNames);
        }

        /// <summary>
        /// Converts this DataSet into a Dictionary with the specified key,value types.  
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dataSet">The data set.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="keyColumnName">Name of the key column.</param>
        /// <param name="valueFormatter">The value formatter.</param>
        /// <param name="valueColumnNames">The value column names.</param>
        /// <returns></returns>
        public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(this DataSet dataSet, string tableName, string keyColumnName, string valueFormatter, params string[] valueColumnNames) where TValue : class
        {
            if (!dataSet.Tables.Contains(tableName))
                throw new ArgumentException("The DataSet doesn't contain a table named {0}.", tableName);

            int tableIndex = dataSet.Tables.IndexOf(dataSet.Tables[tableName]);
            return dataSet.ToDictionary<TKey, TValue>(tableIndex, keyColumnName, valueFormatter, valueColumnNames);
        }

        /// <summary>
        /// Converts this DataSet into a Dictionary with the specified key,value types.  
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="ds">The dataset.</param>
        /// <param name="keyColumnName">Name of the key column.</param>
        /// <param name="valueColumnName">Name of the value column.</param>
        /// <returns>An IDictionary with keys of the specified TKey type and values of the specified TValue type.</returns>
        public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(this DataSet ds, string keyColumnName, string valueColumnName) where TValue : class
        {
            if (ds.Tables.Count == 0)
                throw new ArgumentException("There are no tables in the specified DataTable object, please check the data source to verify you are returning data.");

            return ds.ToDictionary<TKey, TValue>(0, keyColumnName, "{0}", valueColumnName);
        }


        /// <summary>
        /// Converts this DataSet into a Dictionary with the specified key,value types.  
        /// Assumes the first column in the DataSet's first DataTable is the key and second is the value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="ds">The dataSet.</param>
        /// <returns>An IDictionary with keys of the specified TKey type and values of the specified TValue type.</returns>
        public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(this DataSet ds) where TValue : class
        {
            if (ds.Tables.Count == 0)
                throw new ArgumentException("There are no tables in the specified DataTable object, please check the data source to verify you are returning data.");

            var dataTable = ds.Tables[0];
            return dataTable.ToDictionary<TKey, TValue>();
        }
        /// <summary>
        /// Converts the DataSet to a generic List.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataSet">The data set.</param>
        /// <returns></returns>
        public static IList<T> ToList<T>(this DataSet dataSet)
        {
            if (dataSet.Tables.Count == 0)
                throw new ArgumentException("The DataSet must have at least one table.");

            return dataSet.Tables[0].ToList<T>();
        }

        /// <summary>
        /// Converts the DataSet to a generic List.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataSet">The data set.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns></returns>
        public static IList<T> ToList<T>(this DataSet dataSet, string columnName)
        {
            if (dataSet.Tables.Count == 0)
                throw new ArgumentException("The DataSet must have at least one table.");

            return dataSet.Tables[0].ToList<T>(columnName);
        }
    }
}