using Domain.Common;

namespace Domain.Entities
{
    public class BookPhoto : EntityBase
    {

        public string Name  { get; set; }

        public string Extenstion { get; set; }

        public int BookId { get; set; }


        public Book Book { get; set; }


    }
}
