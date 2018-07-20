using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training.Context;
using Training.Interface;
using Training.Models;
using Training.Utils;
using Training.ViewModels;

namespace Training.Facades
{
    public class ProductFacade : BaseFacade<Product>, IProductFacade
    {
        public ProductFacade(ProjectDbContext dbContext) : base(dbContext)
        {
        }
    }
}
