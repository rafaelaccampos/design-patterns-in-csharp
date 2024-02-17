using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace DesignPatterns.Structural.Proxy
{
    public class CustomerRepositoryProxy
    {
        private readonly CustomerRepository _customerRepository;
        private readonly IMemoryCache _cache;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomerRepositoryProxy(
            CustomerRepository customerRepository,
            IMemoryCache cache,
            IHttpContextAccessor httpContextAccessor)
        {
            _customerRepository = customerRepository;
            _cache = cache;
            _httpContextAccessor = httpContextAccessor;
        }

        public IList<Customer?> GetBlockedUsers()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if(httpContext == null)
            {
                return null!;
            }

            if (httpContext.Request.Headers["x-role"] != "admin")
            {
                return null!;
            }

            var blockedCustomers = _cache.GetOrCreate("blocked-customers", c =>
            {
                return _customerRepository.GetBlockedUsers();
            });

            return blockedCustomers!;
        }
    }
}
