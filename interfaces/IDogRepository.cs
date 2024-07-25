using ReviewDog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewDog.interfaces
{
    public interface IDogRepository
    {
        ICollection<Dog> GetDogs();
        Dog GetDog(int id);
        Dog GetDog(string name);
        decimal GetDogRating(int dogid);
        bool DogExists(int dogid);
        bool CreateDog(Dog dog, int ownerId, int breedId);
        bool UpdateDog(Dog dog, int ownerId, int breedId);
        bool DeleteDog(Dog dog);
        bool Save();
    }
}
