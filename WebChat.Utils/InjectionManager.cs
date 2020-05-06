using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using Logic;
using Microsoft.Extensions.DependencyInjection;

namespace WebChat.Utils
{
    public class InjectionManager
    {
        public static void InjectionClass(IServiceCollection services)
        {
            services.AddScoped<UserLogic, UserLogic>();

        }
    }
}
