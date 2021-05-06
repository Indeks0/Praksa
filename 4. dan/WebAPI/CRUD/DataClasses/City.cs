using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD.DataClasses
{
    public class City
    {
        public int ZipCode { get; set; }
        public string Name { get; set; }

        public City(int zipCode, string name)
        {
            this.ZipCode = zipCode;
            this.Name = name;
        }
    }
}