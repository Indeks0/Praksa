using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Common
{
    public class PlayerSort
    {
        public int PageNumber { get; set; }
        public string FilterBy { get; set; }
        public string SortBy { get; set; }
        public string SortingOrder { get; set; }

        public PlayerSort(int pageNumber, string filterBy, string sortBy, string sortingOrder)
        {
            PageNumber = pageNumber;
            FilterBy = filterBy;
            SortBy = sortBy;
            SortingOrder = sortingOrder;
        }
    }
}
