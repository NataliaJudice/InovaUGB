using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace InnovaCore.Data.Context
{
    public static class DbInitializer
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // 1. DEFINIR AS ROLES (APENAS ADMIN E ALUNO)
            string[] roleNames = { "Admin", "Aluno" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // 2. CRIAR O ADMINISTRADOR DO SISTEMA
            var adminEmail = "admin@innocore.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var admin = new IdentityUser
                {
                    UserName = "admin_innocore",
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(admin, "Admin@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }

            // 3. CRIAR UM ALUNO DE TESTE
            var alunoEmail = "aluno@innocore.com";
            var alunoUser = await userManager.FindByEmailAsync(alunoEmail);

            if (alunoUser == null)
            {
                var aluno = new IdentityUser
                {
                    UserName = "aluno_teste",
                    Email = alunoEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(aluno, "Aluno@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(aluno, "Aluno");
                }
            }
        }
    }
}