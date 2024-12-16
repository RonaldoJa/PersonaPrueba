using System;
using System.Data;

namespace Personas.DAC.Helper
{
	public static class DbHelper
	{
		public static T GetCell<T>(this DataRow dataRow, string columnName)
		{
			if (dataRow.Table.Columns.Contains(columnName))
			{
				if (dataRow[columnName] == DBNull.Value)
					return default;
				return (T)dataRow[columnName];
			}
			return default;
		}
	}
}

