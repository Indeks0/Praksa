using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsExampleOOP_Tjedan1Dan1.Animals.Mammals
{
    class Sheep : Mammal, IAnimalBehaviour
    {
        public void MakeNoise()
        {
            this.MakeSound();
        }

        protected override void EatFood()
        {
            Console.WriteLine("Sheep ate some food.");
        }

        protected override void MakeSound()
        {
            Console.WriteLine("*SHEEP SOUND*");
        }

        protected override void Move()
        {
            throw new NotImplementedException("Sheep walked a few meters");
        }

        void IAnimalBehaviour.EatFood()
        {
            this.EatFood();
        }
    }
}
