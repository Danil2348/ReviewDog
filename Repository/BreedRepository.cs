using ReviewDog.Data;
using ReviewDog.interfaces;
using ReviewDog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewDog.Repository
{
    public class BreedRepository: IBreedRepository
    {
        private readonly DataContext _dataContext;

        public BreedRepository(DataContext context)
        {
            _dataContext = context;
        }

        public bool BreedExists(int id)
        {
            return _dataContext.Breeds.Any(b => b.Id == id);
        }

        public bool CreateBreed(Breed breed)
        {
            _dataContext.Add(breed);
            return Save();
        }

        public bool DeleteBreed(Breed breed)
        {
            _dataContext.Remove(breed);
            
            return Save();
        }

        public Breed GetBreed(int id)
        {
            return _dataContext.Breeds.Where(b => b.Id == id).FirstOrDefault();
        }

        public ICollection<Breed> GetBreeds()
        {
            return _dataContext.Breeds.ToList();
        }

        public ICollection<Dog> GetDogsByBreed(int breedid)
        {
            return _dataContext.DogBreeds.Where(b => b.BreedId == breedid).Select(d => d.Dog).ToList();
        }

        public bool Save()
        {
            var saved=_dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateBreed(Breed breed)
        {
            _dataContext.Update(breed);
            return Save();
        }
    }
}
