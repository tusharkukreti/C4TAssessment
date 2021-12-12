using C4TAssessment.BusinessAbstraction;
using C4TAssessment.Models.Enquiries;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;
        private readonly string countryApiEndpoint;
        public EnquiriesBusinessDomain(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            countryApiEndpoint = _configuration["Endpoints:CountryUrl"];
        }

        ///<inheritdoc/>
        public async Task<CountryEnquiryResponseDto<List<EnquiryResponse>>> GetCountryDetails(EnquiryRequest enquiryRequest)
        {
            CountryEnquiryResponseDto<List<EnquiryResponse>> countryEnquiryResponseDto = new();
            List<EnquiryResponse> enquiryResponses = new();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format(countryApiEndpoint, enquiryRequest.Name));
            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                var serializaedJsonStringResponse = await reader.ReadToEndAsync();
                if (serializaedJsonStringResponse.Contains("Not Found"))
                {
                    countryEnquiryResponseDto.StatusCode = StatusCodes.Status404NotFound;
                    countryEnquiryResponseDto.Message = $"No country found having name: {enquiryRequest.Name}";
                }
                else
                {
                    enquiryResponses = JsonConvert.DeserializeObject<List<EnquiryResponse>>(serializaedJsonStringResponse);
                    FillBrowserNameToResponse(enquiryResponses);
                }
                countryEnquiryResponseDto.Message = "Success";
                countryEnquiryResponseDto.Response = enquiryResponses;
            }
            return countryEnquiryResponseDto;
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
