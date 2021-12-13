using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeOrder.Views;
using Xamarin.Forms;

namespace CoffeeOrder
{
    public partial class MainPage : ContentPage
    {
        public Cart cartPage;
        public Customize CustomizePage;
        public MainPage mp;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CrudOp.Crud.GetInstance().Get(recentOrders, "recentOrders");
            CrudOp.Crud.GetInstance().Get(favs, "favs");


            if (!favs.IsVisible)
            {
                noFavsLabel.IsVisible = true;
                selectFavLabel.IsVisible = true;

            }
            if (!recentOrders.IsVisible)
            {
                noRecents.IsVisible = true;
            }
        }

        public MainPage()
        {

            InitializeComponent();
            LoadHotList();
            LoadIcedList();
            CrudOp.Crud.GetInstance().Get(recentOrders, "recentOrders");
            CrudOp.Crud.GetInstance().Get(favs, "favs");
           
        }

        void LoadIcedList()
        {
            List<Models.Coffee.root> icedDrinks = new List<Models.Coffee.root>();

            Models.Coffee.root icedCoffee = new Models.Coffee.root();
            icedCoffee.name = "Iced Coffee";
            icedCoffee.milk = "Whole Milk";
            icedCoffee.imagePath = "icedCoffee.jpg";



            Models.Coffee.root icedLatte = new Models.Coffee.root();
            icedLatte.name = "Iced Latte";
            icedLatte.milk = "Whole Milk";
            icedLatte.espressoShots = "Espresso Shots: 2";
            icedLatte.imagePath = "icedLatte.jpg";

            Models.Coffee.root coldBrew = new Models.Coffee.root();
            coldBrew.name = "Cold Brew";
            coldBrew.imagePath = "coldBrew.jpg";

            Models.Coffee.root icedMach = new Models.Coffee.root();
            icedMach.name = "Iced Macchiato";
            icedMach.espressoShots = "Espresso Shots: 2";
            icedMach.milk = "Whole Milk";
            icedMach.imagePath = "icedMach.jpg";

            Models.Coffee.root icedCap = new Models.Coffee.root();
            icedCap.name = "Iced Cappucino";
            icedCap.espressoShots = "Espresso Shots: 2";
            icedCap.milk = "Whole Milk";
            icedCap.imagePath = "icedCap.jpg";

            Models.Coffee.root icedAmer = new Models.Coffee.root();
            icedAmer.name = "Iced Americano";
            icedAmer.espressoShots = "Espresso Shots: 2";
            icedAmer.imagePath = "icedAmer.jpg";

            icedDrinks.Add(icedCoffee);
            icedDrinks.Add(icedLatte);
            icedDrinks.Add(coldBrew);
            icedDrinks.Add(icedMach);
            icedDrinks.Add(icedCap);

            List<Models.Coffee.CoffeeViewModel> viewModels = new List<Models.Coffee.CoffeeViewModel>();
            foreach (var iced in icedDrinks)
            {
                Models.Coffee.CoffeeViewModel cvm = new Models.Coffee.CoffeeViewModel(iced);
                viewModels.Add(cvm);

            }
            iced.ItemsSource = viewModels;
            
        }

        void LoadHotList()
        {
            List<Models.Coffee.root> hotDrinks = new List<Models.Coffee.root>();

            Models.Coffee.root latte = new Models.Coffee.root();
            latte.name = "Latte";
            latte.milk = "Whole Milk";
            latte.espressoShots = "Espresso Shots: 2";
            latte.imagePath = "latte.jpg";
            

            Models.Coffee.root regCoffee = new Models.Coffee.root();
            regCoffee.name = "Coffee";
            regCoffee.imagePath = "regCoffee.jpg";
           

            Models.Coffee.root americano = new Models.Coffee.root();
            americano.name = "Americano";
            americano.espressoShots = "Espresso Shots: 2";
            americano.imagePath = "americano.jpg";

            Models.Coffee.root cappuccino = new Models.Coffee.root();
            cappuccino.name = "Cappuccino";
            cappuccino.milk = "Whole Milk";
            cappuccino.espressoShots = "Espresso Shots: 2";
            cappuccino.imagePath = "cap.jpeg";
        
            Models.Coffee.root macchiato = new Models.Coffee.root();
            macchiato.name = "Macchiato";
            macchiato.milk = "Whole Milk";
            macchiato.espressoShots = "Espresso Shots: 2";
            macchiato.imagePath = "mach.jpg";
            

            Models.Coffee.root espressoShot = new Models.Coffee.root();
            espressoShot.name = "Espresso";
            espressoShot.espressoShots = "Espresso Shots: 1";
            espressoShot.imagePath = "shot.jpg";

            hotDrinks.Add(latte);
            hotDrinks.Add(regCoffee);
            hotDrinks.Add(americano);
            hotDrinks.Add(cappuccino);
            hotDrinks.Add(macchiato);
            hotDrinks.Add(espressoShot);



            List<Models.Coffee.CoffeeViewModel> viewModels = new List<Models.Coffee.CoffeeViewModel>();
            foreach ( var hd in hotDrinks )
            {
                Models.Coffee.CoffeeViewModel cvm = new Models.Coffee.CoffeeViewModel(hd);
                viewModels.Add(cvm);

            }

            hot.ItemsSource = viewModels;
        }

        async void cartButton_Clicked(System.Object sender, System.EventArgs e)
        {
            cartPage = new Cart();
            await Navigation.PushAsync(cartPage);
        }

        async void Item_Tapped(System.Object sender, System.EventArgs e)
        {

            Models.Coffee.root drink = (Models.Coffee.root)((TappedEventArgs)e).Parameter;
            CustomizePage = new Customize(drink);
            await Navigation.PushAsync(CustomizePage);
        }

        async void favs_Tapped(System.Object sender, System.EventArgs e)
        {
            
            Models.Coffee.root drink = (Models.Coffee.root)((TappedEventArgs)e).Parameter;

            String answer = await DisplayActionSheet("What would you like to do with this item?", "Cancel", null, "Delete", "Order");
            

            if (answer == "Delete")
            {
                CrudOp.Crud.GetInstance().Delete(drink, "favs");
                CrudOp.Crud.GetInstance().Get(favs, "favs");

            }else if(answer == "Order")
            {
                drink.size = null;
                CustomizePage = new Customize(drink);
                await Navigation.PushAsync(CustomizePage);
            }


        }
        async void Recents_Tapped(System.Object sender, System.EventArgs e)
        {
            
            Models.Coffee.root drink = (Models.Coffee.root)((TappedEventArgs)e).Parameter;
            String answer = await DisplayActionSheet("What would you like to do with this item?", "Cancel", null, "Add to Favorites", "Order Again", "Delete");


            if (answer == "Delete")
            {
                CrudOp.Crud.GetInstance().Delete(drink, "recentOrders");
                CrudOp.Crud.GetInstance().Get(recentOrders, "recentOrders");
               
            }
            else if (answer == "Order Again")
            {
                drink.size = null;
                CustomizePage = new Customize(drink);
                await Navigation.PushAsync(CustomizePage);
            }
            else if(answer == "Add to Favorites")
            {
                CrudOp.Crud.GetInstance().Post(drink, "favs");
                CrudOp.Crud.GetInstance().Get(favs, "favs");
                
            }


        }

        async void refresh_Refreshing(Object sender, EventArgs e)
        {
            
            mp = new MainPage();
            refresh.IsRefreshing = false;
            await Navigation.PushAsync(mp);
        }
    }
}
