using C4TAssessment.BusinessAbstraction;
using C4TAssessment.Models.Enquiries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace C4TAssessment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnquiriesController : ControllerBase
    {
        private readonly IEnquiriesBusinessDomain _enquiriesBusinessDomain;
        private readonly ILogger<EnquiriesController> _logger;

        /// <summary>
        /// Contructor body
        /// </summary>
        /// <param name="enquiriesBusinessDomain"></param>
        public EnquiriesController(IEnquiriesBusinessDomain enquiriesBusinessDomain, ILogger<EnquiriesController> logger)
        {
            _enquiriesBusinessDomain = enquiriesBusinessDomain;
            _logger = logger;
        }

        /// <summary>
        /// API Action method to get the country details
        /// </summary>
        /// <param name="enquiryRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> EnquireCountries(EnquiryRequest enquiryRequest)
        {
            throw new System.Exception("dsds");
            _logger.LogInformation("EnquireCountries: Starting operation..");
            var countryDetails = await _enquiriesBusinessDomain.GetCountryDetails(enquiryRequest);
            _logger.LogInformation($"EnquireCountries: Operation complete.");
            return StatusCode(countryDetails.StatusCode, countryDetails.Response);
        }
    }
}
