using CRUDTest.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Training.Models;
using Training.ViewModels;

namespace CRUDTest.DataUtils
{
    public class BuyerDataUtil : IDataUtil<Buyer>
    {
        public Buyer GetModelData()
        {
            return new Buyer()
            {
                Id = 0,
                Code = "TestCode",
                Name = "TestName"
            };
        }

        public BuyerViewModel GetViewModelData()
        {
            return new BuyerViewModel()
            {
                Id = 1,
                Code = "TestCode",
                Name = "TestName"
            };
        }
    }
}
