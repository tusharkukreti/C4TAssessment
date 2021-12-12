using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C4TAssessment.Models.Enquiries
{
    /// <summary>
    /// Request object for Enquries API.
    /// </summary>
    public class EnquiryRequest
    {
        /// <summary>
        /// Name of the country to be searched for.
        /// </summary>
        public string Name { get; set; }
    }
}
