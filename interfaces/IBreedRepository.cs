using ReviewDog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewDog.interfaces
{
    public interface IBreedRepository
    {
        ICollection<Breed> GetBreeds();
        Breed GetBreed(int id);
        ICollection<Dog> GetDogsByBreed(int breedid);
        bool BreedExists(int id);
        bool CreateBreed(Breed breed);
        bool UpdateBreed(Breed breed);
        bool DeleteBreed(Breed breed);
        bool Save();
    }
}
