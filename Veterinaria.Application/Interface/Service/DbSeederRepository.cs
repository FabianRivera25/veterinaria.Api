using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Veterinaria.Application.Interface.Repository;
using Veterinaria.Domain.Entities;

namespace Veterinaria.Application.Interface.Service
{
    public class DbSeederRepository : IdbSeederRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbSeederRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeederAsync()
        {
            //se crearan los roles si estos no existen
            string[] roleNames = { "Admin", "Cliente", "Veterinario" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //si no hay un administrador en la base de datos, se creara uno por defecto
            if(!await _userManager.Users.AnyAsync())
            {
                var adminUser = new ApplicationUser
                {
                    NombreCompleto = "Administrador",
                    UserName = "adminVeterinaria@gmail.com",
                    Email = "adminVeterinaria@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    //FechaRegistro = DateTime.UtcNow,
                    //Estado = "Activo"
                };

                var resultado = await _userManager.CreateAsync(adminUser, "Admin123");
                if (resultado.Succeeded)
                {
                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

        }

    }
}
