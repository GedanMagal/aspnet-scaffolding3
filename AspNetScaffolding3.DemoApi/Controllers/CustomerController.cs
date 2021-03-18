﻿using AspNetScaffolding.DemoApi.Entities;
using AspNetScaffolding.DemoApi.Models;
using AspNetScaffolding.Extensions.Logger;
using AspNetScaffolding.Extensions.Mapper;
using AspNetSerilog;
using Microsoft.AspNetCore.Mvc;
using RestSharp.Easy;
using System.Net.Http;
using WebApi.Models.Response;

namespace AspNetScaffolding.Controllers
{
    public class CustomerController : BaseController
    {
        public LogAdditionalInfo LogAdditionalInfo { get; set; }

        public CustomerController(LogAdditionalInfo logAdditionalInfo)
        {
            LogAdditionalInfo = logAdditionalInfo;
        }

        /// <summary>
        /// Get Customer
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("customers/{customerId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorsResponse), 400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Get(CustomerRequest request)
        {
            LogAdditionalInfo.Data.Add("CustomerId", request.CustomerId);

            SimpleLogger.Info("teste teste teste", new {request});

            return Ok(new
            {
                request,
                test = "1",
                test_1 = "123"
            });
        }

        [HttpGet("customers/{customerId}/string")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorsResponse), 400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetString([FromRoute] string customerId)
        {
            return Ok(new { customerId });
        }

        [HttpGet("customers/{customerId}/none")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorsResponse), 400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetNone(CustomerRequest2 request)
        {
            return Ok(request);
        }

        [HttpGet("customers/{customerId}/fromroute")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorsResponse), 400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetFromRoute([FromRoute] CustomerRequest2 request)
        {
            return Ok(request);
        }

        [HttpPost("customers")]
        public IActionResult Create([FromBody] CustomerRequest2 request2)
        {
            var customer = request2.As<Customer>();

            var rest = new EasyRestClient("http://pruu.herokuapp.com/dump/baaaaarr");

            var result = rest.SendRequest<object>(HttpMethod.Post, "");

            return Created("", customer);
        }

        [HttpPost("customers/{customerId}")]
        public IActionResult Create2(CustomerRequest2 request2)
        {
            var customer = request2.As<Customer>();

            return Created("", customer);
        }

        [HttpGet("customers/null")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorsResponse), 400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetNull()
        {
            return CreateJsonResponse(ApiResponse.OK());
        }
    }
}
