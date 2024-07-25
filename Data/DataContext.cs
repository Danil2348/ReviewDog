using Microsoft.EntityFrameworkCore;
using ReviewDog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewDog.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Breed> Breeds { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Dog> Dogs { get; set; }
        public DbSet<DogBreed> DogBreeds { get; set; }
        public DbSet<DogOwner> DogOwners { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DogBreed>()
                .HasKey(db => new { db.BreedId, db.DogId });
            builder.Entity<DogBreed>()
                .HasOne(d => d.Dog)
                .WithMany(db => db.DogBreeds)
                .HasForeignKey(b => b.BreedId);
            builder.Entity<DogBreed>()
                .HasOne(b => b.Breed)
                .WithMany(db => db.DogBreeds)
                .HasForeignKey(d => d.DogId);

            builder.Entity<DogOwner>()
                .HasKey(dow => new {dow.OwnerId, dow.DogId });
            builder.Entity<DogOwner>()
                .HasOne(d => d.Dog)
                .WithMany(dow => dow.DogOwners)
                .HasForeignKey(o => o.OwnerId);
            builder.Entity<DogOwner>()
                .HasOne(o => o.Owner)
                .WithMany(dow => dow.DogOwners)
                .HasForeignKey(d => d.DogId);

            builder.Entity<Owner>()
                .HasOne(o => o.Country)
                .WithMany(c => c.Owners)
                .HasForeignKey(o => o.CountryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Review>()
               .HasOne(o => o.Reviewer)
               .WithMany(c => c.Reviews)
               .HasForeignKey(o => o.ReviewerId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Review>()
               .HasOne(o => o.Dog)
               .WithMany(c => c.Reviews)
               .HasForeignKey(o => o.DogId)
               .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
