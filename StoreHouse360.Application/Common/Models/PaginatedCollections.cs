﻿using System.Collections;

namespace StoreHouse360.Application.Common.Models
{
    public interface IPaginatedCollections<T> : IEnumerable<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int PagesCount { get; set; }
    }

    public class PaginatedCollections<T> : IPaginatedCollections<T>
    {
        private IEnumerable<T> data;
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int PagesCount { get; set; }

        public IEnumerator<T> GetEnumerator()
        {
            return data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static PaginatedCollections<TModel> Create<TModel>(IQueryable<TModel> query, int currentPage, int pageSize) where TModel : class
        {
            var modelCount = query.Count();
            var pagesCount = (int)Math.Ceiling((double)modelCount / pageSize);
            var skip = (currentPage - 1) * pageSize;

            return new PaginatedCollections<TModel> { CurrentPage = currentPage, PagesCount = pagesCount, PageSize = pageSize };
        }


    }

    public static class QueryableExtensions
    {
        public static IPaginatedCollections<T> AsPaginatedQuery<T>(this IQueryable<T> query, int currentPage, int pageSize) where T : class
        {
            return PaginatedCollections<T>.Create<T>(query, currentPage, pageSize);
        }
    }
}