using System;
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
    public class RestaurantsController : ApiController
    {
        private topofContext db = new topofContext();

        // GET: api/Restaurants
        public IQueryable<Restaurant> GetRestaurants()
        {
            return db.Restaurants;
        }

        // GET: api/Restaurants/5
        [ResponseType(typeof(Restaurant))]
        public IHttpActionResult GetRestaurant(int id)
        {
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return Ok(restaurant);
        }

        // PUT: api/Restaurants/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRestaurant(int id, Restaurant restaurant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != restaurant.RestaurantID)
            {
                return BadRequest();
            }

            db.Entry(restaurant).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantExists(id))
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

        // POST: api/Restaurants
        [ResponseType(typeof(Restaurant))]
        public IHttpActionResult PostRestaurant(Restaurant restaurant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Restaurants.Add(restaurant);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = restaurant.RestaurantID }, restaurant);
        }

        // DELETE: api/Restaurants/5
        [ResponseType(typeof(Restaurant))]
        public IHttpActionResult DeleteRestaurant(int id)
        {
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            db.Restaurants.Remove(restaurant);
            db.SaveChanges();

            return Ok(restaurant);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RestaurantExists(int id)
        {
            return db.Restaurants.Count(e => e.RestaurantID == id) > 0;
        }

          //GET api/restaurants/3/cities
        [Route("api/restaurants/{cityId:int}/cities")]
        public IEnumerable<Restaurant> GetRestaurantsByCity(int cityId)
        {
            return db.Restaurants
                        .Where(restaurant => restaurant.CityID == cityId);
        }

            //GET api/restaurants/baklava/foods
        [Route("api/restaurants/{foodID:int}/{cityID:int}")]
        public IEnumerable<Restaurant> GetRestaurantsWithFood(int foodID, int cityID)
        {
            IEnumerable<Restaurant> restaurants = null;

            restaurants = (from food in db.Foods
                           join rating in db.Ratings
                           on food.FoodID equals rating.FoodID
                           join restaurant in db.Restaurants
                           on rating.RestaurantID equals restaurant.RestaurantID
                           join city in db.Cities
                           on restaurant.CityID equals city.CityID
                           where rating.FoodID == foodID && city.CityID == cityID
                           select restaurant).ToList();

            //restaurants = (from restaurant in db.Restaurants
            //               join rating in db.Ratings
            //               on restaurant.RestaurantID equals rating.RestaurantID
            //               join food in db.Foods
            //               on rating.FoodID equals food.FoodID
            //               where food.FoodID == foodID
            //               select restaurant).ToList();
            return restaurants;

        }
    }
}