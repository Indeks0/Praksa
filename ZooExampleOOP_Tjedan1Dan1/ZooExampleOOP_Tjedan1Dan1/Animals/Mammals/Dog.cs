using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsExampleOOP_Tjedan1Dan1.Animals.Mammals
{
    class Dog : Mammal, IAnimalBehaviour
    {
        private int LevelOfTraining = 0;

        public bool IsTrainedEnough = false;

        public void AskToSitDown()
        {
            if(this.LevelOfTraining == 10)
                Console.WriteLine("Dog sits down.");

            Console.WriteLine("Dog refuses to sit down, he is not trained enough");
        }

        public void MakeNoise()
        {
            this.MakeSound();
        }

        public bool Train()
        {
            if(IsTrainedEnough == true)
            {
                Console.WriteLine("This dog is trained enough, no need to train him anymore.");
                return false;
            }
            Console.WriteLine("Dog training...");
            LevelOfTraining++;
            Console.WriteLine($"Dog level of training increased to {this.LevelOfTraining}");
            if (LevelOfTraining == 10)
                this.IsTrainedEnough = true; // Na sljedecem treningu ce se vidjeti da je istreniran dovoljno
            return true;
        }

        protected override void EatFood()
        {
            Console.WriteLine("Dog ate some food.");
        }

        protected override void MakeSound()
        {
            Console.WriteLine("Woof woof");
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
