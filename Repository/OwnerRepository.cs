using ReviewDog.Data;
using ReviewDog.interfaces;
using ReviewDog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewDog.Repository
{
    public class OwnerRepository: IOwnerRepository
    {
        private readonly DataContext _dataContext;

        public OwnerRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool CreateOwner(Owner owner)
        {
            _dataContext.Add(owner);
            return Save();
        }

        public bool DeleteOwner(Owner owner)
        {
            _dataContext.Remove(owner);
            return Save();
        }

        public ICollection<Dog> GetDogByOwner(int ownerId)
        {
            return _dataContext.DogOwners.Where(o => o.Owner.Id == ownerId).Select(d => d.Dog).ToList();
        }

        public Owner GetOwner(int Id)
        {
            return _dataContext.Owners.Where(o => o.Id == Id).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnerOfaDog(int dogId)
        {
            return _dataContext.DogOwners.Where(d => d.Dog.Id == dogId).Select(o => o.Owner).ToList();
        }

        public ICollection<Owner> GetOwners()
        {
            return _dataContext.Owners.ToList();
        }

        public bool OwnerExists(int ownerId)
        {
            return _dataContext.Owners.Any(o => o.Id==ownerId);
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateOwner(Owner owner)
        {
            _dataContext.Update(owner);
            return Save();
        }
    }
}
