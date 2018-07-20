using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Training.Context;
using Training.Utils;

namespace Training.Models
{
    public class Product : BaseModel, IValidatableObject
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string UOM { get; set; }
        public string Currency { get; set; }
        public double Price { get; set; }
        public string Tags { get; set; }
        public string Description { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            ProjectDbContext dbContext = validationContext.GetService<ProjectDbContext>();
            if (dbContext.Products.Any(p => p.IsDeleted == false && p.Code == Code && p.Id != Id))
            {
                yield return new ValidationResult("Code is already exist", new List<string> { "Code" });
            }
        }
    }
}
