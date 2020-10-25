using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterviewTestPagination.Models.Page
{
    public class Page<T>
    {
        public IEnumerable<T> Collection { get; }

        public int NumberOfPages { get; }

        public int PageSize { get; }

        public int CurrentPage { get; }

        public int TotalItems { get; }

        public Page(int currentPage, int pageSize, IEnumerable<T> originalCollection)
        {
            this.CurrentPage = currentPage > 0 ? currentPage : 1;
            this.PageSize = pageSize > 0 ? pageSize : 1;
            this.TotalItems = originalCollection.Count();
            this.NumberOfPages = (int)Math.Ceiling(TotalItems / (decimal)this.PageSize);

            var firstItem = (this.CurrentPage - 1) * this.PageSize;
            int lastItem = Math.Min(firstItem + this.PageSize - 1, this.TotalItems - 1);

            var collection = new List<T>();
            for (var i = firstItem; i <= lastItem; i++)
                collection.Add(originalCollection.ElementAt(i));

            this.Collection = collection;
        }
    }
}