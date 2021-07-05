using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Blog.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Data.SeedData.json
{
    public class Seeder
    {
        static string path = Directory.GetParent(Directory.GetCurrentDirectory()) + "\\Blog.Data\\SeedData.json\\";

        private const string regularPassword = "P@ssw0rd";

        public async static void EnsurePopulated(IApplicationBuilder app)
        {
            //Get the Db context
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<BlogDbContext>();

            
            if (context.AppUsers.Any())
            {
                return;
            }
            else
            {
                //Get Usermanager
                var userManager = app.ApplicationServices.CreateScope()
                                              .ServiceProvider.GetRequiredService<UserManager<AppUser>>();

               
                //Seed Users
                var appUsers = GetSampleData<AppUser>(File.ReadAllText($"{path}User.json"));
                

                foreach (var item in appUsers)
                {
                    await userManager.CreateAsync(item, regularPassword);
                   
                }

            }

        }

        //Get sample data from json files
        private static List<T> GetSampleData<T>(string filelocation)
        {
            var output = JsonSerializer.Deserialize<List<T>>(filelocation, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return output;
        }

    }
}
