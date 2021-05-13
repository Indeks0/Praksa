using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Common
{
    public interface ICity
    {
        int ZipCode { get; set; }
        string Name { get; set; }
    }
}
