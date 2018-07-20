using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Training.Interface;

namespace Training.ViewModels
{
    public class ProductViewModel : IViewModel, IValidatableObject
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string UOM { get; set; }
        public string Currency { get; set; }
        public double? Price { get; set; }
        public string Tags { get; set; }
        public string Description { get; set; }

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

            if (string.IsNullOrWhiteSpace(UOM))
            {
                yield return new ValidationResult("UOM is required", new List<string>() { "UOM" });
            }

            if (string.IsNullOrWhiteSpace(Currency))
            {
                yield return new ValidationResult("Currency is required", new List<string>() { "Currency" });
            }

            if (Price == null || Price == 0)
            {
                yield return new ValidationResult("Price is required", new List<string>() { "Price" });
            }
        }
    }
}
