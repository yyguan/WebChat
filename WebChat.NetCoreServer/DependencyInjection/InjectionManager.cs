using Logic;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebChat.NetCoreServer.DependencyInjection
{
    public  class InjectionManager
    {
        public static void InjectionClass(IServiceCollection services)
        {
            services.AddScoped<UserLogic, UserLogic>();

        }
    }
}
