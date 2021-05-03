using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsExampleOOP_Tjedan1Dan1.Animals.Birds
{
    class Pigeon : Bird, IAnimalBehaviour
    {
        public void EatFood()
        {
            Console.WriteLine("Pigeon ate a worm");
        }

        public void MakeNoise()
        {
            this.MakeSound();
        }

        protected override void Fly()
        {
            Console.WriteLine("Pigeon used his wings to fly");
        }

        protected override void MakeSound()
        {
            Console.WriteLine("Coo, coo, coo!!");
        }

        protected override void Move()
        {
            Console.WriteLine("Pigeon flew a short distance");
        }
    }
}
