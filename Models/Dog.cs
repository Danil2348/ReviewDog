using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewDog.Models
{
    public class Dog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<DogOwner> DogOwners { get; set; }
        public ICollection<DogBreed> DogBreeds { get; set; }

    }
}
