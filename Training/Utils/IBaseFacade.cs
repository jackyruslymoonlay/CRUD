using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Training.Utils
{
    public interface IBaseFacade<TModel>
    {
        ICollection<TModel> Read();
        TModel ReadById(int id);
        int DeleteById(int id);
        int Create(TModel model);
        int UpdateById(TModel newModel);
    }
}
