using Application.Contracts.WebHostEnvironment;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.WebHostEnvironment
{
    public class WebHostEnvironmentAccessor : IWebHostEnvironmentAccessor
    {
        private readonly IWebHostEnvironment _env;

        public WebHostEnvironmentAccessor(IWebHostEnvironment env)
        {
            _env = env;
        }

        public string WebRootPath => _env.WebRootPath;
    }
}
