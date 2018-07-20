using CRUDTest.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Training.Models;
using Training.ViewModels;

namespace CRUDTest.DataUtils
{
    public class ProductDataUtil : IDataUtil<Product>
    {
        public Product GetModelData()
        {
            return new Product()
            {
                Id = 0,
                Code = "TestCode",
                Name = "TestName",
                Currency = "TestCurrency",
                Price = 100000.25,
                UOM = "TestUOM",
                Tags = "TestTags",
                Description = "TestDescription"
            };
        }

        public ProductViewModel GetViewModelData()
        {
            return new ProductViewModel()
            {
                Id = 1,
                Code = "TestCode",
                Name = "TestName",
                Currency = "TestCurrency",
                Price = 100000.25,
                UOM = "TestUOM",
                Tags = "TestTags",
                Description = "TestDescription"
            };
        }
    }
}
