using Microsoft.EntityFrameworkCore;
using SampleMvc.Identity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleMvc.Data
{
    public partial class AppDbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<UserClaim> UserClaims { get; set; }

        public DbSet<UserLogin> UserLogins { get; set; }

        public DbSet<UserToken> UserTokens { get; set; }
    }
}
