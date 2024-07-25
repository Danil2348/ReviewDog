using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewDog.Models
{
    public class DogOwner
    {
        public int DogId { get; set; }
        public int OwnerId { get; set; }
        public Dog Dog { get; set; }
        public Owner Owner { get; set; }
    }
}
