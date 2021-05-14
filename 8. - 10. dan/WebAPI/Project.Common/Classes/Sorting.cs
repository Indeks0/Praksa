using Project.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common.Classes
{
    public class Sorting : ISorting
    {
        public string AttributeToSort { get; set; }
        public string SortingOrder { get; set; }

        public Sorting(string attributeToSort, string sortingOrder)
        {
            AttributeToSort = attributeToSort;
            SortingOrder = sortingOrder;
        }
    }
}
