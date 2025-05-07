using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.WebHostEnvironment
{
    public interface IWebHostEnvironmentAccessor
    {
        string WebRootPath { get; }
    }
}
