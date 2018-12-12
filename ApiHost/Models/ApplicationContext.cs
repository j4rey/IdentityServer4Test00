﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiHost.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions opts) : base(opts)
        {
        }
        public DbSet<GrayScaleWebsite> GrayScaleWebsites { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
