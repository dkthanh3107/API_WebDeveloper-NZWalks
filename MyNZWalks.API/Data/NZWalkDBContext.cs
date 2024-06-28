using Microsoft.EntityFrameworkCore;
using MyNZWalks.API.Models.Domain;

namespace MyNZWalks.API.Data
{
    public class NZWalkDBContext : DbContext
    {
        public NZWalkDBContext(DbContextOptions<NZWalkDBContext> dbContextOptions) : base(dbContextOptions) 
        {

        }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Regions> Regions { get; set; }
        public DbSet<Walks> Walks { get; set; }
        public DbSet<Image> Images { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("84580491-59d9-403a-abc7-aa3e5d55c030"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("3c09e1ab-2d79-4fe6-830d-84a645b38d4e"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("8009e0ea-5788-4d46-ac72-a4664affb9f1"),
                    Name = "Hard"
                }
            };
            modelBuilder.Entity<Difficulty>().HasData(difficulties);
            var regions = new List<Regions>
            {
                new Regions
                {
                    Id = Guid.Parse("7f14bd2a-32f9-4fbf-b55c-607640a93b34"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageURL = "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Regions
                {
                    Id = Guid.Parse("31e513ad-ea02-45f2-8e14-ec15d71c1139"),
                    Name = "Northland",
                    Code = "NTL",
                    RegionImageURL = null
                },
                new Regions
                {
                    Id = Guid.Parse("3b2322cb-73a9-4820-aafa-a9ae8f0aed2e"),
                    Name = "Bay Of Plenty",
                    Code = "BOP",
                    RegionImageURL = null
                },
                new Regions
                {
                    Id = Guid.Parse("60dda257-b71d-4076-bb49-0640996de186"),
                    Name = "Wellington",
                    Code = "WGN",
                    RegionImageURL = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Regions
                {
                    Id = Guid.Parse("ef02783e-f79a-4703-b652-4710ebedabd0"),
                    Name = "Nelson",
                    Code = "NSN",
                    RegionImageURL = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Regions
                {
                    Id = Guid.Parse("5c2b6c29-20b3-4614-bcb2-21368ff4d956"),
                    Name = "Southland",
                    Code = "STL",
                    RegionImageURL = null
                },
            };
            modelBuilder.Entity<Regions>().HasData(regions);
        }
    }
}
