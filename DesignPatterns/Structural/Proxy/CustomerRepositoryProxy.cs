namespace DesignPatterns.Structural.Proxy
{
    public class CustomerRepositoryProxy
    {
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
                return null;
            }

            if (httpContext.Request.Headers["x-role"] != "admin")
            {
                return null;
            }

            var blockedCustomers = _cache.GetOrCreate("blocked-customers", c =>
            {
                return _customerRepository.GetBlockedUsers();
            });

            return blockedCustomers;
        }
    }
}
