using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewDog.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Owner> Owners { get; set; }
    }
}
