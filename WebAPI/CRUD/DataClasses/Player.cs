using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD.DataClasses
{
    public class Player
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public Player(string name, string surname)
        {
            this.Name = name;
            this.Surname = surname;
        }

        public override string ToString()
        {
            return Name + " " + Surname;
        }


    }
}