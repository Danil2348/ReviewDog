using ReviewDog.Data;
using ReviewDog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewDog
{
    public class Seed
    {
        private readonly DataContext dataContext;

        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.DogOwners.Any())
            {
                List<DogOwner> dogOwners = new List<DogOwner>
                {
                    new DogOwner
                    {
                        Dog=new Dog
                        {
                            Name="Барсик",
                            BirthDay=new DateTime(2014,5,15),
                            DogBreeds=new List<DogBreed>()
                            {
                                new DogBreed
                                {
                                    Breed=new Breed
                                    {
                                        Title="Чихуа хуа"
                                    }
                                }
                            },
                            Reviews=new List<Review>()
                            {
                                new Review {
                                    Title="Барсик",
                                    Text="хорошая собака",
                                    Reviewer=new Reviewer{FirstName="Ксюша", LastName="Васильевна"},
                                    Rating=5
                                },
                                new Review {
                                    Title="Барсик",
                                    Text="прикольная собака",
                                    Reviewer=new Reviewer{FirstName="Адольф", LastName="гитлкер"},
                                    Rating=4
                                },
                            }
                        },
                        Owner = new Owner
                        {
                            Name="Jack",
                            Address="Щорса 8",
                            Country=new Country
                            {
                                Title="Россия"
                            }
                        }
                    },
                    new DogOwner
                    {
                        Dog=new Dog
                        {
                            Name="Ихвильнихт",
                            BirthDay=new DateTime(1941,6,22),
                            DogBreeds=new List<DogBreed>()
                            {
                                new DogBreed
                                {
                                    Breed=new Breed
                                    {
                                        Title="Немецкая  овчарка"
                                    }
                                }
                            },
                            Reviews=new List<Review>()
                            {
                                new Review {
                                    Title="Ихвильнихт",
                                    Text="хорошая собака",
                                    Reviewer=new Reviewer{FirstName="Вася", LastName="Васильевна"},
                                    Rating=3
                                },
                                new Review {
                                    Title="Ихвильнихт",
                                    Text="прикольная собака",
                                    Reviewer=new Reviewer{FirstName="Соня", LastName="гитлкер"},
                                    Rating=5
                                },
                            }
                        },
                        Owner = new Owner
                        {
                            Name="Джон",
                            Address="Токарей 3",
                            Country=new Country
                            {
                                Title="Россия"
                            }
                        }
                    },

                };
                dataContext.DogOwners.AddRange(dogOwners);
                dataContext.SaveChanges();
            }
        }
    }
}
