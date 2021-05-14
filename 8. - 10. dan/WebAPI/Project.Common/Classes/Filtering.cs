using Project.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common.Classes
{
    public class Filtering : IFiltering
    {
        public string AttributeToFilter { get; set; }
        public string AttributeValue { get; set; }

        public Filtering(string attributeToFilter, string attributeValue)
        {
            AttributeToFilter = attributeToFilter;
            AttributeValue = attributeValue;
        }
    }
}
