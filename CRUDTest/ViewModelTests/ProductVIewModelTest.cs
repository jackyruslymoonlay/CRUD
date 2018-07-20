using CRUDTest.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using Training.ViewModels;

namespace CRUDTest.ViewModelTests
{
    public class ProductViewModelTest : BasicViewModelTest<ProductViewModel>
    {
        private static DataUtils.ProductDataUtil productDataUtil = new DataUtils.ProductDataUtil();
        protected override ProductViewModel GetTestData()
        {
            return productDataUtil.GetViewModelData();
        }
    }
}
