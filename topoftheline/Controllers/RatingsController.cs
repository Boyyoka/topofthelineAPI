﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using topoftheline;

namespace topoftheline.Controllers
{
    public class RatingsController : ApiController
    {
        private topofContext db = new topofContext();

        // GET: api/Ratings
        public IQueryable<Rating> GetRatings()
        {
            return db.Ratings;
        }

        // PUT: api/Ratings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRating(int id, Rating rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rating.FoodID)
            {
                return BadRequest();
            }

            db.Entry(rating).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Ratings
        [ResponseType(typeof(Rating))]
        public IHttpActionResult PostRating(Rating rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ratings.Add(rating);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (RatingExists(rating.FoodID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = rating.FoodID }, rating);
        }

        // DELETE: api/Ratings/5
        [ResponseType(typeof(Rating))]
        public IHttpActionResult DeleteRating(int id)
        {
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return NotFound();
            }

            db.Ratings.Remove(rating);
            db.SaveChanges();

            return Ok(rating);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RatingExists(int id)
        {
            return db.Ratings.Count(e => e.FoodID == id) > 0;
        }

        [Route("api/ratings/{foodID:int}/{restaurantID:int}")]
        public IEnumerable<Rating> GetRatings(int foodID, int restaurantID)
        {
            IEnumerable<Rating> ratings = null;

            ratings = (from rating in db.Ratings
                       join food in db.Foods
                       on foodID equals food.FoodID
                       join restaurant in db.Restaurants
                       on restaurantID equals restaurant.RestaurantID
                       where foodID == rating.FoodID && restaurantID == rating.RestaurantID
                       select rating).ToList();

            return ratings;
        }
    }
}