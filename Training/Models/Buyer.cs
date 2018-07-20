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
    public class Buyer : BaseModel, IValidatableObject
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            ProjectDbContext dbContext = validationContext.GetService<ProjectDbContext>();
            if (dbContext.Buyers.Any(p => p.IsDeleted == false && p.Code == Code && p.Id != Id))
            {
                yield return new ValidationResult("Code is already exist", new List<string> { "Code" });
            }
        }
    }
}
