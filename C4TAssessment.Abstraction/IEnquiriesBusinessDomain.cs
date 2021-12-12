using C4TAssessment.Models.Enquiries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace C4TAssessment.BusinessAbstraction
{
    /// <summary>
    /// Abstract domain for enquiries
    /// </summary>
    public interface IEnquiriesBusinessDomain
    {
        /// <summary>
        /// Get Country details as per the requested object
        /// </summary>
        /// <param name="enquiryRequest">Enquiry request object</param>
        /// <returns>Enquiry Response with status code and message</returns>
        Task<CountryEnquiryResponseDto<List<EnquiryResponse>>> GetCountryDetails(EnquiryRequest enquiryRequest);
    }
}
