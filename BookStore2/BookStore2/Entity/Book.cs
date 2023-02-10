using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace BookStore2.Entity
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public bool IsPublished { get; set; } = true;
        public Genre Genre { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        public IList<BookAuthor> BookAuthors { get; set; } = null!;
    }
}
