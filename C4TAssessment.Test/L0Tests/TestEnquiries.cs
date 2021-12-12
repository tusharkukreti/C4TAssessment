using C4TAssessment.API.Controllers;
using C4TAssessment.BusinessAbstraction;
using C4TAssessment.Test.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace C4TAssessment.Test.L0Tests
{
    public class TestEnquiries
    {
        public Mock<IEnquiriesBusinessDomain> mock = new Mock<IEnquiriesBusinessDomain>();
        private const string invalidCountryName = "InvalidName";
        private const string validCountryName = "Mexico";
      
        [Fact]
        public async void EnquireCountriesShouldReturn404WhenInvalidCountryNameIsGiven()
        {
            var responseDto = EnquiriesHelper.CountryEnquiryNotFoundResponseDto();
            var request = EnquiriesHelper.CreateEnquiryRequestDto(invalidCountryName);
            mock.Setup(x => x.GetCountryDetails(request)).ReturnsAsync(responseDto);
            EnquiriesController enquiries = new EnquiriesController(mock.Object);
            var result = (ObjectResult)await enquiries.EnquireCountries(request);
            result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }
        [Fact]
        public async void EnquireCountriesShouldReturnSuccessAndValidResultWhenValidCountryNameIsGiven()
        {
            var responseDto = EnquiriesHelper.CountryEnquirySuccessResponseDto();
            var request = EnquiriesHelper.CreateEnquiryRequestDto(invalidCountryName);
            mock.Setup(x => x.GetCountryDetails(request)).ReturnsAsync(responseDto);
            EnquiriesController enquiries = new EnquiriesController(mock.Object);
            var result = (ObjectResult)await enquiries.EnquireCountries(request);
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().BeEquivalentTo(responseDto.Response);
        }
    }
}
