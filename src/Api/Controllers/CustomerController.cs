using Api.Configuration;
using Api.Model;
using AutoMapper;
using Core.Services.Customers;
using Data.Dtos.CustomerDtos;
using Data.Entities.Customers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class CustomerController : Controller
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;
        private readonly AppSettings _appSettings;
        #endregion

        #region Ctor
        public CustomerController(IMapper mapper,
            ICustomerService customerService,
            IOptions<AppSettings> appSettings)
        {
            this._mapper = mapper;
            this._customerService = customerService;
            this._appSettings = appSettings.Value;
        }
        #endregion

        #region Methods

        [HttpGet(ApiRoutes.Customers.GetById)]
        public async Task<IActionResult> GetCustomer(int customerId)
        {
            if (customerId <= 0)
                return BadRequest("Customer id must be greater than zero.");

            // Get customer
            var customer = await _customerService.GetCustomerByIdAsync(customerId);

            return customer is null ?
                BadRequest("Customer not found !!!") :
                Ok(_mapper.Map<GetCustomerDto>(customer));
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Customers.Login)]
        public async Task<IActionResult> Login([FromBody] LoginCustomerDto model)
        {
            if (model is null)
                return BadRequest("Model cannot be null.");

            if (string.IsNullOrEmpty(model.Username))
                return BadRequest("Username cannot be null.");

            if (string.IsNullOrEmpty(model.Password))
                return BadRequest("Password cannot be null.");

            try
            {
                var customer = await _customerService.LoginCustomerWithUsernameAsync(model.Username, model.Password);

                if (customer is null)
                    return BadRequest("Customername or password is incorrect");


                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, customer.Id.ToString())
                    }),
                    Expires = DateTime.Now.AddDays(_appSettings.LoginExpirationDay),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                // return basic customer info and authentication token
                return Ok(new
                {
                    Id = customer.Id,
                    Username = customer.Username,
                    Message = string.Format("Your login session will be take {0} day.", _appSettings.LoginExpirationDay),
                    Token = tokenString
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Customers.Register)]
        public async Task<IActionResult> Register([FromBody] CreateCustomerDto model)
        {
            if (model is null)
                return BadRequest(nameof(model));

            if (string.IsNullOrEmpty(model.Username))
                return BadRequest("Username cannot be null.");

            var existCustomerByCustomername = await _customerService.GetCustomerByUsernameAsync(model.Username);

            if (existCustomerByCustomername != null)
                return Content("This customer is already exist !!!\nPlease use different email and customername.");

            try
            {
                // map model to entity
                var customer = _mapper.Map<Customer>(model);

                // create customer
                await _customerService.RegisterCustomerAsync(customer, model.Password);
            }
            catch (Exception ex)
            {
                return Ok("Customer not registered.");
            }

            return Ok("Customer registered ✔");
        }
        #endregion
    }
}
