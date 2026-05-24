using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterView.Application.Helper
{
    public static class MyServiceProvider
    {
        private static IServiceProvider serviceProvider { get; set; }
        public static IServiceCollection Create(IServiceCollection sevice)
        {
            serviceProvider = sevice.BuildServiceProvider();
            return sevice;
        }

        public static T GetService<T>()
        {
            return serviceProvider.GetService<T>();
        }
    }
}
