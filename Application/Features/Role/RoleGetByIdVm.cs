using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Role
{
    public class RoleGetByIdVm
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<ControllerVM> AllControllerVms { get; set; }

        public List<RoleClaimVM> RoleClaimVM {  get; set; }

    }
}
