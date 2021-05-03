using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsExampleOOP_Tjedan1Dan1.Animals.Mammals
{
    class Cat : Mammal, IAnimalBehaviour
    {
        public void MakeNoise()
        {
            this.MakeSound();
        }

        protected override void EatFood()
        {
            Console.WriteLine("Cat ate some food from a bowl");
        }

        protected override void MakeSound()
        {
            Console.WriteLine("Meow, meow, meow.");
        }

        protected override void Move()
        {
            Console.WriteLine("Cat ran a short distance.");
        }

        void IAnimalBehaviour.EatFood()
        {
            this.EatFood();
        }
    }
}
