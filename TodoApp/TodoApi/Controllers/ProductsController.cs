using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;


namespace TodoApi.Controllers
{
    //     Using Pagination 
    //     Sample Web API in C# with pagination implemented 
    //     using a common approach with query parameters like pageNumber and pageSize.
    //     This example is ideal for scenarios like listing users, products, posts, etc.
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }


    [Route("api/[controller]")]
    //[Route("api")]
    [ApiController]
    [AllowAnonymous]
    public class ProductsController : ControllerBase
    {
        private readonly List<Product> _products;
        private readonly IFeatureManager _featureManager;

        public ProductsController(IFeatureManager featureManager)
        {
            _featureManager = featureManager;

            // [+] Mock Data - Normally this comes from DB
            _products = Enumerable.Range(1, 100).Select(i => new Product
            {
                Id = i,
                Name = $"Product {i}"
            }).ToList();
            // [-] Mock Data
        }


        [HttpGet("{pageNumber}/{pageSize}")]
        public ActionResult<PagedResult<Product>> GetPagedProducts(int pageNumber = 1, int pageSize = 10)
        {
            var totalCount = _products.Count;
            var items = _products
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var result = new PagedResult<Product>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return Ok(result);
        }









        // Here we will try out the Feature Flag approach
        // GET: api/Products/BetaFeature  
        [HttpGet("BetaFeature", Name = "BetaFeatureRoute")]
        public async Task<ActionResult<string>> BetaFeature()
        {
            try
            {
                var output = "";
                if (await _featureManager.IsEnabledAsync("BetaFeature"))
                {
                    output="Beta Feature is Enabled";
                }
                else
                                    {
                    output="Beta Feature is Disabled";
                }
                return Ok(output);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }




        [HttpGet("NewUI", Name = "NewUI")]
        [FeatureGate("NewUI")]
        public async Task<ActionResult<string>> NewUI()
        {
            try
            {
                var output = "";
                if (await _featureManager.IsEnabledAsync("NewUI"))
                {
                    output = "NewUI Feature is Enabled";
                }
                else
                {
                    output = "NewUI Feature is Disabled";
                }
                return Ok(output);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


    }
}
