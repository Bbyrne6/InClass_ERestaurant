using eRestaurant.DAL;
using eRestaurant.Entities.POCOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRestaurant.BLL
{
    [DataObject]
    public class ReportController
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<CategoryMenuItem> GetReportCategoryMenuItem()
        {
            using (RestaurantContext context = new RestaurantContext())
    {
             //This query is for pylling out data to be used in a 
            //details report, The query gets all the menu items
            // and their categories, sorting them by the category
            //description and then by the menu item descrition. 
        var results = from cat in context.Items
                      orderby cat.Category.Description, cat.Description
                      select new CategoryMenuItem()
                      {
                          CategoryDescription = cat.Category.Description,
                          ItemDescription = cat.Description,
                          Price = cat.CurrentPrice,
                          Calories = cat.Calories,
                          Comment = cat.Comment
                      };
                return results.ToList();
    }
    }
    }
}
