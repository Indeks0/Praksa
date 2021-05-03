using AnimalsExampleOOP_Tjedan1Dan1.Animals.Mammals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsExampleOOP_Tjedan1Dan1
{
    class DogTrainer
    {
        private List<Dog> DogsToTrain = new List<Dog>();

        public DogTrainer(Dog dog)
        {
            this.DogsToTrain.Add(dog);
        }

        public DogTrainer(List<Dog> dogs)
        {
            foreach(Dog dog in dogs)
            {
                this.DogsToTrain.Add(dog);
            }
        }

        public void AddDog(Dog dog)
        {
            this.DogsToTrain.Add(dog);
        }

        public void TrainDogs()
        {
            if(this.DogsToTrain.Count > 0)
            {
                foreach (Dog dog in this.DogsToTrain.ToList()) //this.DogsToTrain.ToList() napravi kopiju liste pa nema problema kod uklanjanja elemenata
                {
                    if (!dog.Train()) //ako je pas istreniran dovoljno ukloni ga
                    {
                        this.DogsToTrain.Remove(dog);
                    }
                }
            }
        }
    }
}
