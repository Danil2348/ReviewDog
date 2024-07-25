using ReviewDog.Data;
using ReviewDog.interfaces;
using ReviewDog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewDog.Repository
{
    public class DogRepository: IDogRepository
    {
        private readonly DataContext _dataContext;
        public DogRepository(DataContext context)
        {
            _dataContext = context;
        }

        public bool CreateDog(Dog dog, int ownerId, int breedId)
        {
            var dogBreed = new DogBreed
            {
                Dog = dog,
                Breed = _dataContext.Breeds.Where(b => b.Id == breedId).FirstOrDefault()
            };
            _dataContext.Add(dogBreed);

            var dogOwner = new DogOwner
            {
                Dog = dog,
                Owner = _dataContext.Owners.Where(o => o.Id == ownerId).FirstOrDefault()
            };
            _dataContext.Add(dogOwner);

            _dataContext.Add(dog);
            return Save();
        }

        public bool DeleteDog(Dog dog)
        {
            _dataContext.Remove(dog);
            return Save();
        }

        public bool DogExists(int dogid)
        {
            return _dataContext.Dogs.Any(d => d.Id == dogid);
        }

        public Dog GetDog(int id)
        {
            return _dataContext.Dogs.Where(d => d.Id == id).FirstOrDefault();
        }

        public Dog GetDog(string name)
        {
            return _dataContext.Dogs.Where(d => d.Name == name).FirstOrDefault();
        }

        public decimal GetDogRating(int dogid)
        {
            var review = _dataContext.Reviews.Where(r => r.Dog.Id == dogid);
            if (review.Count() <= 0)
                return 0;
            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

        public ICollection<Dog> GetDogs()
        {
            return _dataContext.Dogs.OrderBy(d => d.Id).ToList();
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateDog(Dog dog, int ownerId, int breedId)
        {
            _dataContext.Update(dog);
            return Save();
        }
    }
}
