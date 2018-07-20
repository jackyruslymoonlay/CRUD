using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training.Context;

namespace Training.Utils
{
    public class BaseFacade<TModel> : IBaseFacade<TModel>
        where TModel : BaseModel
    {
        private readonly ProjectDbContext _dbContext;
        public BaseFacade(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICollection<TModel> Read()
        {
            return _dbContext.Set<TModel>().ToList();
        }

        public TModel ReadById(int id)
        {
            return _dbContext.Set<TModel>().Find(id);
        }

        private void BeforeDelete(TModel model)
        {
            model.IsDeleted = true;
            model.DeletedBy = "Username";
            model.DeletedDate = DateTimeOffset.Now;
        }

        public int DeleteById(int id)
        {
            TModel model = _dbContext.Set<TModel>().Find(id);
            if (model == null)
                return 0;

            _dbContext.Set<TModel>().Update(model);
            return _dbContext.SaveChanges();
        }

        private void BeforeCreate(TModel model)
        {
            model.CreatedBy = "Username";
            model.CreatedDate = DateTimeOffset.Now;
        }

        public int Create(TModel model)
        {
            BeforeCreate(model);
            _dbContext.Add(model);
            return _dbContext.SaveChanges();
        }

        private void BeforeUpdate(TModel oldModel)
        {
            oldModel.UpdatedBy = "Username";
            oldModel.UpdatedDate = DateTimeOffset.Now;
        }

        public int UpdateById(TModel model)
        {
            BeforeUpdate(model);
            _dbContext.Set<TModel>().Update(model);
            return _dbContext.SaveChanges();
        }
    }
}