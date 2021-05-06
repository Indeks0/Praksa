using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD.DataClasses
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public City PlaceOfResidence { get; set; }

        public Player(int id, string name, string surname, City placeOfResidence)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.PlaceOfResidence = placeOfResidence;
        }



    }
}