using System.Collections.Generic;
using System.Linq;

namespace System.Data
{
    /// <summary>
    /// Class containing extension methods for System.Data.DataTable
    /// </summary>
    /// <remarks>
    ///    Date            Developer   Description
    ///    07/04/2013      Dwarika     Created   
    /// </remarks>
    public static class DataTableExtensions
    {
        /// <summary>
        /// Verifies the table has at least one row of data.
        /// </summary>
        /// <param name="table">The DataTable.</param>
        /// <returns>True if the table contains one or more rows.</returns>
        public static bool RowsExist(this DataTable table)
        {
            return (table.Rows.Count > 0);
        }

        /// <summary>
        /// Converts this DataTable into a Dictionary with the specified key and values formatted 
        /// according to the formatter.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dataTable">The data table.</param>
        /// <param name="keyColumnName">Name of the key column.</param>
        /// <param name="valueFormatter">The value formatter.</param>
        /// <param name="valueColumnNames">The value column names.</param>
        /// <returns></returns>
        public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(this DataTable dataTable, string keyColumnName, string valueFormatter, params string[] valueColumnNames) where TValue : class
        {
            if (!dataTable.Columns.Contains(keyColumnName))
                throw new ArgumentException("The keyColumnName {0} does not exist in the DataTable", keyColumnName);
            if (dataTable.Columns[keyColumnName].DataType != typeof(TKey))
                throw new ArgumentException("The column named {0} cannot be converted to a type specified as the TKey generic parameter.", keyColumnName);
            foreach (var valueColumnName in valueColumnNames)
            {
                if (!dataTable.Columns.Contains(valueColumnName))
                    throw new ArgumentException("The valueColumnName {0} does not exist in the DataTable", valueColumnName);
            }
            // This throws an exception if it fails.  This is my quick return.  
            // I don't want to run the lambda operation if the eventual string.Format will fail.
            string.Format(valueFormatter, valueColumnNames);

            return dataTable.Rows.Cast<DataRow>().ToDictionary(
                dr => (TKey)dr[keyColumnName],
                dr =>
                {
                    var columnArray = new object[valueColumnNames.Count()];
                    for (var index = 0; index < valueColumnNames.Length; index++)
                    {
                        columnArray[index] = dr[valueColumnNames[index]];
                    }
                    return string.Format(valueFormatter, columnArray) as TValue;
                });
        }

        /// <summary>
        /// Converts this DataTable into a Dictionary with the specified key,value types.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dataTable">The data table.</param>
        /// <param name="keyColumnName">Name of the key column.</param>
        /// <param name="valueColumnName">Name of the value column.</param>
        /// <returns>
        /// An IDictionary with keys of the specified TKey type and values of the specified TValue type.
        /// </returns>
        public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(this DataTable dataTable, string keyColumnName, string valueColumnName) where TValue : class
        {
            //if (!dataTable.Columns.Contains(keyColumnName))
            //    throw new ArgumentException("The keyColumnName {0} does not exist in the DataTable", keyColumnName);
            //if (dataTable.Columns[keyColumnName].DataType != typeof(TKey))
            //    throw new ArgumentException("The column named {0} cannot be converted to a type specified as the TKey generic parameter.", keyColumnName);
            if (!dataTable.Columns.Contains(valueColumnName))
                throw new ArgumentException("The valueColumnName {0} does not exist in the DataTable", valueColumnName);
            if (dataTable.Columns[valueColumnName].DataType != typeof(TValue))
                throw new ArgumentException("The column named {0} cannot be converted to a type specified as the TValue generic parameter.", valueColumnName);
            return dataTable.Rows.Count != 0 ? dataTable.ToDictionary<TKey, TValue>(keyColumnName, "{0}", valueColumnName) : new Dictionary<TKey, TValue>();
        }

        /// <summary>
        /// Converts this DataTable into a Dictionary with the specified key,value types.
        /// Assumes the first column in the DataSet's first DataTable is the key and second is the value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dataTable">The data table.</param>
        /// <returns>
        /// An IDictionary with keys of the specified TKey type and values of the specified TValue type.
        /// </returns>
        public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(this DataTable dataTable) where TValue : class
        {
            if (dataTable.Columns.Count < 2)
                throw new ArgumentException("The DataTable object must have at least 2 columns to use this method.");

            string keyColumnName = dataTable.Columns[0].ColumnName;
            string valueColumnName = dataTable.Columns[1].ColumnName;

            return dataTable.ToDictionary<TKey, TValue>(keyColumnName, valueColumnName);
        }

        /// <summary>
        /// Converts this DataTable into a Dictionary with the specified key,value types.
        /// Assumes the first column in the DataSet's first DataTable is the key and the rest of the colums are delimted into the value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dataTable">The data table.</param>
        /// <returns>
        /// An IDictionary with keys of the specified TKey type and values of the specified TValue type.
        /// </returns>
        public static IDictionary<TKey, TValue> ToDelimtedDictionary<TKey, TValue>(this DataTable dataTable, string delimiter) where TValue : class
        {
            if (dataTable.Columns.Count < 2)
                throw new ArgumentException("The DataTable object must have at least 2 columns to use this method.");

            return dataTable.Rows.Cast<DataRow>().ToDictionary(
                dr => (TKey)dr[0],
                dr =>
                {
                    var columnArray = new string[dataTable.Columns.Count - 1];
                    for (var index = 0; index < columnArray.Length; index++)
                    {
                        columnArray[index] = dr[dataTable.Columns[index + 1]].ToString();
                    }
                    return string.Join(delimiter, columnArray) as TValue;
                });
        }

        /// <summary>
        /// Converts the DataTable to a List of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of object the list supports.</typeparam>
        /// <param name="dataTable">The data table.</param>
        /// <returns>
        /// An IList of the specified type T that contains the information in the DataTable's first column.
        /// </returns>
        public static IList<T> ToList<T>(this DataTable dataTable)
        {
            if (dataTable.Columns.Count == 0)
                throw new ArgumentException("The DataTable must have at least one column.");
            string firstColumnName = dataTable.Columns[0].ColumnName;

            return dataTable.ToList<T>(firstColumnName);
        }

        /// <summary>
        /// Converts the DataTable to a List of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of object the list supports.</typeparam>
        /// <param name="dataTable">The data table.</param>
        /// <param name="columnName">Name of the column in the DataTable that will be put in the resultant IList of type T.</param>
        /// <returns>
        /// An IList of the specified type T that contains the information in the specified column name of the DataTable.
        /// </returns>
        public static IList<T> ToList<T>(this DataTable dataTable, string columnName)
        {
            if (!dataTable.Columns.Contains(columnName))
                throw new ArgumentException("The DataTable doesn't contain a column named {0}.", columnName);
            if (dataTable.Columns[columnName].DataType != typeof(T))
                throw new ArgumentException("The specified column {0} isn't the same as the type specified by the ToList<T> generic parameter", columnName);
            if (dataTable.Rows.Count == 0)
                return new List<T>();

            return (from DataRow dr in dataTable.Rows select (T)dr[columnName]).ToList();
        }
    }
}
