using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ApplicationUserRole : IdentityUserRole<Guid>
    {
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ApplicationRole ApplicationRole { get; set; }
    }
}
