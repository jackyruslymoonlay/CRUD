using AutoMapper;
using CRUDTest.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using Training.Context;
using Training.Controllers;
using Training.Interface;
using Training.Models;
using Training.ViewModels;
using Xunit;

namespace CRUDTest.ControllerTests
{
    public class BuyersControllerTest
    {
        public const string ENTITY = "Buyer_Controller";
        private static DataUtils.BuyerDataUtil buyerDataUtil = new DataUtils.BuyerDataUtil();
        [Fact]
        public void Should_Success_Get_Data()
        {
            Mock<IBuyerFacade> mockBuyerFacade = new Mock<IBuyerFacade>();
            mockBuyerFacade.Setup(p => p.Read())
                .Returns(new List<Buyer>());
            
            Mock<IMapper> mockMapper = new Mock<IMapper>();
            mockMapper.Setup(p => p.Map<List<BuyerViewModel>>(It.IsAny<List<Buyer>>()))
                .Returns(new List<BuyerViewModel>());

            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider.Setup(p => p.GetService(typeof(IBuyerFacade)))
                .Returns(mockBuyerFacade.Object);

            BuyersController buyersController = new BuyersController(mockServiceProvider.Object, mockMapper.Object);

            Assert.NotNull(buyersController.Get());
        }

        [Fact]
        public void Should_Not_Success_Get_Data_By_Id()
        {
            Mock<IBuyerFacade> mockBuyerFacade = new Mock<IBuyerFacade>();
            mockBuyerFacade.Setup(p => p.ReadById(0))
                .Returns((Buyer)null);

            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider.Setup(p => p.GetService(typeof(IBuyerFacade)))
                .Returns(mockBuyerFacade.Object);

            BuyersController buyersController = new BuyersController(mockServiceProvider.Object, null);
            IActionResult result = buyersController.GetById(0);
            //Assert.Equal(HttpStatusCode.NotFound, result.GetType().GetProperty("StatusCode").GetValue(result,null));
            Assert.Equal(HttpStatusCode.NotFound, (HttpStatusCode)((NotFoundObjectResult)result).StatusCode);
        }

        [Fact]
        public void Should_Success_Get_Data_By_Id()
        {
            Mock<IBuyerFacade> mockBuyerFacade = new Mock<IBuyerFacade>();
            mockBuyerFacade.Setup(p => p.ReadById(It.IsAny<int>()))
                .Returns(new Buyer());

            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider.Setup(p => p.GetService(typeof(IBuyerFacade)))
                .Returns(mockBuyerFacade.Object);

            Mock<IMapper> mockMapper = new Mock<IMapper>();
            mockMapper.Setup(p => p.Map<List<BuyerViewModel>>(It.IsAny<List<Buyer>>()))
                .Returns(new List<BuyerViewModel>());

            BuyersController buyersController = new BuyersController(mockServiceProvider.Object, mockMapper.Object);
            IActionResult result = buyersController.GetById(1);
            //Assert.Equal(HttpStatusCode.NotFound, result.GetType().GetProperty("StatusCode").GetValue(result,null));
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)((OkObjectResult)result).StatusCode);
        }

        [Fact]
        public void Should_Success_Create_Data()
        {
            Mock<IBuyerFacade> mockBuyerFacade = new Mock<IBuyerFacade>();
            mockBuyerFacade.Setup(p => p.Create(It.IsAny<Buyer>()))
                .Returns(1);

            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider.Setup(p => p.GetService(typeof(IBuyerFacade))).Returns(mockBuyerFacade.Object);
            mockServiceProvider.Setup(p => p.GetService(typeof(ProjectDbContext))).Returns(MemoryDbHelper.GetDB(Helper.GetCurrentMethod(ENTITY)));

            Mock<IMapper> mockMapper = new Mock<IMapper>();
            mockMapper.Setup(p => p.Map<Buyer>(It.IsAny<BuyerViewModel>()))
                .Returns(buyerDataUtil.GetModelData());

            BuyersController buyersController = new BuyersController(mockServiceProvider.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            buyersController.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");

            IActionResult result = buyersController.Post(buyerDataUtil.GetViewModelData());
            Assert.Equal(HttpStatusCode.Created, (HttpStatusCode)((CreatedResult)result).StatusCode);
        }

        [Fact]
        public void Should_Return_500_Create_Data()
        {
            Mock<IBuyerFacade> mockBuyerFacade = new Mock<IBuyerFacade>();
            mockBuyerFacade.Setup(p => p.Create(It.IsAny<Buyer>()))
                .Returns(0);

            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider.Setup(p => p.GetService(typeof(IBuyerFacade))).Returns(mockBuyerFacade.Object);
            mockServiceProvider.Setup(p => p.GetService(typeof(ProjectDbContext))).Returns(MemoryDbHelper.GetDB(Helper.GetCurrentMethod(ENTITY)));

            Mock<IMapper> mockMapper = new Mock<IMapper>();
            mockMapper.Setup(p => p.Map<Buyer>(It.IsAny<BuyerViewModel>()))
                .Returns(buyerDataUtil.GetModelData());

            BuyersController buyersController = new BuyersController(mockServiceProvider.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            buyersController.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");

            BuyerViewModel viewModel = buyerDataUtil.GetViewModelData();
            IActionResult result = buyersController.Post(viewModel);
            Assert.Equal(HttpStatusCode.InternalServerError, (HttpStatusCode)((StatusCodeResult)result).StatusCode);
        }

        [Fact]
        public void Should_Return_Bad_Request_Create_Data()
        {
            ProjectDbContext projectDbContext = MemoryDbHelper.GetDB(Helper.GetCurrentMethod(ENTITY));
            Mock<IBuyerFacade> mockBuyerFacade = new Mock<IBuyerFacade>();
            mockBuyerFacade.Setup(p => p.Create(It.IsAny<Buyer>()))
                .Returns(0);

            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider.Setup(p => p.GetService(typeof(IBuyerFacade))).Returns(mockBuyerFacade.Object);
            mockServiceProvider.Setup(p => p.GetService(typeof(ProjectDbContext))).Returns(projectDbContext);

            Mock<IMapper> mockMapper = new Mock<IMapper>();
            mockMapper.Setup(p => p.Map<Buyer>(It.IsAny<BuyerViewModel>()))
                .Returns(buyerDataUtil.GetModelData());

            BuyersController buyersController = new BuyersController(mockServiceProvider.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            buyersController.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");

            //Add data for make it double
            projectDbContext.Add(buyerDataUtil.GetModelData());
            projectDbContext.SaveChanges();

            BuyerViewModel viewModel = buyerDataUtil.GetViewModelData();
            IActionResult result = buyersController.Post(viewModel);
            Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)((BadRequestObjectResult)result).StatusCode);
        }

        [Fact]
        public void Should_Success_Update_Data()
        {
            Mock<IBuyerFacade> mockBuyerFacade = new Mock<IBuyerFacade>();
            mockBuyerFacade.Setup(p => p.UpdateById(It.IsAny<Buyer>())).Returns(1);
            mockBuyerFacade.Setup(p => p.ReadById(It.IsAny<int>())).Returns(buyerDataUtil.GetModelData());

            Mock<IMapper> mockMapper = new Mock<IMapper>();
            mockMapper.Setup(p => p.Map(It.IsAny<BuyerViewModel>(), It.IsAny<Buyer>()))
                .Returns(buyerDataUtil.GetModelData());

            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider.Setup(p => p.GetService(typeof(IBuyerFacade))).Returns(mockBuyerFacade.Object);
            mockServiceProvider.Setup(p => p.GetService(typeof(ProjectDbContext))).Returns(MemoryDbHelper.GetDB(Helper.GetCurrentMethod(ENTITY)));

            BuyersController buyersController = new BuyersController(mockServiceProvider.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            buyersController.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");

            IActionResult result = buyersController.Put(1,buyerDataUtil.GetViewModelData());
            Assert.Equal(HttpStatusCode.NoContent, (HttpStatusCode)((NoContentResult)result).StatusCode);
        }

        [Fact]
        public void Should_Return_500_No_Update_Data()
        {
            Mock<IBuyerFacade> mockBuyerFacade = new Mock<IBuyerFacade>();
            mockBuyerFacade.Setup(p => p.UpdateById(It.IsAny<Buyer>())).Returns(0); // No update row
            mockBuyerFacade.Setup(p => p.ReadById(It.IsAny<int>())).Returns(buyerDataUtil.GetModelData());

            Mock<IMapper> mockMapper = new Mock<IMapper>();
            mockMapper.Setup(p => p.Map(It.IsAny<BuyerViewModel>(), It.IsAny<Buyer>()))
                .Returns(buyerDataUtil.GetModelData());

            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider.Setup(p => p.GetService(typeof(IBuyerFacade))).Returns(mockBuyerFacade.Object);
            mockServiceProvider.Setup(p => p.GetService(typeof(ProjectDbContext))).Returns(MemoryDbHelper.GetDB(Helper.GetCurrentMethod(ENTITY)));

            BuyersController buyersController = new BuyersController(mockServiceProvider.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            buyersController.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");

            IActionResult result = buyersController.Put(1, buyerDataUtil.GetViewModelData());
            Assert.Equal(HttpStatusCode.InternalServerError, (HttpStatusCode)((StatusCodeResult)result).StatusCode);
        }

        [Fact]
        public void Should_Return_500_Invalid_Update_Data()
        {
            Mock<IBuyerFacade> mockBuyerFacade = new Mock<IBuyerFacade>();
            mockBuyerFacade.Setup(p => p.UpdateById(It.IsAny<Buyer>())).Returns(1);
            mockBuyerFacade.Setup(p => p.ReadById(It.IsAny<int>())).Returns(buyerDataUtil.GetModelData());

            Mock<IMapper> mockMapper = new Mock<IMapper>();
            mockMapper.Setup(p => p.Map(It.IsAny<BuyerViewModel>(), It.IsAny<Buyer>()))
                .Returns(buyerDataUtil.GetModelData());

            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider.Setup(p => p.GetService(typeof(IBuyerFacade))).Returns(mockBuyerFacade.Object);
            mockServiceProvider.Setup(p => p.GetService(typeof(ProjectDbContext))).Returns(MemoryDbHelper.GetDB(Helper.GetCurrentMethod(ENTITY)));

            BuyersController buyersController = new BuyersController(mockServiceProvider.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            buyersController.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");

            // Id view model not same
            IActionResult result = buyersController.Put(0, buyerDataUtil.GetViewModelData());
            Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)((BadRequestObjectResult)result).StatusCode);
        }

        [Fact]
        public void Should_Return_Validate_Error_View_Update_Data()
        {
            Mock<IBuyerFacade> mockBuyerFacade = new Mock<IBuyerFacade>();
            mockBuyerFacade.Setup(p => p.UpdateById(It.IsAny<Buyer>())).Returns(0);
            mockBuyerFacade.Setup(p => p.ReadById(It.IsAny<int>())).Returns(buyerDataUtil.GetModelData());

            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider.Setup(p => p.GetService(typeof(IBuyerFacade))).Returns(mockBuyerFacade.Object);
            mockServiceProvider.Setup(p => p.GetService(typeof(ProjectDbContext))).Returns(MemoryDbHelper.GetDB(Helper.GetCurrentMethod(ENTITY)));

            Mock<IMapper> mockMapper = new Mock<IMapper>();
            mockMapper.Setup(p => p.Map<Buyer>(It.IsAny<BuyerViewModel>()))
                .Returns(buyerDataUtil.GetModelData());

            BuyersController buyersController = new BuyersController(mockServiceProvider.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            buyersController.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");

            BuyerViewModel viewModel = buyerDataUtil.GetViewModelData();
            viewModel.Code = ""; // ViewModel Invalid
            IActionResult result = buyersController.Put(1, viewModel);
            Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)((BadRequestObjectResult)result).StatusCode);
        }

        //[Fact]
        //public void Should_Return_Validate_Error_Model_Update_Data()
        //{
        //    ProjectDbContext projectDbContext = MemoryDbHelper.GetDB(Helper.GetCurrentMethod(ENTITY));
        //    Mock<IBuyerFacade> mockBuyerFacade = new Mock<IBuyerFacade>();
        //    mockBuyerFacade.Setup(p => p.UpdateById(It.IsAny<Buyer>())).Returns(0);
        //    mockBuyerFacade.Setup(p => p.ReadById(It.IsAny<int>())).Returns(buyerDataUtil.GetModelData());

        //    Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
        //    mockServiceProvider.Setup(p => p.GetService(typeof(IBuyerFacade))).Returns(mockBuyerFacade.Object);
        //    mockServiceProvider.Setup(p => p.GetService(typeof(ProjectDbContext))).Returns(projectDbContext);

        //    Mock<IMapper> mockMapper = new Mock<IMapper>();
        //    mockMapper.Setup(p => p.Map<Buyer>(It.IsAny<BuyerViewModel>()))
        //        .Returns(buyerDataUtil.GetModelData());

        //    BuyersController buyersController = new BuyersController(mockServiceProvider.Object, mockMapper.Object)
        //    {
        //        ControllerContext = new ControllerContext()
        //        {
        //            HttpContext = new DefaultHttpContext()
        //        }
        //    };
        //    buyersController.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");

        //    BuyerViewModel viewModel = buyerDataUtil.GetViewModelData();
        //    Buyer model = buyerDataUtil.GetModelData();
        //    projectDbContext.Add(model);

        //    viewModel.Id = 2;
        //    viewModel.Code = "TestCode2";
        //    projectDbContext.Add(model);
        //    projectDbContext.SaveChanges();

        //    IActionResult result = buyersController.Put(2, viewModel);
        //    Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)((BadRequestObjectResult)result).StatusCode);
        //}

        [Fact]
        public void Should_Return_Error_Update_Data()
        {
            Mock<IBuyerFacade> mockBuyerFacade = new Mock<IBuyerFacade>();
            mockBuyerFacade.Setup(p => p.UpdateById(It.IsAny<Buyer>())).Throws(new Exception("Error")); // Throw exception error when update
            mockBuyerFacade.Setup(p => p.ReadById(It.IsAny<int>())).Returns(buyerDataUtil.GetModelData());

            Mock<IMapper> mockMapper = new Mock<IMapper>();
            mockMapper.Setup(p => p.Map(It.IsAny<BuyerViewModel>(), It.IsAny<Buyer>()))
                .Returns(buyerDataUtil.GetModelData());

            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider.Setup(p => p.GetService(typeof(IBuyerFacade))).Returns(mockBuyerFacade.Object);
            mockServiceProvider.Setup(p => p.GetService(typeof(ProjectDbContext))).Returns(MemoryDbHelper.GetDB(Helper.GetCurrentMethod(ENTITY)));

            BuyersController buyersController = new BuyersController(mockServiceProvider.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            buyersController.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");

            IActionResult result = buyersController.Put(1, buyerDataUtil.GetViewModelData());
            Assert.Equal(HttpStatusCode.InternalServerError, (HttpStatusCode)((ObjectResult)result).StatusCode);
        }

        [Fact]
        public void Should_Return_Error_2_Update_Data()
        {
            Mock<IBuyerFacade> mockBuyerFacade = new Mock<IBuyerFacade>();
            mockBuyerFacade.Setup(p => p.UpdateById(It.IsAny<Buyer>())).Returns(0);
            mockBuyerFacade.Setup(p => p.ReadById(It.IsAny<int>())).Returns(buyerDataUtil.GetModelData());

            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider.Setup(p => p.GetService(typeof(IBuyerFacade))).Returns(mockBuyerFacade.Object);
            mockServiceProvider.Setup(p => p.GetService(typeof(ProjectDbContext))).Returns(MemoryDbHelper.GetDB(Helper.GetCurrentMethod(ENTITY)));

            Mock<IMapper> mockMapper = new Mock<IMapper>();
            mockMapper.Setup(p => p.Map<Buyer>(It.IsAny<BuyerViewModel>()))
                .Returns(buyerDataUtil.GetModelData());

            BuyersController buyersController = new BuyersController(mockServiceProvider.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            buyersController.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");

            BuyerViewModel viewModel = buyerDataUtil.GetViewModelData();
            IActionResult result = buyersController.Put(1,viewModel);
            Assert.Equal(HttpStatusCode.InternalServerError, (HttpStatusCode)((ObjectResult)result).StatusCode);
        }

        [Fact]
        public void Should_Return_Not_Found_Update_Data()
        {
            ProjectDbContext projectDbContext = MemoryDbHelper.GetDB(Helper.GetCurrentMethod(ENTITY));
            Mock<IBuyerFacade> mockBuyerFacade = new Mock<IBuyerFacade>();
            mockBuyerFacade.Setup(p => p.UpdateById(It.IsAny<Buyer>())).Returns(0);

            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider.Setup(p => p.GetService(typeof(IBuyerFacade))).Returns(mockBuyerFacade.Object);
            mockServiceProvider.Setup(p => p.GetService(typeof(ProjectDbContext))).Returns(projectDbContext);

            Mock<IMapper> mockMapper = new Mock<IMapper>();
            mockMapper.Setup(p => p.Map<Buyer>(It.IsAny<BuyerViewModel>()))
                .Returns(buyerDataUtil.GetModelData());

            BuyersController buyersController = new BuyersController(mockServiceProvider.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            buyersController.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");

            //Add data for make it double
            projectDbContext.Add(buyerDataUtil.GetModelData());
            projectDbContext.SaveChanges();

            BuyerViewModel viewModel = buyerDataUtil.GetViewModelData();
            IActionResult result = buyersController.Put(1,viewModel);
            Assert.Equal(HttpStatusCode.NotFound, (HttpStatusCode)((NotFoundObjectResult)result).StatusCode);
        }

        [Fact]
        public void Should_Success_Delete_Data()
        {
            Mock<IBuyerFacade> mockBuyerFacade = new Mock<IBuyerFacade>();
            mockBuyerFacade.Setup(p => p.DeleteById(1)).Returns(1);
            
            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider.Setup(p => p.GetService(typeof(IBuyerFacade))).Returns(mockBuyerFacade.Object);
            mockServiceProvider.Setup(p => p.GetService(typeof(ProjectDbContext))).Returns(MemoryDbHelper.GetDB(Helper.GetCurrentMethod(ENTITY)));

            BuyersController buyersController = new BuyersController(mockServiceProvider.Object, null)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            buyersController.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");

            IActionResult result = buyersController.Delete(1);
            Assert.Equal(HttpStatusCode.NoContent, (HttpStatusCode)((NoContentResult)result).StatusCode);
        }

        [Fact]
        public void Should_Not_Found_Delete_Data()
        {
            Mock<IBuyerFacade> mockBuyerFacade = new Mock<IBuyerFacade>();
            mockBuyerFacade.Setup(p => p.DeleteById(1)).Returns(0);

            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider.Setup(p => p.GetService(typeof(IBuyerFacade))).Returns(mockBuyerFacade.Object);
            mockServiceProvider.Setup(p => p.GetService(typeof(ProjectDbContext))).Returns(MemoryDbHelper.GetDB(Helper.GetCurrentMethod(ENTITY)));

            BuyersController buyersController = new BuyersController(mockServiceProvider.Object, null)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            buyersController.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");

            IActionResult result = buyersController.Delete(1);
            Assert.Equal(HttpStatusCode.NotFound, (HttpStatusCode)((NotFoundObjectResult)result).StatusCode);
        }

        [Fact]
        public void Should_Error_Delete_Data()
        {
            Mock<IBuyerFacade> mockBuyerFacade = new Mock<IBuyerFacade>();
            mockBuyerFacade.Setup(p => p.DeleteById(1)).Throws(new Exception("Error"));

            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider.Setup(p => p.GetService(typeof(IBuyerFacade))).Returns(mockBuyerFacade.Object);
            mockServiceProvider.Setup(p => p.GetService(typeof(ProjectDbContext))).Returns(MemoryDbHelper.GetDB(Helper.GetCurrentMethod(ENTITY)));

            BuyersController buyersController = new BuyersController(mockServiceProvider.Object, null)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            buyersController.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");

            IActionResult result = buyersController.Delete(1);
            Assert.Equal(HttpStatusCode.InternalServerError, (HttpStatusCode)((ObjectResult)result).StatusCode);
        }
    }
}
