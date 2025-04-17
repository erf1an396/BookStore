using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Service.OutSide
{
    public interface ISignalRService
    {
        Task SendAsync(string method, object arg);
    }
}
