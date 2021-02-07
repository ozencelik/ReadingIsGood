using Api.Configuration;
using AutoMapper;
using Core.Services.Customers;
using Data.Dtos.CustomerDtos;
using Data.Entities.Customers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    public class CustomerController : Controller
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;
        #endregion

        #region Ctor
        public CustomerController(IMapper mapper,
            ICustomerService customerService)
        {
            this._mapper = mapper;
            this._customerService = customerService;
        }
        #endregion

        #region Methods
        [HttpPost(ApiRoutes.Customers.Register)]
        public async Task<IActionResult> Register([FromBody] CreateCustomerDto model)
        {
            if (model is null)
                return BadRequest(nameof(model));

            if (string.IsNullOrEmpty(model.Username))
                return BadRequest("Username cannot be null.");

            var existCustomerByCustomername = await _customerService.GetCustomerByUsernameAsync(model.Username);

            if (existCustomerByCustomername != null)
            {
                return Content("This customer is already exist !!!\nPlease use different email and customername.");
            }

            try
            {
                // map model to entity
                var customer = _mapper.Map<Customer>(model);

                // create customer
                await _customerService.RegisterCustomerAsync(customer, model.Password);
            }
            catch
            {
                return Ok("Customer not registered.");
            }

            return Ok("Customer registered ✔");
        }
        #endregion
    }
}
