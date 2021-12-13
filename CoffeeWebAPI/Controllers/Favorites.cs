using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using CoffeeOrder.Models;

namespace CoffeeWebAPI.Controllers
{
    [Route("api/favs")]
    [ApiController]
    public class Favorites : ControllerBase
    {
      

        [HttpGet]
        public List<Coffee.root> Get()
        {
            return Startup.favDrinks;
        }

        [HttpGet("{id}")]
        public Coffee.root Get(String id)
        {
            int index = Startup.favDrinks.IndexOf(Startup.favDrinks.Where(x => x.id == id).FirstOrDefault());
            return Startup.favDrinks[index];
        }

        [HttpPost]
        public Coffee.root Post([FromBody] Coffee.root value)
        {
            Coffee.root temp = value;
            temp.id = Guid.NewGuid().ToString();
            Startup.favDrinks.Add(temp);
            return temp;
        }

        [HttpPut("{id}")]
        public Coffee.root Put(String id, [FromBody] Coffee.root value)
        {
            int index = Startup.favDrinks.IndexOf(Startup.favDrinks.Where(x => x.id == id).FirstOrDefault());
            Startup.favDrinks[index].name = value.name;
            Startup.favDrinks[index].size = value.size;
            Startup.favDrinks[index].flavor = value.flavor;
            Startup.favDrinks[index].milk = value.milk;
            Startup.favDrinks[index].espressoShots = value.espressoShots;
            return value;
        }

        [HttpDelete("{id}")]
        public String Delete(string id)
        {
            int index = Startup.favDrinks.IndexOf(Startup.favDrinks.Where(x => x.id == id).FirstOrDefault());
            Startup.favDrinks.RemoveAt(index);
            return "Id is " + id;
        }

        [HttpDelete()]
        public String Delete([FromBody] Coffee.root c)
        {
            int index = Startup.favDrinks.IndexOf(Startup.favDrinks.Where(x => x.id == c.id).FirstOrDefault());
            Startup.favDrinks.RemoveAt(index);
            return "Delete this drink " + c.ToString();
        }
    }
}
