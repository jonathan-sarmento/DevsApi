using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using DevsApi.API;
using DevsApi.Models;
using DevsApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevsApi.Controllers
{
    [AuthorizeRoles(RoleType.Common, RoleType.Admin)]
    [Route("[controller]")]
    [ApiController]
    public class DevsController : ApiBaseController
    {
        readonly IDevsService _service;
        public DevsController(IDevsService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index() => ApiOk(_service.All());

        [Route("{id}"), HttpGet]
        public IActionResult Get(int? id) => 
            _service.Get(id) != null ? 
                ApiOk(_service.Get(id)) :
                ApiNotFound("Registration not found.");

        [HttpPost]
        public IActionResult Create([FromBody] Developer developer){
            
            developer.createdById = User.Claims.FirstOrDefault(d => d.Type == ClaimTypes.NameIdentifier).Value;

            return _service.Create(developer) ? 
                    ApiOk("Successful registration."):
                    ApiNotFound("Error in creation");
        }

        [HttpPut]
        public IActionResult Update([FromBody] Developer developer){

            developer.updatedById = User.Claims.FirstOrDefault(d => d.Type == ClaimTypes.NameIdentifier).Value;

            return _service.Update(developer) ? 
                    ApiOk("Successful update."):
                    ApiNotFound("Error in update");
        }

        [AuthorizeRoles(RoleType.Admin)]
        [Route("{id}"), HttpDelete]
        public IActionResult Delete(int? id) => 
            _service.Delete(id) ? 
                ApiOk("Successful delete."):
                ApiNotFound("Error in deletion");

        [Route("Random"), HttpGet]
        public IActionResult Random(){
            
            Random random = new();
            List<Developer> lista = _service.All(); 
            return ApiOk(_service.Get(lista[random.Next(lista.Count)].id));
        }

        [AllowAnonymous]
        [Route("DevsByRole/{role?}")]
        [HttpGet]
        public IActionResult ProductsByRole(string role) => ApiOk(_service.DevelopersByUserRole(role));
        
    }
}