using C4TAssessment.BusinessAbstraction;
using C4TAssessment.Models.Enquiries;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace C4TAssessment.BusinessImplementation
{
    /// <summary>
    /// Enquiry Business Implementation
    /// </summary>
    public class EnquiriesBusinessDomain : IEnquiriesBusinessDomain
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAzureService _azureService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<EnquiriesBusinessDomain> _logger;
        private readonly string countryApiEndpoint;

        /// <summary>
        /// Domain Initializer
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="configuration"></param>
        /// <param name="azureService"></param>
        /// <param name="logger"></param>
        public EnquiriesBusinessDomain(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IAzureService azureService, ILogger<EnquiriesBusinessDomain> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            countryApiEndpoint = _configuration["Endpoints:CountryUrl"];
            _azureService = azureService;
            _logger = logger;
        }

        ///<inheritdoc/>
        public async Task<CountryEnquiryResponseDto<List<EnquiryResponse>>> GetCountryDetails(EnquiryRequest enquiryRequest)
        {
            CountryEnquiryResponseDto<List<EnquiryResponse>> countryEnquiryResponseDto = new();
            List<EnquiryResponse> enquiryResponses = new();
            _logger.LogInformation($"EnquireCountries: Hitting {string.Format(countryApiEndpoint, enquiryRequest.Name)}");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format(countryApiEndpoint, enquiryRequest.Name));
            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                var serializaedJsonStringResponse = await reader.ReadToEndAsync();
                if (serializaedJsonStringResponse.Contains("Not Found"))
                {
                    _logger.LogError($"EnquireCountries: Invalid country name provided. No record found.");
                    countryEnquiryResponseDto.StatusCode = StatusCodes.Status404NotFound;
                }
                else
                {
                    _logger.LogInformation($"EnquireCountries: Country details fetched successfully.");
                    enquiryResponses = JsonConvert.DeserializeObject<List<EnquiryResponse>>(serializaedJsonStringResponse);
                    FillBrowserNameToResponse(enquiryResponses);
                    _logger.LogInformation($"EnquireCountries: Pushing country detail to Azure service bus queue.");
                    await PushEnquiryResponseToQueue(enquiryResponses);
                }
                countryEnquiryResponseDto.Response = enquiryResponses;
            }

            return countryEnquiryResponseDto;
        }

        private async Task PushEnquiryResponseToQueue(List<EnquiryResponse> enquiryResponses)
        {
            var messagesToBePushed = new List<string>();
            foreach (var response in enquiryResponses)
            {
                messagesToBePushed.Add(JsonConvert.SerializeObject(response));
            }
            await _azureService.PushMessageToServiceBusQueue(messagesToBePushed);
            _logger.LogInformation($"EnquireCountries: Country details pushed successfully to queue.");
        }

        private void FillBrowserNameToResponse(List<EnquiryResponse> enquiryResponses)
        {
            _ = enquiryResponses.Select(x => { x.BrowserName = GetBrowserName(); return x; }).ToList();
        }

        private string GetBrowserName()
        {
            var userAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"];
            return Convert.ToString(userAgent);
        }
    }
}
