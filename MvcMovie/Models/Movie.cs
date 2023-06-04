using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
	public class Movie
	{
		public int MovieID { get; set; }
		[Display(Name = "Film Title")]
		public string Title { get; set; }
		[Display(Name = "Release Date")]
		[DataType(DataType.Date)]
		[DisplayFormat(ApplyFormatInEditMode = true,
		  DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime ReleaseDate { get; set; }
		public string Genre { get; set; }
		public int? Price { get; set; }
		public int RatingID { get; set; }

		public Rating? Rating { get; set; }
	}
}

