﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleSecurity.Entities;

namespace SimpleSecurity.Repositories
{
    public class SecurityContext : DbContext
    {
        public SecurityContext()
            : base("PhotoManagerDB_3_layer")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

}
