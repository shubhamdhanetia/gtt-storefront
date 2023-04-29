using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtoCommerce.Storefront.Domain.Security;
using VirtoCommerce.Storefront.Infrastructure;
using VirtoCommerce.Storefront.Model;
using VirtoCommerce.Storefront.Model.Common;

namespace VirtoCommerce.Storefront.Controllers
{
    [StorefrontRoute]
    public class HomeController : StorefrontControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        public HomeController(IWorkContextAccessor workContextAccessor, IStorefrontUrlBuilder urlBuilder, IAuthorizationService authorizationService) : base(workContextAccessor, urlBuilder)
        {
            _authorizationService = authorizationService;
        }

        [HttpGet("old")]
        public IActionResult Old()
        {

            return View("index");
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, OnlyRegisteredUserAuthorizationRequirement.PolicyName);
            if (!authorizationResult.Succeeded)
            {
                return Challenge();
            }
            WorkContext.Layout = "dashboard_layout";
            return View("dashboard-index");
        }

        [HttpGet("about")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }



    }
}
