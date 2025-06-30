using System;
using MagicVilla_API.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Datos;

public class AplicationDBContext : DbContext
{
    public DbSet<Villa> Villas { get; set; }
    public DbSet<VillaNumber> villaNumbers { get; set; }
    public AplicationDBContext(DbContextOptions<AplicationDBContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        DateTime now = DateTime.Now;
        DateOnly get_date = new DateOnly(now.Year, now.Month, now.Day);

        modelBuilder.Entity<Villa>().HasData(
            new Villa()
            {
                Id = 1,
                Name = "Villa Real",
                Details = "Detalles de la Villa",
                ImageUrl = "",
                Occupants = 5,
                SquareMeters = 50,
                Tariff = 200,
                Amenity = "",
                CreatedDate = get_date,
                UpdateDate = get_date
            },
            new Villa()
            {
                Id = 2,
                Name = "Premium Vista a la Piscina",
                Details = "Detalles de la Villa",
                ImageUrl = "",
                Occupants = 4,
                SquareMeters = 40,
                Tariff = 150,
                Amenity = "",
                CreatedDate = get_date,
                UpdateDate = get_date
            }
        );
    }
}
