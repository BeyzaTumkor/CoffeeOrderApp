using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using CoffeeOrder.Models;


namespace CoffeeWebAPI.Controllers
{
    
    [Route("api/cart")]
    [ApiController]
    public class Cart : ControllerBase
    {
      
        [HttpGet]
        public List<Coffee.root> Get()
        {
            Console.WriteLine(Startup.cartDrinks.Count);
            
            return Startup.cartDrinks;

        }

        [HttpGet("{id}")]
        public Coffee.root Get(String id)
        {

            int index = Startup.cartDrinks.IndexOf(Startup.cartDrinks.Where(x => x.id == id).FirstOrDefault());
            return Startup.cartDrinks[index];
        }

        [HttpPost]
        public Coffee.root Post([FromBody] Coffee.root value)
        {
          
            Coffee.root temp = value;
            temp.id = Guid.NewGuid().ToString();
            Startup.cartDrinks.Add(temp);
            Console.WriteLine(Startup.cartDrinks.Count);
            return temp;
        }

        [HttpPut("{id}")]
        public Coffee.root Put(String id, [FromBody] Coffee.root value)
        {
            int index = Startup.cartDrinks.IndexOf(Startup.cartDrinks.Where(x => x.id == id).FirstOrDefault());
            Startup.cartDrinks[index].name = value.name;
            Startup.cartDrinks[index].size = value.size;
            Startup.cartDrinks[index].flavor = value.flavor;
            Startup.cartDrinks[index].milk = value.milk;
            Startup.cartDrinks[index].espressoShots = value.espressoShots;
            return value;
        }

        [HttpDelete("{id}")]
        public String Delete(string id)
        {
            int index = Startup.cartDrinks.IndexOf(Startup.cartDrinks.Where(x => x.id == id).FirstOrDefault());
            Startup.cartDrinks.RemoveAt(index);
            return "Id is " + id;
        }

        [HttpDelete()] 
        public String Delete([FromBody] Coffee.root c)
        {
            int index = Startup.cartDrinks.IndexOf(Startup.cartDrinks.Where(x => x.id == c.id).FirstOrDefault());
            Startup.cartDrinks.RemoveAt(index);
            return "Delete this drink " + c.ToString();
        }
    }
}