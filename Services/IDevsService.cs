using System.Collections.Generic;
using DevsApi.Models;

namespace DevsApi.Services
{
    public interface IDevsService
    {
         List<Developer> All();
         
         Developer Get(int? id);

         bool Create(Developer developer);
         
         bool Update(Developer developer);
         
         bool Delete(int? id);

         List<Developer> DevelopersByUserRole(string getRole);

    }
}