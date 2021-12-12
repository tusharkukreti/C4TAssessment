
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace C4TAssessment.Models.Enquiries
{
   
    /// <summary>
    /// Response object for Enquries API.
    /// </summary>
    [Serializable]
    public class EnquiryResponse
    {
        /// <summary>
        /// Constructor body
        /// </summary>       
        public EnquiryResponse()
        {
            RegionalBlocs = new List<RegionalBloc>();
        }
        /// <summary>
        /// Name of the country
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// Alpha2 code of the country
        /// </summary>
        [JsonProperty("alpha2Code")]
        public string Code { get; set; }
        /// <summary>
        /// Name of the browser that is used to make the request
        /// </summary>
        public string BrowserName { get; set; }
        /// <summary>
        /// The moment the request is made
        /// </summary>
        public string Timeestamp { get; set; } = DateTime.UtcNow.ToString();
        /// <summary>
        /// List of RegionalBlocs
        /// </summary>
        [JsonProperty("regionalBlocs")]
        public List<RegionalBloc> RegionalBlocs { get; set; }
    }

    /// <summary>
    /// Regional Bloc
    /// </summary>
    public class RegionalBloc
    {
        /// <summary>
        /// Constructor body
        /// </summary>
        public RegionalBloc()
        {
            Countries = new List<string>();
        }
        /// <summary>
        /// Name of the Regional Bloc
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// Acronym of the regionalBloc
        /// </summary>
        [JsonProperty("acronym")]
        public string Code { get; set; }
        /// <summary>
        /// List of all countries (their Dutch name) that are part of this regionalBloc
        /// </summary>
        [JsonProperty("otherNames")]
        public List<string> Countries { get; set; }
    }
}
