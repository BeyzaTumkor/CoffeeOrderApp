using System;

using Xamarin.Forms;

namespace CoffeeOrder.Models
{
    public class Coffee
    {
        public class root
        {

            public string id { get; set; }
            public string name { get; set; }
            public string size { get; set; }
            public string flavor { get; set; }
            public string milk { get; set; }
            public string espressoShots { get; set; }
            public string imagePath { get; set; }


        }

        public class CoffeeViewModel : root
        {
            public ImageSource imageSource
            {
                get
                {
                    return ImageSource.FromFile(imagePath);
                }
                
            }
            public CoffeeViewModel( Models.Coffee.root c)
            {
                this.espressoShots = c.espressoShots;
                this.flavor = c.flavor;
                this.id = c.id;
                this.imagePath = c.imagePath;
                this.milk = c.milk;
                this.name = c.name;
                this.size = c.size;
            }
                

        }
    }
}
    


