using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Training.Context;

namespace CRUDTest.Utils
{
    public static class MemoryDbHelper
    {
        public static ProjectDbContext GetDB(string UniqueName)
        {
            DbContextOptionsBuilder<ProjectDbContext> optionsBuilder = new DbContextOptionsBuilder<ProjectDbContext>();
            optionsBuilder.UseInMemoryDatabase(UniqueName);

            return new ProjectDbContext(optionsBuilder.Options);
        }
    }
}
