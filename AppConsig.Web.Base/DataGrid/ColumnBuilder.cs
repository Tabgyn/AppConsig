using System;
using System.Linq.Expressions;

namespace AppConsig.Web.Base.DataGrid
{
    /// <summary>
    /// Create instances of DataTableColumns.
    /// </summary>
    /// <typeparam name="TModel">Type of model to render in the table.</typeparam>
    public class ColumnBuilder<TModel> where TModel : class
    {
        public DataTableBuilder<TModel> TableBuilder { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tableBuilder">Instance of a DataTableBuilder.</param>
        public ColumnBuilder(DataTableBuilder<TModel> tableBuilder)
        {
            TableBuilder = tableBuilder;
        }

        /// <summary>
        /// Add lambda expressions to the DataTableBuilder.
        /// </summary>
        /// <typeparam name="TProperty">Class property that is rendered in the column.</typeparam>
        /// <param name="expression">Lambda expression identifying a property to be rendered.</param>
        /// <returns>An instance of DataTableColumn.</returns>
        public IDataTableColumn Expression<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            return TableBuilder.AddColumn(expression);
        }
    }
}