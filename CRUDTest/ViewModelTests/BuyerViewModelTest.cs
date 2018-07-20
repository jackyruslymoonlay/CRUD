using CRUDTest.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Training.ViewModels;
using Xunit;

namespace CRUDTest.ViewModelTests
{
    public class BuyerViewModelTest : BasicViewModelTest<BuyerViewModel>
    {
        private static DataUtils.BuyerDataUtil buyerDataUtil = new DataUtils.BuyerDataUtil();
        protected override BuyerViewModel GetTestData()
        {
            return buyerDataUtil.GetViewModelData();
        }
    }
}
