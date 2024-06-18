﻿using Microsoft.EntityFrameworkCore;
using PetSocialNetwork.Models;

namespace PetSocialNetwork.Infrastructure.DataEntityFramework
{
    public class AppDbContext : DbContext
    {
        DbSet<Account> Accounts => Set<Account>();
        DbSet<UserProfile> UserProfiles => Set<UserProfile>();
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

    }
}
