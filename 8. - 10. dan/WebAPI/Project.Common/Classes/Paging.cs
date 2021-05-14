using Project.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common.Classes
{
    public class Paging : IPaging
    {
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }

        public Paging(int currentPage, int itemsPerPage)
        {
            CurrentPage = currentPage;
            ItemsPerPage = itemsPerPage;
        }
    }
}
