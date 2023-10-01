using FRN.API.Services;
using FRN.Application._1._1_Interface;
using FRN.Application._1._2_AppService;
using FRN.Domain._2._1_Interface;
using FRN.Domain._2._2_Entity;
using Microsoft.AspNetCore.Mvc;

namespace FRN.API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ApiController
    {

        private readonly IUserAppService _userAppService;
        public UserController(IUserAppService userAppService,
                              IDomainNotificationHandler notifications) : base(notifications)
        {
            _userAppService = userAppService;
        }

        [HttpGet("Login")]
        public IActionResult Login(string userName, string senha)
        {
            var user = new Users
            {
                UserName = userName,
                Password = senha
            };

            var result = _userAppService.Get(user);            

            if (result == null) return BadRequest(new { message = "Usuário ou Senha Inválido"});

            var token = TokenService.GenerateToken(user);

            user.Password = "";

            var value = new
            {
                user = user,
                token = token,
            };

            return Ok(value);            
        }

        [HttpGet("getAll")]
        public IActionResult GetAllUser()
        {
            var result = _userAppService.GetAllUser();

            if (IsValidOperation())
                return Response(result);

            if (result == null) return NotFound();
            return Response(result);
        }

        [HttpPost("create")]
        public IActionResult Post([FromBody] Users user)
        {
            if (!ModelState.IsValid) return Response(ModelState);
            _userAppService.Post(user);
            return Response(user);
        }

        [HttpPut("update")]
        public IActionResult Put([FromBody] Users user)
        {
            if (!ModelState.IsValid) return Response(ModelState);
            _userAppService.Put(user);
            return Response(user);
        }
    }
}
