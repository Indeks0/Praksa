using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Common
{
    public interface IPlayer
    {
        int Id { get; set; }
        string Name { get; set; }
        string Surname { get; set; }
        // IPlayer PlaceOfResidence { get; set; } //Ne mogu staviti Player zbog obostranog referenciranja
        // DI je rjesenje
    }
}
