using System;
using System.Collections.Generic;
using DA = System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Training.Models;
using Training.ViewModels;
using Training.Utils;
using Training.Interface;

namespace Training.Controllers
{
    [Produces("application/json")]
    [Route("api/Products")]
    public class ProductsController : BaseController<IProductFacade, Product, ProductViewModel>
    {
        public ProductsController(IServiceProvider serviceProvider, IMapper mapper)  : base(serviceProvider, mapper)
        {
        }
    }
}
