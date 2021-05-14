using Project.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common.Classes
{
    public class CustomDBQuery : ICustomDBQuery
    {
        public IFiltering Filtering { get; set; }
        public ISorting Sorting { get; set; }
        public IPaging Paging { get; set; }

        public CustomDBQuery(Filtering filtering, Sorting sorting, Paging paging)
        {
            Filtering = filtering;
            Sorting = sorting;
            Paging = paging;
        }
    }
}
