using System;
using System.Collections.Generic;
using System.Text;

namespace CRUDTest.Interface
{
    public interface IDataUtil<TModel>
    {
        TModel GetModelData();
    }
}
