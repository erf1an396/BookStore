using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AuthorPhoto : EntityBase
    {
        public string Name { get; set; }

        public string Extenstion { get; set; }

        public int AuthorId { get; set; }


        public Author Author { get; set; }
    }
}
