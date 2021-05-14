using Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class Player : IPlayer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICity PlaceOfResidence { get; set; }

        public Player(Guid id, string name, string surname, City placeOfResidence)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.PlaceOfResidence = placeOfResidence;
        }

    }
}