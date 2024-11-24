using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SteamRoulette.Persistanse;

namespace SteamRoullete.WebApi.Conrollers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private MyDbContext _db;

        public TestController(MyDbContext myDbContext)
        {
            _db = myDbContext;
        }
    }
}