namespace AppConsig.Web.Base.DataGrid
{
    /// <summary>
    /// Properties and methods used within the DataTableBuilder class.
    /// </summary>
    public interface IDataTableColumnInternal<TModel> where TModel : class
    {
        string ColumnTitle { get; set; }
        string Evaluate(TModel model);
    }
}