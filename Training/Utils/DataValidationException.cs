using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Training.Utils
{
    public class DataValidationException : Exception
    {
        public List<ValidationResult> validationResults { get;set;}
        public DataValidationException(List<ValidationResult> validationResults, string message) : base (message)
        {
            this.validationResults = validationResults;
        }
    }
}
