using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Xunit;

namespace CRUDTest.Utils
{
    public abstract class BasicViewModelTest<TViewModel>
        where TViewModel : IValidatableObject, new()
    {
        protected abstract TViewModel GetTestData();

        [Fact]
        public void Should_Return_Error_Message_Validate_View_Model()
        {
            TViewModel buyerViewModel = new TViewModel();
            List<ValidationResult> validationResults = buyerViewModel.Validate(null).ToList();
            Assert.NotEmpty(validationResults);
        }

        [Fact]
        public void Should_Success_Validate_View_Model()
        {
            TViewModel buyerViewModel = GetTestData();
            List<ValidationResult> validationResults = buyerViewModel.Validate(null).ToList();
            Assert.Empty(validationResults);
        }
    }
}
