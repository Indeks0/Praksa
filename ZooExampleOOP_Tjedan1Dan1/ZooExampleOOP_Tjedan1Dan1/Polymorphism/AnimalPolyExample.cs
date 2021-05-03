using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsExampleOOP_Tjedan1Dan1.Polymorphism
{
    class AnimalPolyExample
    {

        public void MakeSound()
        {
            Console.WriteLine("Animal sound...");
        }

        public virtual void Move()
        {
            Console.WriteLine("Animal moved");
        }
    }
}
