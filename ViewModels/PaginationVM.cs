using System.Collections.Generic;

namespace BackFinal.ViewModels
{
    public class PaginationVM<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<T> Items { get; set; }

        public PaginationVM(int currentPage, int pageSize, List<T> items)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            Items = items;
        }
    }
}

