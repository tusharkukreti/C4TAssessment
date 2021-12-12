using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C4TAssessment.Models.Enquiries
{
    /// <summary>
    /// Country response DTO
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CountryEnquiryResponseDto<T> where T : new()
    {
        public CountryEnquiryResponseDto()
        {
            Response = new();
        }
        /// <summary>
        /// Status Code of the API
        /// </summary>
        public int StatusCode { get; set; } = StatusCodes.Status200OK;
        /// <summary>
        /// Status message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Respone value
        /// </summary>
        public T Response { get; set; }
    }
}
