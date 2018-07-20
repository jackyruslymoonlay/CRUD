using CRUDTest.Interface;
using System;
using Training.Context;
using Training.Utils;
using Xunit;

namespace CRUDTest.Utils
{
    public abstract class BasicFacadeTest<TFacade, TModel, TDataUtil>
        where TFacade : IBaseFacade<TModel>
        where TModel : BaseModel
        where TDataUtil : IDataUtil<TModel>, new()
    {
        public const string ENTITY = "Facade";
        private static dynamic dataUtil = new TDataUtil();

        [Fact]
        public void Should_Success_Read_Data()
        {
            ProjectDbContext projectDbContext = MemoryDbHelper.GetDB(Helper.GetCurrentMethod(ENTITY));
            TFacade tFacade = (TFacade)Activator.CreateInstance(typeof(TFacade), projectDbContext);
            
            projectDbContext.Add(dataUtil.GetModelData());
            projectDbContext.Add(dataUtil.GetModelData());
            projectDbContext.SaveChanges();

            Assert.Equal(2, tFacade.Read().Count);
        }

        [Fact]
        public void Should_Success_Read_Data_By_Id()
        {
            ProjectDbContext projectDbContext = MemoryDbHelper.GetDB(Helper.GetCurrentMethod(ENTITY));
            TFacade tFacade = (TFacade)Activator.CreateInstance(typeof(TFacade), projectDbContext);

            TModel model = dataUtil.GetModelData();
            tFacade.Create(model);
            dynamic result = tFacade.ReadById(model.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_Create_Data()
        {
            ProjectDbContext projectDbContext = MemoryDbHelper.GetDB(Helper.GetCurrentMethod(ENTITY));
            TFacade tFacade = (TFacade)Activator.CreateInstance(typeof(TFacade), projectDbContext);

            Assert.NotEqual(0, tFacade.Create(dataUtil.GetModelData()));
        }

        [Fact]
        public void Should_Success_Update_Data()
        {
            ProjectDbContext projectDbContext = MemoryDbHelper.GetDB(Helper.GetCurrentMethod(ENTITY));
            TFacade tFacade = (TFacade)Activator.CreateInstance(typeof(TFacade), projectDbContext);

            TModel model = dataUtil.GetModelData();
            tFacade.Create(model);

            Assert.NotEqual(0, tFacade.UpdateById(model));
        }

        [Fact]
        public void Should_Success_Delete_Data()
        {
            ProjectDbContext projectDbContext = MemoryDbHelper.GetDB(Helper.GetCurrentMethod(ENTITY));
            TFacade tFacade = (TFacade)Activator.CreateInstance(typeof(TFacade), projectDbContext);

            TModel model = dataUtil.GetModelData();
            tFacade.Create(model);

            Assert.NotEqual(0, tFacade.DeleteById(model.Id));
        }
    }
}
