using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace StartMvc.Models{
    public class MovieGenreModelView
    {
        public List<Movie> Movies { get; set; }

        public SelectList Genres { get; set; }

        public string MovieGenre  { get; set; }
    }
}