using Task;

namespace Test
{
    [TestFixture]
    public class AdoServiceTests
    {
        private AdoService _adoService;
        private string _connectionString = "Server=localhost;Database=learn13;User Id=vova;Password=vova;TrustServerCertificate=True;";

        [SetUp]
        public void Setup()
        {
            _adoService = new AdoService(_connectionString);
        }

        [Test]
        public void CreateProduct_ShouldCreateProduct()
        {
            int productId = _adoService.CreateProduct("TestProduct", "TestDescription", 1.0m, 1.0m, 1.0m, 1.0m);
            var product = _adoService.GetProduct(productId);
            Assert.IsNotNull(product);
            Assert.That(product.Name, Is.EqualTo("TestProduct"));
        }

        [Test]
        public void UpdateProduct_ShouldUpdateProduct()
        {
            int productId = _adoService.CreateProduct("TestProduct", "TestDescription", 1.0m, 1.0m, 1.0m, 1.0m);
            _adoService.UpdateProduct(productId, "UpdatedProduct", "UpdatedDescription", 2.0m, 2.0m, 2.0m, 2.0m);
            var product = _adoService.GetProduct(productId);
            Assert.That(product.Name, Is.EqualTo("UpdatedProduct"));
        }

        [Test]
        public void GetAllProducts_ShouldReturnAllProducts()
        {
            var products = _adoService.GetAllProducts();
            Assert.IsNotEmpty(products);
        }

        [Test]
        public void CreateOrder_ShouldCreateOrder()
        {
            int productId = _adoService.CreateProduct("TestProduct", "TestDescription", 1.0m, 1.0m, 1.0m, 1.0m);
            int orderId = _adoService.CreateOrder("Pending", DateTime.Now, DateTime.Now, productId);
            var order = _adoService.GetOrder(orderId);
            Assert.IsNotNull(order);
            Assert.That(order.Status, Is.EqualTo("Pending"));
        }

        [Test]
        public void UpdateOrder_ShouldUpdateOrder()
        {
            int productId = _adoService.CreateProduct("TestProduct", "TestDescription", 1.0m, 1.0m, 1.0m, 1.0m);
            int orderId = _adoService.CreateOrder("Pending", DateTime.Now, DateTime.Now, productId);
            _adoService.UpdateOrder(orderId, "Completed", DateTime.Now, DateTime.Now, productId);
            var order = _adoService.GetOrder(orderId);
            Assert.That(order.Status, Is.EqualTo("Completed"));
        }

        [Test]
        public void DeleteOrder_ShouldDeleteOrder()
        {
            int productId = _adoService.CreateProduct("TestProduct", "TestDescription", 1.0m, 1.0m, 1.0m, 1.0m);
            int orderId = _adoService.CreateOrder("Pending", DateTime.Now, DateTime.Now, productId);
            _adoService.DeleteOrder(orderId);
            var order = _adoService.GetOrder(orderId);
            Assert.IsNull(order);
        }

        [Test]
        public void GetOrders_ShouldReturnFilteredOrders()
        {
            int productId = _adoService.CreateProduct("TestProduct", "TestDescription", 1.0m, 1.0m, 1.0m, 1.0m);
            _adoService.CreateOrder("Pending", DateTime.Now, DateTime.Now, productId);
            var orders = _adoService.GetOrders(month: DateTime.Now.Month, year: DateTime.Now.Year, status: "Pending");
            Assert.IsNotEmpty(orders);
        }

        [Test]
        public void DeleteOrders_ShouldDeleteFilteredOrders()
        {
            int productId = _adoService.CreateProduct("TestProduct", "TestDescription", 1.0m, 1.0m, 1.0m, 1.0m);
            _adoService.CreateOrder("Pending", DateTime.Now, DateTime.Now, productId);
            _adoService.DeleteOrders(month: DateTime.Now.Month, year: DateTime.Now.Year, status: "Pending", productId: productId);
            var orders = _adoService.GetOrders(month: DateTime.Now.Month, year: DateTime.Now.Year, status: "Pending", productId: productId);
            Assert.IsEmpty(orders);
        }

        [Test]
        public void DeleteProduct_ShouldDeleteProduct()
        {
            int productId = _adoService.CreateProduct("TestProduct", "TestDescription", 1.0m, 1.0m, 1.0m, 1.0m);
            _adoService.DeleteProduct(productId);
            var product = _adoService.GetProduct(productId);
            Assert.IsNull(product);
        }
    }
}

