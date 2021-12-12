using C4TAssessment.Models.Enquiries;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C4TAssessment.Test.Helpers
{
    public static class EnquiriesHelper
    {
        public static EnquiryRequest CreateEnquiryRequestDto(string name)
        {
            return new EnquiryRequest()
            {
                Name = name
            };
        }
        public static CountryEnquiryResponseDto<List<EnquiryResponse>> CountryEnquiryNotFoundResponseDto()
        {
            CountryEnquiryResponseDto<List<EnquiryResponse>> response = new();
            response.StatusCode = StatusCodes.Status404NotFound;
            response.Response = new();
            return response;
        }
        public static CountryEnquiryResponseDto<List<EnquiryResponse>> CountryEnquirySuccessResponseDto()
        {
            CountryEnquiryResponseDto<List<EnquiryResponse>> response = new();
            List<EnquiryResponse> enquiryResponses = new();
            List<RegionalBloc> regionalBlocs = new();
            regionalBlocs.Add(new RegionalBloc()
            {
                Name = "Pacific Alliance",
                Code = "PA",
                Countries = new List<string>() { "Alianza del Pacífico" }
            });
            enquiryResponses.Add(new EnquiryResponse()
            {
                Name = "Mexico",
                Code = "MX",
                BrowserName = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.93 Safari/537.36",
                Timeestamp = "12-12-2021 05:16:17",
                RegionalBlocs = regionalBlocs
            });
            response.StatusCode = StatusCodes.Status200OK;
            response.Response = enquiryResponses;
            return response;
        }
    }
}
