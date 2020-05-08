using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Domain.Interfaces;
using WebApi.Domain.Entities;

namespace WebApi.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;

        public UserController(ILogger<UserController> logger, IUserRepository borrowerRepository)
        {
            _logger = logger;
            _userRepository = borrowerRepository;
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<User> Get(string userId)
        {
            try
            {
                var user = await _userRepository.GetUser(userId);

                if (user != null)
                {
                    _logger.LogInformation($"User Found and returned: {user.UserId}");
                    return user;
                }
                else
                {
                    _logger.LogWarning($"Borrower not found: {userId}. Request Details: {Request.Headers}");
                    return null;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }
    }
}
