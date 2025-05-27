using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Author : EntityBase
    {
        public string Name { get; set; }

        public int Born_Year { get; set; }

        public BookLanguageEnum Language { get; set; }

        public string Description { get; set; }

        public int Book_Count { get; set; }

        public int Prize_Count { get; set; }

        public ICollection<Book> Books { get; set; }

        public ICollection<AuthorPhoto> AuthorPhotos { get; set; }
    }
}
