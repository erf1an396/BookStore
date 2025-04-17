using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OryPersianDateTime;

namespace Domain.Common
{
    public class EntityBase
    {

        public int Id { get; set; }

        public PersianDateTime CreatedDate { get; set; }

        public PersianDateTime? LastModifiedDate { get; set; }

    }
}
