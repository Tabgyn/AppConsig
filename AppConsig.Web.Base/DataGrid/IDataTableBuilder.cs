using System;
using System.Collections.Generic;

namespace AppConsig.Web.Base.DataGrid
{
    /// <summary>
    /// Properties and methods used by the consumer to configure the DataTableBuilder.
    /// </summary>
    public interface IDataTableBuilder<TModel> where TModel : class
    {
        DataTableBuilder<TModel> DataSource(IEnumerable<TModel> dataSource);
        DataTableBuilder<TModel> Columns(Action<ColumnBuilder<TModel>> columnBuilder);
    }
}