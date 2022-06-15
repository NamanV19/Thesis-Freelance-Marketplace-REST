using Common.PostModels;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class LoginController : ControllerBase
    {
        public readonly DatabaseContext _context;
        public LoginController(DatabaseContext context) => _context = context;

        [HttpPost]
        public ActionResult<SimpleLoginPostModel> SimpleLogin(SimpleLoginPostModel loginInfo)
        {
            var validate = false;

            if (loginInfo.Type == "Buyer") {
                validate = _context.Buyers.Any(buyer => buyer.Email == loginInfo.Email && buyer.Password == loginInfo.Password);
            }
            else if(loginInfo.Type == "Freelancer")
            {
                validate = _context.Freelancers.Any(freelancer => freelancer.Email == loginInfo.Email && freelancer.Password == loginInfo.Password);
            }

            if (validate == true){
                return Ok("Successful Login");
            }
            else
            {
                return NotFound($"This user does not exist");
            }
        }
    }
}
