using Microsoft.EntityFrameworkCore;
using PetSocialNetwork.Domain.Membership;
using PetSocialNetwork.Models;

namespace PetSocialNetwork.Data;


public class PetSocialNetworkDbContext : DbContext
{
    public PetSocialNetworkDbContext(DbContextOptions<PetSocialNetworkDbContext> options) : 
        base(options) { }

    
    public DbSet<User> Users { get; init; }

    public DbSet<UserProfile> UserProfiles { get; init; }
    protected override void OnModelCreating(ModelBuilder modelBuilder) => 
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
}