using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsExampleOOP_Tjedan1Dan1.Polymorphism
{
    class DogPolyExample : AnimalPolyExample
    {
        public void MakeSound()
        {
            Console.WriteLine("Bark, bark");
        }

        public override void Move()
        {
            Console.WriteLine("Dog moved.");
        }


    }
}
