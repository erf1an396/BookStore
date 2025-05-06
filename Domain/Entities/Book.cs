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

        public string Author { get; set; }

        public string Publisher { get; set; }

        public int Publication_Year { get; set; }

        public string? Isbn { get; set; }

        public BookLanguageEnum Language { get; set; }

        public int Pages { get; set; }

        public  string? Description { get; set; }


        public Category  Category { get; set; }

        public int CategoryId { get; set; }



        


    }
}
