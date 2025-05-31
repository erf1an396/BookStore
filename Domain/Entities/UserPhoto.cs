using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserPhoto : EntityBase
    {
        public string Name { get; set; }

        public string Extenstion { get; set; }

        public Guid  UserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
