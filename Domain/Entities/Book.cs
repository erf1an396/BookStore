using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Book : EntityBase
    {
        public string Title { get; set; }

        public Author Author { get; set; }

        public int AuthorId { get; set; }

        public string Publisher { get; set; }

        public int Publication_Year { get; set; }

        public string? Isbn { get; set; }

        public BookLanguageEnum Language { get; set; }

        public int Pages { get; set; }

        public int Price { get; set; }

        public  string? Description { get; set; }

        public string Review { get; set; }


        #region relations

        public Category  Category { get; set; }

        public int CategoryId { get; set; }

        public ICollection<BookPhoto> BookPhtotos { get; set; }


        #endregion



    }
}
