using System;
using System.Collections.Generic;
using System.Linq;
using DevsApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DevsApi.Data
{
    public static class SeedDatabase
    {
        public static void Initialize(IHost app)
        {
            using (var scope = app.Services.CreateScope())
            {

                var context = scope.ServiceProvider.GetRequiredService<DevsApiContext>();
                context.Database.Migrate();

                if (!context.Developers.Any())
                {

                    Random random = new();
                    List<string> names = new()
                    {
                        "Alexandre", "Amanda", "Eduardo", "Catarina", "Henrique", "Gabriela", "Murilo", "Maria", "Theo", "Laís", "André","Olívia", "Enrico", "Ana", "Henry", "Cecília", "Nathan", "Gabrielle", "Thiago", "Lara"
                    };
                    List<string> lastnames = new() {"Sousa", "Souza", "Sena", "Silva", "Andrade", "Magalhães", "Vargas", "Dantas", "Abreu", "Alves", "González", "Santos"};

                    List<string> stacks = new(){"Back-end", "Front-end", "Fullstack"};
                    
                    List<string> seniorities = new(){"Junior", "Full", "Senior"};
                    
                    List<string> technologiesBack = new(){"C#", "Python", "Node.js", "Ruby", "Java", "PHP", "SQL Server", "Postgresql", "MongoDB", "MySQL"};
                    List<string> technologiesFront = new(){"Javascript", "React.js", "Angular", "Vue.js"};
                    
                    List<string> countries = new(){"Canada", "United States", "Brazil"};

                    List<string> statesBrazil = new(){"Amazonas", "São Paulo","Rio de Janeiro", "Ceará", "Goiás"};
                    List<string> statesEUA = new(){"California", "Orlando", "Miami", "Texas", "Colorado"};
                    List<string> statesCanada = new(){"Ontaro", "Quebec", "Nova Scotia", "New Brunswick", "Alberta"};
                    var CreatedBy = new IdentityUser(){UserName = "System"};
                    
                    for (var i = 0; i < 50; i++)
                    {
                        var Name = $"{names[random.Next(names.Count)]} {lastnames[random.Next(lastnames.Count)]}"; 
                        var Cpf = $"{random.Next(111, 1000)}{random.Next(111, 1000)}{random.Next(111, 1000)}{random.Next(1, 10)}";
                        // Convert.ToDateTime(DateTime.Today - new DateTime(1, 1, 1950)).Year
                        var Age = DateTime.Today.Year - random.Next(1950, DateTime.Today.Year-15);// Minimum age to work: 16 years
                        
                        var Stack = stacks[random.Next(stacks.Count)];
                        var Technologies = "";
                        if(Stack == "Back-end"){
                            Technologies = $"{technologiesBack[random.Next(6)]}, {technologiesBack[random.Next(6, technologiesBack.Count)]}"; 
                        }else if(Stack == "Front-end"){
                            Technologies = $"HTML & CSS, {technologiesFront[random.Next(technologiesFront.Count)]}";
                        }else{
                            Technologies = $"HTML & CSS, {technologiesFront[random.Next(technologiesFront.Count)]}, {technologiesBack[random.Next(technologiesBack.Count)]}";
                        }
                        
                        var Seniority = seniorities[random.Next(seniorities.Count)];

                        var Country = countries[random.Next(countries.Count)];
                        var State = "";
                        if (Country == "Brazil"){
                            State = statesBrazil[random.Next(statesBrazil.Count)];
                        }else if(Country == "United States"){
                            State = statesEUA[random.Next(statesEUA.Count)];
                        }else{
                            State = statesCanada[random.Next(statesCanada.Count)];
                        }

                        var Created = DateTime.Now;

                        context.Developers.Add(new Developer(){name = Name, cpf = Cpf, age = Age, stack = Stack, seniority = Seniority, technologies = Technologies, country = Country, state = State, created = Created, createdBy = CreatedBy});
                    }

                    context.SaveChanges();
                }

            }

        }
    }
}