using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CoffeeOrder.Views
{
    public partial class Cart : ContentPage
    {
        public MainPage mp;
        public Customize cp;
        public Cart cartp;
        List<Models.Coffee.root> orders;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            CrudOp.Crud.GetInstance().Get(cartList, "cart");
         
        }

        public Cart()
        {
            InitializeComponent();
            CrudOp.Crud.GetInstance().Get(cartList, "cart");
           
        }

        async void orderButton_Clicked(System.Object sender, System.EventArgs e)
        {
            int size = CrudOp.Crud.GetInstance().GetSize("cart");
            if(size == 0)
            {
                await DisplayAlert("", "Your cart is empty", "OK");
            }
            else
            {
                CrudOp.Crud.GetInstance().GetnPost("cart", "recentOrders");
                CrudOp.Crud.GetInstance().GetnDelete("cart", "cart");
                await DisplayAlert("", "Your order was placed!", "YAY!");
                mp = new MainPage();
                await Navigation.PushAsync(mp);
            }

            
        }

        async void delete_Clicked(System.Object sender, System.EventArgs e)
        {
            cartp = new Cart();
            var mi = ((MenuItem)sender);
            Models.Coffee.CoffeeViewModel d = (Models.Coffee.CoffeeViewModel)mi.CommandParameter;
            Models.Coffee.root drink = new Models.Coffee.root();
            drink.id = d.id;
            drink.name = d.name;
            drink.imagePath = d.imagePath;
            drink.milk = d.milk;
            drink.size = d.size;
            drink.espressoShots = d.espressoShots;
            drink.flavor = d.flavor;
            CrudOp.Crud.GetInstance().Delete(drink, "cart/"+ drink.id);
            await Navigation.PushAsync(cartp);

        }

        async void edit_Clicked(System.Object sender, System.EventArgs e)
        {

            var mi = ((MenuItem)sender);
            Models.Coffee.CoffeeViewModel d = (Models.Coffee.CoffeeViewModel)mi.CommandParameter;
            Models.Coffee.root drink = new Models.Coffee.root();
            drink.id = d.id;
            drink.name = d.name;
            drink.imagePath = d.imagePath;
            drink.milk = d.milk;
            drink.size = d.size;
            drink.espressoShots = d.espressoShots;
            drink.flavor= d.flavor;
            cp = new Customize(drink);
            await Navigation.PushAsync(cp);
        }

        async void keepShopping_Clicked(System.Object sender, System.EventArgs e)
        {
            mp = new MainPage();
            await Navigation.PushAsync(mp);
        }
    }
}
