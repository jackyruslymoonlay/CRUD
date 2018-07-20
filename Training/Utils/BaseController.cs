using System;
using System.Collections.Generic;
using DA = System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Training.Interface;

namespace Training.Utils
{
    public class BaseController<TFacade, TModel, TViewModel> : Controller
        where TFacade : IBaseFacade<TModel>
        where TModel : BaseModel, IValidatableObject
        where TViewModel : class, IViewModel, IValidatableObject
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly IMapper _mapper;
        protected readonly TFacade _facade;

        public BaseController(IServiceProvider serviceProvider, IMapper mapper)  //TModelFacade facade
        {
            _serviceProvider = serviceProvider;
            _facade = serviceProvider.GetService<TFacade>();
            _mapper = mapper;
        }
        // GET: api/TModels
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_mapper.Map<List<TViewModel>>(_facade.Read()));
        }

        // GET: api/TModels/5
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            TModel model = _facade.ReadById(id);
            if (model == null)
            {
                return NotFound("Data not found");
            }
            TViewModel viewModel = _mapper.Map<TViewModel>(model);
            return Ok(viewModel);
        }

        private void Validate<T>(T instance)
            where T : IValidatableObject
        {
            DA.ValidationContext validationContext = new DA.ValidationContext(instance, _serviceProvider, null);
            List<ValidationResult> validationResults = instance.Validate(validationContext).ToList();
            if (validationResults.Count() > 0)
            {
                throw new DataValidationException(validationResults, "Data does not pass validation");
            }
        }

        // POST: api/TModels
        [HttpPost]
        public IActionResult Post([FromBody]TViewModel viewModel)
        {
            try
            {
                Validate(viewModel);

                TModel model = _mapper.Map<TModel>(viewModel);

                Validate(model);

                int result = _facade.Create(model);
                if (result > 0)
                {
                    return Created(String.Concat(Request.Path, "/", model.Id), String.Concat(result, " Row affected"));
                }
                else
                {
                    return StatusCode(500);
                }
            }
            catch (DataValidationException e)
            {
                return BadRequest(e.validationResults);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Something went wrong: " + e.Message);
            }
        }

        // PUT: api/TModels/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]TViewModel viewModel)
        {
            try
            {
                if (id != viewModel.Id)
                {
                    return BadRequest("Invalid data");
                }

                Validate(viewModel);

                TModel model = _facade.ReadById(id);
                if (model == null)
                    return NotFound("Data not found");

                Validate(_mapper.Map(viewModel, model));

                int result = _facade.UpdateById(model);
                if (result > 0)
                {
                    return NoContent();
                }
                else
                {
                    return StatusCode(500);
                }
            }
            catch (DataValidationException e)
            {
                return BadRequest(e.validationResults);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Something went wrong: " + e.Message);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                int result = _facade.DeleteById(id);
                if (result == 0)
                {
                    return NotFound("Data not found");
                }

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, "Something went wrong: " + e.Message);
            }
        }
    }
}
