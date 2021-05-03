using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsExampleOOP_Tjedan1Dan1.Animals.Mammals
{
    class Dog : Mammal, IAnimalBehaviour
    {
        public void MakeNoise()
        {
            this.MakeSound();
        }

        protected override void EatFood()
        {
            Console.WriteLine("Dog ate some food from a bowl");
        }

        protected override void MakeSound()
        {
            Console.WriteLine("Bark, bark, bark!!");
        }

        protected override void Move()
        {
            throw new NotImplementedException("Dog walked a few meters");
        }

        void IAnimalBehaviour.EatFood()
        {
            this.EatFood();
        }
    }
}
