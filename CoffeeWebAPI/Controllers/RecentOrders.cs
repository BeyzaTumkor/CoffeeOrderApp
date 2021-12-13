using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using CoffeeOrder.Models;

namespace CoffeeWebAPI.Controllers
{
    [Route("api/recentOrders")]
    [ApiController]
    public class RecentOrders : ControllerBase
    {
        

        [HttpGet]
        public List<Coffee.root> Get()
        {
            return Startup.recentOrderDrinks;
        }

        [HttpGet("{id}")]
        public Coffee.root Get(String id)
        {
            int index = Startup.recentOrderDrinks.IndexOf(Startup.recentOrderDrinks.Where(x => x.id == id).FirstOrDefault());
            return Startup.recentOrderDrinks[index];
        }

        [HttpPost]
        public Coffee.root Post([FromBody] Coffee.root value)
        {
            Coffee.root temp = value;
            temp.id = Guid.NewGuid().ToString();
            Startup.recentOrderDrinks.Add(temp);
            return temp;
        }

        [HttpPut("{id}")]
        public Coffee.root Put(String id, [FromBody] Coffee.root value)
        {
            int index = Startup.recentOrderDrinks.IndexOf(Startup.recentOrderDrinks.Where(x => x.id == id).FirstOrDefault());
            Startup.recentOrderDrinks[index].name = value.name;
            Startup.recentOrderDrinks[index].size = value.size;
            Startup.recentOrderDrinks[index].flavor = value.flavor;
            Startup.recentOrderDrinks[index].milk = value.milk;
            Startup.recentOrderDrinks[index].espressoShots = value.espressoShots;
            return value;
        }

        [HttpDelete("{id}")]
        public String Delete(string id)
        {
            int index = Startup.recentOrderDrinks.IndexOf(Startup.recentOrderDrinks.Where(x => x.id == id).FirstOrDefault());
            Startup.recentOrderDrinks.RemoveAt(index);
            return "Id is " + id;
        }

        [HttpDelete()]
        public String Delete([FromBody] Coffee.root c)
        {
            int index = Startup.recentOrderDrinks.IndexOf(Startup.recentOrderDrinks.Where(x => x.id == c.id).FirstOrDefault());
            Startup.recentOrderDrinks.RemoveAt(index);
            return "Delete this drink " + c.ToString();
        }
    }
}
