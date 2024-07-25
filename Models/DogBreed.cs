using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewDog.Models
{
    public class DogBreed
    {
        public int DogId { get; set; }
        public int BreedId { get; set; }
        public Dog Dog { get; set; }
        public Breed Breed { get; set; }
    }
}
