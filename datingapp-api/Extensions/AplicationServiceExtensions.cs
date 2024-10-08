﻿using datingapp_api.Data.Entities;
using datingapp_api.Helpers;
using datingapp_api.Interfaces;
using datingapp_api.Repositories;
using datingapp_api.Services;
using Microsoft.EntityFrameworkCore;

namespace datingapp_api.Extensions
{
    public static class AplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DatingAppContext>(option =>
            {
                option.UseSqlServer(config.GetConnectionString("SqlServerConnection"));
            });
            //add DI
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILikesRepository, LikesRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddScoped<LogUserActivity>();

            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));

            return services;
        }
    }
}
