using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Training.Interface;

namespace Training.ViewModels
{
    public class BuyerViewModel : IViewModel, IValidatableObject
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Code))
            {
                yield return new ValidationResult("Code is required", new List<string>() { "Code" });
            }

            if (string.IsNullOrWhiteSpace(Name))
            {
                yield return new ValidationResult("Name is required", new List<string>() { "Name" });
            }
        }
    }
}
