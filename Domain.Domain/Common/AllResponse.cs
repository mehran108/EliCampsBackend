using System.Collections.Generic;

namespace ELI.Data.Repositories.Main
{
    /// <summary>
    /// AllResponse class is used as base class for All Object response.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AllResponse<T>
    {
        #region Propeties
        public List<T> Data { get; set; }
        public int Offset { get; set; }
        public int PageSize { get; set; }
        public int TotalRecord { get; set; }
        public string SortColumn { get; set; }
        public bool SortAscending { get; set; }
        public int FilterColumnId { get; set; }
        public string SearchText { get; set; }

        #endregion
    }
}