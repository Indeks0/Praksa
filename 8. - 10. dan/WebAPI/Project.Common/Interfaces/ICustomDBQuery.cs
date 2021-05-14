using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common.Interfaces
{
    public interface ICustomDBQuery
    {
        IFiltering Filtering { get; set; }
        IPaging Paging { get; set; }
        ISorting Sorting { get; set; }
    }
}
