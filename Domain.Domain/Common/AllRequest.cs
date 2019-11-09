namespace ELI.Data.Repositories.Main
{
    /// <summary>
    /// AllRequest class is used as base class for All Object request.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AllRequest<T>
    {
        #region Propeties
        public T Data { get; set; }
        public int Offset { get; set; }
        public int PageSize { get; set; }
        public string SortColumn { get; set; }
        public bool SortAscending { get; set; }
        public int FilterColumnId { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public bool Active { get; set; }
        public string SearchText { get; set; }
        #endregion
    }
}