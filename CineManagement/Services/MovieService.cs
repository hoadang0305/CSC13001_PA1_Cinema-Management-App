﻿using CineManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CineManagement.Services
{
    public class MovieService
    {
        public List<Movie> getMovies()
        {
            using (var context = new CinemaManagementContext())
            {
                List<Movie> movies = context.Movies.Include(movie => movie.Director).ToList();

                if (movies == null)
                {
                    throw new Exception("No movie found.");
                }
                else
                {
                    foreach(Movie movie in movies)
                    {
                        movie.Actors = context.Entry(movie).Collection(m => m.Actors).Query().ToList();
                        movie.Genres = context.Entry(movie).Collection(m => m.Genres).Query().ToList();
                        movie.MovieInfo = context.MovieInfos
                            .Where(mi => mi.MovieId == movie.MovieId)
                            .FirstOrDefault();
                    }
                    return movies;
                }
            }
        }

        public Movie getMovieById(int id)
        {
            using (var context = new CinemaManagementContext())
            {
                Movie movie = context.Movies.FirstOrDefault(x => x.MovieId == id);

                if (movie == null)
                {
                    throw new Exception("Movie not found.");
                }
                else
                {
                    movie.Actors = context.Entry(movie).Collection(m => m.Actors).Query().ToList();
                    movie.Genres = context.Entry(movie).Collection(m => m.Genres).Query().ToList();
                    movie.MovieInfo = context.MovieInfos
                        .Where(mi => mi.MovieId == movie.MovieId)
                        .FirstOrDefault();

                    return movie;
                }
            }
        }
    }
}