using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Common
{
    public interface IPlayer
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Surname { get; set; }
        ICity PlaceOfResidence { get; set; }
    }
}
