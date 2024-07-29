using DesignPatterns.Structural.Proxy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace DesignPatterns.Tests.Proxy
{
    public class CustomerRepositoryProxyTests
    {
        private IHttpContextAccessor _httpContextAccessor;
        private HttpContext _httpContext;
        private CustomerRepository _customerRepository;
        private IMemoryCacheWrapper _memoryCacheWrapper;

        [SetUp] 
        public void SetUp() 
        {
            _httpContextAccessor = Substitute.For<IHttpContextAccessor>();
            _httpContext = _httpContextAccessor.HttpContext;
            _customerRepository = Substitute.For<CustomerRepository>();
            _memoryCacheWrapper = Substitute.For<IMemoryCacheWrapper>();
        }

        [Test]
        public void ShouldBeAbleToGetBlockedCustomersWhenPermissionIsAdmin()
        {
            _httpContext.Request.Headers["x-role"] = "admin";
            _httpContextAccessor.HttpContext.Returns(_httpContext);

            var expectedBlockedUsers = new List<Customer>
            {
                new Customer("Fulano", DateTime.Now.AddYears(-20))
            };

            _customerRepository.GetBlockedUsers().Returns(expectedBlockedUsers);
            _memoryCacheWrapper.GetOrCreate("blocked-customers", Arg.Any<Func<ICacheEntry, object>>())
                .Returns(expectedBlockedUsers);

            var customerRepositoryProxy = new CustomerRepositoryProxy(
                _customerRepository,
                _memoryCacheWrapper,
                _httpContextAccessor);

            var blockedUsers = customerRepositoryProxy.GetBlockedUsers();

            blockedUsers.Should().BeEquivalentTo(expectedBlockedUsers);
        }

        [Test]
        public void ShouldNotBeAbleToGetBlockedCustomersWhenPermissionIsNotAdmin()
        {
            _httpContext.Request.Headers["x-role"] = "user";
            _httpContextAccessor.HttpContext.Returns(_httpContext);

            var expectedBlockedUsers = new List<Customer>
            {
                new Customer("Fulano", DateTime.Now.AddYears(-20))
            };

            _customerRepository.GetBlockedUsers().Returns(expectedBlockedUsers);
            _memoryCacheWrapper.GetOrCreate("blocked-customers", Arg.Any<Func<ICacheEntry, object>>()).ReturnsNull();

            var customerRepositoryProxy = new CustomerRepositoryProxy(
                _customerRepository,
                _memoryCacheWrapper,
                _httpContextAccessor);

            var blockedUsers = customerRepositoryProxy.GetBlockedUsers();

            blockedUsers.Should().BeNull();
        }

        [Test]
        public void ShouldNotBeAbleToGetBlockedCustomersWhenContextIsNull()
        {
            _httpContextAccessor = null!;
            _customerRepository.GetBlockedUsers().ReturnsNull();
            _memoryCacheWrapper.GetOrCreate("blocked-customers", Arg.Any<Func<ICacheEntry, object>>()).ReturnsNull();

            var customerRepositoryProxy = new CustomerRepositoryProxy(
                _customerRepository,
                _memoryCacheWrapper,
                _httpContextAccessor);

            var blockedUsers = customerRepositoryProxy.GetBlockedUsers();

            blockedUsers.Should().BeNull();
        }
    }
}
