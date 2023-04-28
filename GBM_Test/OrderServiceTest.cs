using GBM_Stocks_Accounts_Infrastructure.Interfaces;
using GBM_Stocks_Core_Infrastructure.Interfaces;
using GBM_Stocks_Database.Domain;
using GBM_Stocks_Infrastructure.Implementations;
using GBM_Stocks_Infrastructure.Interfaces;
using GBM_Stocks_Orders_Core.Interfaces;
using GBM_Stocks_Orders_Core.Services;
using GBM_Stocks_Orders_Domain.Request;
using GBM_Stocks_Orders_Domain.Response;
using GBM_Stocks_Orders_Domain.Views;
using Moq;

namespace GBM_Test
{
    [TestClass]
    public class OrderServiceTest
    {
        public Mock<ITransact> TransactMock;
        public Mock<IUnitOfWork> UnitOfWorkMock;
        public Mock<IAccountRepository> AccountRepositoryMock;

        public IOrderBusinessRules OrderBusinessRules;

        public StoredSingleResponse<GetAccountDetailsResponse> StoredSingleResponse;

        [TestMethod]
        public void SetUp()
        {
            StoredSingleResponse = new StoredSingleResponse<GetAccountDetailsResponse>();
            var getAccountDetailsResponse = new GetAccountDetailsResponse();
            getAccountDetailsResponse.Orders = new List<OrderView>();
            getAccountDetailsResponse.ShareByAccount = new List<ShareByAccountView>();
            getAccountDetailsResponse.Account = new AccountView();

            StoredSingleResponse.Response = getAccountDetailsResponse;
            StoredSingleResponse.Success = true;

            TransactMock = new Mock<ITransact>();
            UnitOfWorkMock = new Mock<IUnitOfWork>();
            AccountRepositoryMock = new Mock<IAccountRepository>();

            OrderBusinessRules = new OrderBusinessRules();
        }

        [TestMethod]
        public void AccountDontExist()
        {
            SetUp();
            StoredSingleResponse.Response.Account = null;

            //arrange
            var ordersRepositoryMock = new Mock<IOrdersRepository>();
            ordersRepositoryMock.Setup(x => x.GetAccountDetails(It.IsAny<GetAccountDetailsRequest>(), It.IsAny<string>())).ReturnsAsync(StoredSingleResponse);
            var service = new OrdersService(ordersRepositoryMock.Object, OrderBusinessRules, AccountRepositoryMock.Object, UnitOfWorkMock.Object);
            var createOrderRequest = new CreateOrderRequest
            {
                AccountId = 1
            };

            //act
            var created = service.CreateOrder(createOrderRequest, "created order").Result;

            //assert
            Assert.IsFalse(created.Success);
            Assert.AreEqual("Account not exist", created.Response.BusinessError);
        }

        [TestMethod]
        public void MarketClosed()
        {
            SetUp();
            StoredSingleResponse.Response.Account = new AccountView { AccountId = 1, Cash = 1000 };

            //arrange
            var ordersRepositoryMock = new Mock<IOrdersRepository>();
            ordersRepositoryMock.Setup(x => x.GetAccountDetails(It.IsAny<GetAccountDetailsRequest>(), It.IsAny<string>())).ReturnsAsync(StoredSingleResponse);
            var service = new OrdersService(ordersRepositoryMock.Object, OrderBusinessRules, AccountRepositoryMock.Object, UnitOfWorkMock.Object);
            var createOrderRequest = new CreateOrderRequest
            {
                AccountId = 1,
                Timestamp = 1682730000 //Fri Apr 28 2023 19:00:00 GMT-0600 

            };

            //act
            var created = service.CreateOrder(createOrderRequest, "created order").Result;

            //assert
            Assert.IsFalse(created.Success);
            Assert.AreEqual("Market is closed", created.Response.BusinessError);
        }

        [TestMethod]
        public void MinimiumShare()
        {
            SetUp();
            StoredSingleResponse.Response.Account = new AccountView { AccountId = 1, Cash = 1000 };

            //arrange
            var ordersRepositoryMock = new Mock<IOrdersRepository>();
            ordersRepositoryMock.Setup(x => x.GetAccountDetails(It.IsAny<GetAccountDetailsRequest>(), It.IsAny<string>())).ReturnsAsync(StoredSingleResponse);
            var service = new OrdersService(ordersRepositoryMock.Object, OrderBusinessRules, AccountRepositoryMock.Object, UnitOfWorkMock.Object);
            var createOrderRequest = new CreateOrderRequest
            {
                AccountId = 1,
                Timestamp = 1682712000, //Fri Apr 28 2023 14:00:00 GMT-0600
                Shares= 0,

            };

            //act
            var created = service.CreateOrder(createOrderRequest, "created order").Result;

            //assert
            Assert.IsFalse(created.Success);
            Assert.AreEqual("You must put a share amount greater than 0", created.Response.BusinessError);
        }

        [TestMethod]
        public void DuplicatedOrder()
        {
            SetUp();
            StoredSingleResponse.Response.Account = new AccountView { AccountId = 1, Cash = 1000 };
            StoredSingleResponse.Response.Orders = new List<OrderView> { new OrderView { AccountId = 1, Timestamp = 1682712000, IssuerName = "Test", Shares = 1, SharePrice = 1000 } };

            //arrange
            var ordersRepositoryMock = new Mock<IOrdersRepository>();
            ordersRepositoryMock.Setup(x => x.GetAccountDetails(It.IsAny<GetAccountDetailsRequest>(), It.IsAny<string>())).ReturnsAsync(StoredSingleResponse);
            var service = new OrdersService(ordersRepositoryMock.Object, OrderBusinessRules, AccountRepositoryMock.Object, UnitOfWorkMock.Object);
            var createOrderRequest = new CreateOrderRequest
            {
                AccountId = 1,
                Timestamp = 1682712000, //Fri Apr 28 2023 14:00:00 GMT-0600
                Shares = 1,
                IssuerName= "Test",
                SharePrice= 1000
            };

            //act
            var created = service.CreateOrder(createOrderRequest, "created order").Result;

            //assert
            Assert.IsFalse(created.Success);
            Assert.AreEqual("This operation is duplicated", created.Response.BusinessError);
        }


        [TestMethod]
        public void InsufficientBalance()
        {
            SetUp();
            StoredSingleResponse.Response.Account = new AccountView { AccountId = 1, Cash = 1000 };

            //arrange
            var ordersRepositoryMock = new Mock<IOrdersRepository>();
            ordersRepositoryMock.Setup(x => x.GetAccountDetails(It.IsAny<GetAccountDetailsRequest>(), It.IsAny<string>())).ReturnsAsync(StoredSingleResponse);
            var service = new OrdersService(ordersRepositoryMock.Object, OrderBusinessRules, AccountRepositoryMock.Object, UnitOfWorkMock.Object);
            var createOrderRequest = new CreateOrderRequest
            {
                AccountId = 1,
                Timestamp = 1682712000, //Fri Apr 28 2023 14:00:00 GMT-0600
                Shares = 10,
                IssuerName = "Test",
                SharePrice = 1000,
                Operation = true
            };

            //act
            var created = service.CreateOrder(createOrderRequest, "created order").Result;

            //assert
            Assert.IsFalse(created.Success);
            Assert.AreEqual("Cash is not enough", created.Response.BusinessError);
        }


        [TestMethod]
        public void InsufficientStocks()
        {
            SetUp();
            StoredSingleResponse.Response.Account = new AccountView { AccountId = 1, Cash = 1000 };
            StoredSingleResponse.Response.ShareByAccount = new List<ShareByAccountView> { new ShareByAccountView { IssuerName = "Test", SharePrice= 1000, TotalShare = 9 } };

            //arrange
            var ordersRepositoryMock = new Mock<IOrdersRepository>();
            ordersRepositoryMock.Setup(x => x.GetAccountDetails(It.IsAny<GetAccountDetailsRequest>(), It.IsAny<string>())).ReturnsAsync(StoredSingleResponse);
            var service = new OrdersService(ordersRepositoryMock.Object, OrderBusinessRules, AccountRepositoryMock.Object, UnitOfWorkMock.Object);
            var createOrderRequest = new CreateOrderRequest
            {
                AccountId = 1,
                Timestamp = 1682712000, //Fri Apr 28 2023 14:00:00 GMT-0600
                Shares = 10,
                IssuerName = "Test",
                SharePrice = 1000,
                Operation = false
            };

            //act
            var created = service.CreateOrder(createOrderRequest, "created order").Result;

            //assert
            Assert.IsFalse(created.Success);
            Assert.AreEqual("Insufficient Stocks", created.Response.BusinessError);
        }


        [TestMethod]
        public void SuccessfulBuy()
        {
            SetUp();
            StoredSingleResponse.Response.Account = new AccountView { AccountId = 1, Cash = 1000 };

            //arrange
            var ordersRepositoryMock = new Mock<IOrdersRepository>();
            ordersRepositoryMock.Setup(x => x.GetAccountDetails(It.IsAny<GetAccountDetailsRequest>(), It.IsAny<string>())).ReturnsAsync(StoredSingleResponse);
            var service = new OrdersService(ordersRepositoryMock.Object, OrderBusinessRules, AccountRepositoryMock.Object, UnitOfWorkMock.Object);
            var createOrderRequest = new CreateOrderRequest
            {
                AccountId = 1,
                Timestamp = 1682712000, //Fri Apr 28 2023 14:00:00 GMT-0600
                Shares = 10,
                IssuerName = "Test",
                SharePrice = 100,
                Operation = true
            };

            //act
            var created = service.CreateOrder(createOrderRequest, "created order").Result;

            //assert
            Assert.IsTrue(created.Success);
            Assert.AreEqual(0, OrderBusinessRules.UpdateCashAccount(createOrderRequest, StoredSingleResponse.Response.Account));
        }

        [TestMethod]
        public void SuccessfulSell()
        {
            SetUp();
            StoredSingleResponse.Response.Account = new AccountView { AccountId = 1, Cash = 1000 };

            //arrange
            var ordersRepositoryMock = new Mock<IOrdersRepository>();
            ordersRepositoryMock.Setup(x => x.GetAccountDetails(It.IsAny<GetAccountDetailsRequest>(), It.IsAny<string>())).ReturnsAsync(StoredSingleResponse);
            var service = new OrdersService(ordersRepositoryMock.Object, OrderBusinessRules, AccountRepositoryMock.Object, UnitOfWorkMock.Object);
            var createOrderRequest = new CreateOrderRequest
            {
                AccountId = 1,
                Timestamp = 1682712000, //Fri Apr 28 2023 14:00:00 GMT-0600
                Shares = 1,
                IssuerName = "Test",
                SharePrice = 100,
                Operation = false
            };

            //act
            var created = service.CreateOrder(createOrderRequest, "created order").Result;

            //assert
            Assert.IsTrue(created.Success);
            Assert.AreEqual(1100, OrderBusinessRules.UpdateCashAccount(createOrderRequest, StoredSingleResponse.Response.Account));
        }
    }
}