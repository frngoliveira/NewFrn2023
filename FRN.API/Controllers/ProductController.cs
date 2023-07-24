using FRN.Application._1._1_Interface;
using FRN.Domain._2._1_Interface;
using FRN.Domain._2._2_Entity;
using Microsoft.AspNetCore.Mvc;

namespace FRN.API.Controllers
{
    [Route("api/[controller]")]    
    public class ProductController : ApiController
    {
        private readonly IProdutoAppService _produtoAppService;
        public ProductController(IProdutoAppService produtoAppService,
                                 IDomainNotificationHandler notifications) : base(notifications) 
        {
            _produtoAppService = produtoAppService;
        }

        [HttpGet("get")]
        public IActionResult Get() 
        {
            var result = _produtoAppService.Get();

            if (IsValidOperation())
                return Response(result);               

            if (result == null) return NotFound();
                return Response(result);
        }

        [HttpPost("create")]
        public IActionResult Post([FromBody]Product product) 
        {
            if (!ModelState.IsValid) return Response(ModelState);
            _produtoAppService.Post(product);
            return Response(product);
        }

        [HttpPut("update")]
        public IActionResult Put([FromBody]Product product)
        {
            if (!ModelState.IsValid) return Response(ModelState);
            _produtoAppService.Put(product);
            return Response(product);
        }

    }
}
