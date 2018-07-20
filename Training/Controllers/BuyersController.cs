using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Training.Interface;
using Training.Models;
using Training.Utils;
using Training.ViewModels;

namespace Training.Controllers
{
    [Produces("application/json")]
    [Route("api/Buyers")]
    public class BuyersController : BaseController<IBuyerFacade, Buyer, BuyerViewModel>
    {
        public BuyersController(IServiceProvider serviceProvider, IMapper mapper) : base(serviceProvider, mapper)
        {
        }
    }
}
