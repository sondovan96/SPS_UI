using System;
using System.Collections.Generic;
using System.Text;

namespace SPS.UI.Data.Core
{
    public class PageListModel<T>
    {
        #region Properties
        /// <summary>
        /// Data source
        /// </summary>
        public T[] Source { get; set; }
        /// <summary>
        /// Page Index
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// Page Size
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Total items
        /// </summary>
        public int TotalItems { get; set; }
        /// <summary>
        /// Total Pages
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// Has Previous Page
        /// </summary>
        public bool HasPreviousPage => PageIndex > 1;
        /// <summary>
        ///Has Next Page
        /// </summary>
        public bool HasNextPage => PageIndex < TotalPages;
        #endregion
    }
}
