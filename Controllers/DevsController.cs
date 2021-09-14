using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Security.Claims;
using DevsApi.API;
using DevsApi.Models;
using DevsApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevsApi.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
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

        /// <summary>
        /// Returns a list of all developers registered in database.
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult Index() => ApiOk(_service.All());

        /// <summary>
        /// Returns a specified developer according to ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id}"), HttpGet]
        public IActionResult Get(int? id) => 
            _service.Get(id) != null ? 
                ApiOk(_service.Get(id)) :
                ApiNotFound("Registration not found.");

        /// <summary>
        /// Creates a specified developer according to body data.
        /// </summary>
        /// <param name="developer"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost]
        public IActionResult Create([FromBody] Developer developer){
            
            developer.createdById = User.Claims.FirstOrDefault(d => d.Type == ClaimTypes.NameIdentifier).Value;

            return _service.Create(developer) ? 
                    ApiCreated($"{Request.Path}/{_service.All().LastOrDefault().id}", "Successful registration."):
                    ApiNotFound("Error in creation");
        }

        /// <summary>
        /// Updates a specified developer according to body data.
        /// </summary>
        /// <param name="developer"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut]
        public IActionResult Update([FromBody] Developer developer){

            developer.updatedById = User.Claims.FirstOrDefault(d => d.Type == ClaimTypes.NameIdentifier).Value;

            return _service.Update(developer) ? 
                    ApiOk("Successful update."):
                    ApiNotFound("Error in update");
        }

        /// <summary>
        /// Deletes a specified developer according to ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AuthorizeRoles(RoleType.Admin)]
        [Route("{id}"), HttpDelete]
        public IActionResult Delete(int? id) => 
            _service.Delete(id) ? 
                ApiOk("Successful delete."):
                ApiNotFound("Error in deletion");

        /// <summary>
        /// Returns a random developer.
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Random"), HttpGet]
        public IActionResult Random(){
            
            Random random = new();
            List<Developer> lista = _service.All(); 
            return ApiOk(_service.Get(lista[random.Next(lista.Count)].id));
        }

        /// <summary>
        /// Returns a list of all developers created according to a specified role.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        [Route("DevsByRole/{role?}"), HttpGet]
        public IActionResult ProductsByRole(string role) => ApiOk(_service.DevelopersByUserRole(role));
        
    }
}