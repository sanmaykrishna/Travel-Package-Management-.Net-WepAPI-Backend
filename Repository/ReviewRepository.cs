using BookingSystem.Data;
using BookingSystem.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSystem.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly CombinedDbContext _context;

        public ReviewRepository(CombinedDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddReviewAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateReviewAsync(int reviewID, int rating, string comment, DateTime timeStamp)
        {
            var review = await _context.Reviews.FindAsync(reviewID);
            if (review == null) return 0;

            review.Rating = rating;
            review.Comment = comment;
            review.TimeStamp = timeStamp;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteReviewAsync(int reviewID)
        {
            var review = await _context.Reviews.FindAsync(reviewID);
            if (review == null) return 0;

            _context.Reviews.Remove(review);
            return await _context.SaveChangesAsync();
        }

        public async Task<Review> GetReviewByIdAsync(int reviewID)
        {
            return await _context.Reviews.FindAsync(reviewID);
        }

        public async Task<List<Review>> GetAllReviewsAsync()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<int> ReviewCountAsync()
        {
            return await _context.Reviews.CountAsync();
        }

        public async Task<List<Review>> FetchReviewsByPackageIDAsync(int packageID)
        {
            return await _context.Reviews
                .Where(r => r.PackageID == packageID)
                .ToListAsync();
        }

        public async Task<List<Review>> FetchReviewsByUserAsync(int userID)
        {
            return await _context.Reviews
                .Where(r => r.UserID == userID)
                .ToListAsync();
        }

        public async Task<List<Review>> FetchReviewsByRatingAsync(int rating)
        {
            return await _context.Reviews
                .Where(r => r.Rating == rating)
                .ToListAsync();
        }

        public async Task<List<Review>> FetchRecentReviewsAsync(int count)
        {
            return await _context.Reviews
                .OrderByDescending(r => r.TimeStamp)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<Review>> FetchTopRatedReviewsAsync(int count)
        {
            return await _context.Reviews
                .OrderByDescending(r => r.Rating)
                .Take(count)
                .ToListAsync();
        }

        public async Task<double> FetchAverageRatingAsync(int packageID)
        {
            return await _context.Reviews
                .Where(r => r.PackageID == packageID)
                .AverageAsync(r => (double?)r.Rating) ?? 0.0;
        }

        public async Task<List<Review>> FetchReviewsByKeywordAsync(string keyword)
        {
            return await _context.Reviews
                .Where(r => r.Comment.Contains(keyword))
                .ToListAsync();
        }

        public async Task<List<Review>> FetchTopRatedFoodReviewsAsync(int count)
        {
            return await _context.Reviews
                .Where(r => r.FoodReview.HasValue)
                .OrderByDescending(r => r.FoodReview)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<Review>> FetchTopRatedFlightReviewsAsync(int count)
        {
            return await _context.Reviews
                .Where(r => r.FlightReview.HasValue)
                .OrderByDescending(r => r.FlightReview)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<Review>> FetchTopRatedHotelReviewsAsync(int count)
        {
            return await _context.Reviews
                .Where(r => r.HotelReview.HasValue)
                .OrderByDescending(r => r.HotelReview)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<Review>> FetchTopRatedTravelAgentReviewsAsync(int count)
        {
            return await _context.Reviews
                .Where(r => r.TravelAgentReview.HasValue)
                .OrderByDescending(r => r.TravelAgentReview)
                .Take(count)
                .ToListAsync();
        }
    }
}
