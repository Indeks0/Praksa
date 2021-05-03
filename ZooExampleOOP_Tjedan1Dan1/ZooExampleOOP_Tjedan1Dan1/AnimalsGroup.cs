using AnimalsExampleOOP_Tjedan1Dan1.Animals.Birds;
using AnimalsExampleOOP_Tjedan1Dan1.Animals.Mammals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsExampleOOP_Tjedan1Dan1
{
    class AnimalsGroup
    {
        private List<IAnimalBehaviour> Animals = new List<IAnimalBehaviour>();

        public AnimalsGroup()
        {
            this.Animals.Add(new Parrot());
            this.Animals.Add(new Pigeon());
            this.Animals.Add(new Dog());
            this.Animals.Add(new Cat());
        }

        public void AddAnimal(IAnimalBehaviour animal)
        {
            this.Animals.Add(animal);
        }

        public void MakeALotOfNoise()
        {
            foreach(IAnimalBehaviour animal in this.Animals)
            {
                animal.MakeNoise();
            }
        }
    }
}
