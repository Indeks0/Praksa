using AnimalsExampleOOP_Tjedan1Dan1.Animals.Mammals;
using AnimalsExampleOOP_Tjedan1Dan1.Generics;
using AnimalsExampleOOP_Tjedan1Dan1.Polymorphism;
using System;

namespace AnimalsExampleOOP_Tjedan1Dan1
{
    class Program
    {
        static void Main(string[] args)
        {
            //--------------------------------------------------
            //testiranje sucelja 
            AnimalsGroup animalsInAYard = new AnimalsGroup();

            animalsInAYard.MakeALotOfNoise();
            Console.WriteLine("\n");
            animalsInAYard.MakeALotOfNoise();
            Console.WriteLine("\n");
            animalsInAYard.MakeALotOfNoise();
            //--------------------------------------------------

            Console.WriteLine("\n");

            //--------------------------------------------------
            //testiranje agregacije klase Dog u klasi DogTrainer

            Dog rex = new Dog();
            Dog zeus = new Dog();

            DogTrainer trainer = new DogTrainer(rex);

            for (int i = 0; i < 11; i++)
                trainer.TrainDogs();

            Console.WriteLine("\n");

            trainer.AddDog(zeus);

            for (int i = 0; i < 10; i++)
                trainer.TrainDogs();
            //--------------------------------------------------

            Console.WriteLine("\n");

            //--------------------------------------------------
            //testiranje generic klase GenericDataStorage
            GenericDataStorage<int> storedInteger = new GenericDataStorage<int>();
            GenericDataStorage<string> storedString = new GenericDataStorage<string>();

            storedInteger.StoreData(1111);
            storedString.StoreData("TestString");

            Console.WriteLine(storedInteger.StoredData);
            Console.WriteLine(storedString.StoredData);

            //--------------------------------------------------

            Console.WriteLine("\n");

            //--------------------------------------------------
            //testiranje polimorfizma (folder Polymorphism)
            AnimalPolyExample polyDogOne = new DogPolyExample();
            polyDogOne.Move();
            polyDogOne.MakeSound();

            Console.WriteLine("\n");

            DogPolyExample polyDogTwo = new DogPolyExample();
            polyDogTwo.Move();
            polyDogTwo.MakeSound();

            //--------------------------------------------------

        }
    }
}
