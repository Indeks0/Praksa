using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsExampleOOP_Tjedan1Dan1.Animals.Birds
{
    class Parrot : Bird, IAnimalBehaviour
    {
        private List<string> LearnedSentences = new List<string>();
        private int SentencesCount;
        private int NextWordToSay = 0;

        public Parrot()
        {
            this.LearnedSentences.Add("Hello there");
            this.LearnedSentences.Add("How Are you?");
            this.LearnedSentences.Add("Unlock the cage!!");
            this.LearnedSentences.Add("I'm clever. What, hey you!?!?");
            this.SentencesCount = LearnedSentences.Count();
        }

        protected override void Fly()
        {
            Console.WriteLine("Parrot is in cage, flying is not possible");
        }

        protected override void MakeSound()
        {
            Console.WriteLine(LearnedSentences[this.NextWordToSay]);
            HandleNextSentence();
        }

        private void HandleNextSentence() //Extracted method from MakeSound() KISS example
        {
            if (NextWordToSay == (SentencesCount - 1))
            {
                NextWordToSay = 0;
            }
            else
            {
                NextWordToSay++;
            }
        }

        public void LearnNewSentence(string sentence)
        {
            this.LearnedSentences.Add(sentence);
            this.SentencesCount = this.LearnedSentences.Count();
        }

        protected override void Move()
        {
            Console.WriteLine("Parrot walked a short distance in its cage.");
        }

        public void MakeNoise()
        {
            this.MakeSound();
        }

        public void EatFood()
        {
            Console.WriteLine("Parrot ate some bird food");
        }
    }
}
