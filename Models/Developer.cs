
using System;
using Microsoft.AspNetCore.Identity;

namespace DevsApi.Models
{
    public class Developer
    {
        public int id { get; set; }
        public string name { get; set; }
        public string cpf { get; set; }
        public int age { get; set; }
        public string stack { get; set; }//Back-end, Front-end or Fullstack
        public string seniority { get; set; }// Junior, Full, Senior
        public string technologies { get; set; }
        public string country { get; set; }
        public string state { get; set; }


        public DateTime? created { get; set; }
        public DateTime? updated { get; set; }
        public string updatedById { get; set; }
        public IdentityUser updatedBy { get; set; }
        public string createdById { get; set; }
        public IdentityUser createdBy { get; set; }
        
        
    }
}