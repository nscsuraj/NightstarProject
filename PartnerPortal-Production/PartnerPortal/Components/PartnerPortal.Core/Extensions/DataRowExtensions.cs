using System.Collections.Generic;
using System.Data;

namespace System
{
    /// <summary>
    ///     Class containing extension methods for System.Data.DataRow
    /// </summary>
    /// <remarks>
    ///    Date            Developer   Description
    ///    07/04/2013      Dwarika     Created   
    /// </remarks>
    public static class DataRowExtensions
    {
        /// <summary>
        /// Checks if a DataRow has a column and if so, does that column have a non-null value.
        /// </summary>
        /// <param name="row">The DataRow to check for the column.</param>
        /// <param name="columnName">The name of the column to look for.</param>
        /// <returns>Boolean flag indicating if the column exists and has a non-null value.</returns>
        public static bool ColumnHasValue(this DataRow row, string columnName)
        {
            return row.Table.Columns.Contains(columnName) && !row[columnName].Equals(DBNull.Value);
        }

        /// <summary>
        /// Tries to get the value of a DateTime column from the DataRow.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="row">The row to get the value from.</param>
        /// <param name="columnName">The name of the column that holds the value.</param>
        /// <param name="defaultValue">The value to use if the column is null if not found.</param>
        /// <param name="dateFormatter">The way to format the date</param>
        /// <returns>Correctly casted date column value or default value.</returns>
        public static T TryGetColumnDate<T>(this DataRow row, string columnName, T defaultValue, string dateFormatter = "G")
        {
            var type = typeof(T);

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                type = Nullable.GetUnderlyingType(type);
            }

            if (row.ColumnHasValue(columnName))
            {
                var date = ((DateTime)Convert.ChangeType(row[columnName], typeof(DateTime))).ToString(dateFormatter);

                return (T)Convert.ChangeType(date, type);
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Tries to get the value of a column from the DataRow.
        /// </summary>
        /// <param name="row">The row to get the value from.</param>
        /// <param name="columnName">The name of the column that holds the value.</param>
        /// <param name="defaultValue">The value to use if the column is null if not found.</param>
        /// <returns>Correctly cast column value or default value.</returns>
        public static T TryGetColumnValue<T>(this DataRow row, string columnName, T defaultValue = default(T))
        {
            var type = typeof(T);

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                type = Nullable.GetUnderlyingType(type);
            }

            try
            {
                return row.ColumnHasValue(columnName) ? (T)Convert.ChangeType(row[columnName], type) : defaultValue;
            }
            catch (FormatException)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Tries to get the fields of the row into a dictionary collection. It will not try to get rows already in 
        /// the collection provided.
        /// </summary>
        /// <param name="row">The row for which the fields need to be retrieved.</param>
        /// <param name="fields">The fields that have already been added.</param>
        /// <returns>A dictionary collection of fields.</returns>
        public static IDictionary<string, string> TryGetDictionary(this DataRow row, IDictionary<string, string> fields)
        {
            foreach (DataColumn col in row.Table.Columns)
            {
                if (!fields.ContainsKey(col.ColumnName))
                {
                    if (col.ColumnName.StartsWith("dt"))
                    {
                        DateTime? date = row.TryGetColumnDate(col.ColumnName, default(DateTime?));
                        fields.Add(col.ColumnName, date.HasValue ? date.Value.ToShortDateString() : null);
                    }
                    else
                    {
                        fields.Add(col.ColumnName,
                                   row.TryGetColumnValue<string>(col.ColumnName, null) != null
                                       ? row[col.ColumnName].ToString().NullIfTrimEmpty()
                                       : null);
                    }
                }
            }
            return fields;
        }
    }
}