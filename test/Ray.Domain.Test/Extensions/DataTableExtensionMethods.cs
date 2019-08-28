using System.Linq;
using Gherkin.Ast;

namespace Ray.Domain.Test.Extensions
{
    public static class DataTableExtensionMethods
    {
        public static float ToFloat(this DataTable dataTable, int row, int column)
        {
            return float.Parse(dataTable.Rows.ElementAt(row).Cells.ElementAt(column).Value);
        }
    }
}
