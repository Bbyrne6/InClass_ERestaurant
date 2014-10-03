using eRestaurant.DAL;
using eRestaurant.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using eRestaurant.Entities.DTOs; //needed for the Lambda version of the .include() method

namespace eRestaurant.BLL
{
    [DataObject]
    public class MenuController
    {
    [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Item> ListMenuItems()
        {
            using (var context = new RestaurantContext())
            {
                //get the item data and include the Category Data for each item
                return context.Items.Include(x => x.Category).ToList();
                //The .Include() method on the DBset<T> class performs eager loading of data. 
            }
        }

    [DataObjectMethod(DataObjectMethodType.Select, false)]
    public List<Category> ListCategorizedMenuItems()
    {
        using (var context = new RestaurantContext())
        {
            var data = from cat in context.MenuCategories
                       orderby cat.Description
                       select new Category()
                       {
                           Description = cat.Description,
                           MenuItems = from item in cat.MenuItems
                                       where item.Active
                                       orderby item.Description
                                       select new MenuItem()
                                       {
                                           Description = item.Description,
                                           Price = item.CurrentPrice,
                                           Calories = item.Calories,
                                           Comment = item.Comment
                                       }
                       };
            return data.ToList();
        }
    }

    }
}
