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
        public uint Offset { get; set; }
        public uint PageSize { get; set; }
        public uint TotalRecord { get; set; }
        public string SortColumn { get; set; }
        public bool SortAscending { get; set; }
        public uint FilterColumnId { get; set; }
        public string SearchText { get; set; }

        #endregion
    }
}