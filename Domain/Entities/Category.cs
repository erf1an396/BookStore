using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Category : EntityBase
    {
        public int? ParentId { get; set; }
        public string Title { get; set; }
        public virtual Category CategoryParent { get; set; }

        public ICollection<Category> CategoryChildren { get; set; } = new HashSet<Category>();

    }
}
