namespace OdeToFood.Migrations
{
	using OdeToFood.Models;
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OdeToFoodDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(OdeToFood.Models.OdeToFoodDb context)
        {
			context.Restaurants.AddOrUpdate(r => r.Name,
				new Restaurant { Name = "Green Bawarchi", City = "New Delhi", Country = "India" },
				new Restaurant { Name = "Punjabi Affairs", City = "Hyderabad", Country = "India" },
				new Restaurant
				{
					Name = "Great Lake",
					City = "Chicago",
					Country = "USA",
					Reviews = new List<RestaurantReview>
                    {
                        new RestaurantReview {Rating=9,Body="Awesome food!!",ReviewerName="Akshat" }
                    }
				});

			for (int i = 0; i < 1000; i++)
			{
				context.Restaurants.AddOrUpdate(r => r.Name,
				new Restaurant { Name = "Restaurant " + i.ToString(), City = "City " + i.ToString(), Country = "India" });
			}
        }
    }
}
