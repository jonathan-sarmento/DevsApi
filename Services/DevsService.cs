using System;
using System.Collections.Generic;
using System.Linq;
using DevsApi.Data;
using DevsApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DevsApi.Services
{
    public class DevsService : IDevsService
    {
        readonly DevsApiContext _context;
        public DevsService(DevsApiContext context)
        {
            _context = context;
        }
        public List<Developer> All() => _context.Developers.ToList();

        public bool Create(Developer developer)
        {
            if(_context.Developers.FirstOrDefault(d => d.cpf == developer.cpf) != null)
                return false;

            try{
                developer.created = DateTime.Now;
                _context.Developers.Add(developer);
                _context.SaveChanges();
                return true;
            }
            catch{
                return false;
            }
        }

        public bool Delete(int? id)
        {
            Developer developer = Get(id);
            if(developer == null)
                return false;

            try{
                _context.Developers.Remove(developer);
                _context.SaveChanges();
                return true;
            }
            catch{
                return false;
            }
        }

        public Developer Get(int? id) => _context.Developers.FirstOrDefault(d => d.id == id);

        public bool Update(Developer developer)
        {
            try{
                if(!_context.Developers.Any(d => d.id == developer.id))
                    throw new Exception("Not found.");

                developer.updated = DateTime.Now;
                _context.Developers.Update(developer);
                _context.SaveChanges();
                return true;
            }
            catch{
                return false;
            }
        }

        public List<Developer> DevelopersByUserRole(string getRole)
        {
            //var query1 = "SELECT p.* FROM Developers p " +
            //    "JOIN AspNetUsers u ON p.createdById = u.Id " +
            //    "JOIN AspNetUserRoles ur ON u.Id = ur.UserId " +
            //    "JOIN AspNetRoles r ON ur.RoleId = r.Id " +
            //    $"WHERE r.Name = '{getRole}'";

            var lquery1 = from developer in _context.Set<Developer>()
                          join user in _context.Set<IdentityUser>()
                            on developer.createdById equals user.Id
                          join userRoles in _context.Set<IdentityUserRole<string>>()
                            on user.Id equals userRoles.UserId
                          join role in _context.Set<IdentityRole>()
                            on userRoles.RoleId equals role.Id
                          where role.Name.ToUpper() == getRole
                          select new Developer()
                          {
                              id = developer.id,
                              name = developer.name,
                              cpf = developer.cpf,
                              age = developer.age,
                              stack = developer.stack,
                              seniority = developer.seniority,
                              technologies = developer.technologies,
                              country = developer.country,
                              state = developer.state,
                              created = developer.created,
                              updated = developer.updated,
                              createdBy = developer.createdBy,
                              updatedBy = developer.updatedBy
                          };

            return lquery1.ToList();
        }
    }
}