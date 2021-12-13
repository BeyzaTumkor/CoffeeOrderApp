using System;
using System.Collections.Generic;
using CoffeeOrder.Models;
using CoffeeOrder.CrudOp;

using Xamarin.Forms;

namespace CoffeeOrder.Views
{
    

    public partial class Customize : ContentPage
    {
        public Coffee.root drink = new Coffee.root();
        public List<string> milks = new List<string>();
        public List<string> flavors = new List<string>();
        public List<string> size = new List<string>();
        public List<int> espresso = new List<int>();
        public Cart cartPage = new Cart();

        public Customize(Coffee.root d)
        {
            InitializeComponent();
            if(d.size == "Small" | d.size == "Medium" | d.size == "Large")
            {
                addFrame.IsVisible = false;
                updateFrame.IsVisible = true;
                cancelFrame.IsVisible = true;
            }
            else
            {
                addFrame.IsVisible = true;
                updateFrame.IsVisible = false;
                cancelFrame.IsVisible = false;
            }

            drink = d;
            CusPic.Source = drink.imagePath;
            name.Text = drink.name;

            milks.Add("No Milk");
            milks.Add("Whole Milk");
            milks.Add("Skim Milk");
            milks.Add("Soy Milk");
            milks.Add("Oat Milk");
            milks.Add("Almond Milk");
            milkPicker.ItemsSource = milks;
            milkPicker.Title = "Select a Milk";
            if(drink.milk != null)
            {
                milkPicker.SelectedItem = drink.milk;
            }

            size.Add("Small");
            size.Add("Medium");
            size.Add("Large");
            sizePicker.ItemsSource = size;
            sizePicker.Title = "Select a Size";
            if(drink.size != null)
            {
                sizePicker.SelectedItem = drink.size;
            }

            flavors.Add("No Flavor");
            flavors.Add("Peppermint");
            flavors.Add("Vanilla");
            flavors.Add("Raspberry");
            flavors.Add("Lavender");
            flavors.Add("Caramel");
            flavors.Add("Pumpkin Spice");
            flavors.Add("Hazelnut");
            flavorPicker.ItemsSource = flavors;
            flavorPicker.Title = "Select a Flavor";
            if(drink.flavor != null)
            {
                flavorPicker.SelectedItem = drink.flavor;
            }

            espresso.Add(0);
            espresso.Add(1);
            espresso.Add(2);
            espresso.Add(3);
            espresso.Add(4);
            espressoPicker.ItemsSource = espresso;
            espressoPicker.Title ="Select espresso shots";

         
            
            
        }

        async void addCart_Clicked(Object sender, EventArgs e)
        {
            if (sizePicker.SelectedItem == null)
            {
                await DisplayAlert("", "You need to select a size", "OK");
            }
            else
            {
                
                drink.size = sizePicker.SelectedItem.ToString();


                if (flavorPicker.SelectedItem != null)
                {
                    if(flavorPicker.SelectedItem.ToString() == flavors[0])
                    {
                        drink.flavor = null;
                    }
                    else
                    {
                        drink.flavor = flavorPicker.SelectedItem.ToString();

                    }
                }

                if (milkPicker.SelectedItem != null)
                {
                    if(milkPicker.SelectedItem.ToString() == milks[0])
                    {
                        drink.milk = null;
                    }
                    else
                    {
                        drink.milk = milkPicker.SelectedItem.ToString();
                    }
                    
                }

                if (espressoPicker.SelectedItem != null)
                {
                    drink.espressoShots = "Espresso Shots: " + espressoPicker.SelectedItem.ToString();
                }


                Crud.GetInstance().Post(drink, "cart");
                await Navigation.PushAsync(cartPage);
            }

        }

        async void save_Clicked(System.Object sender, System.EventArgs e)
        {
            if (sizePicker.SelectedItem == null)
            {
                await DisplayAlert("", "You need to select a size", "OK");
            }
            else
            {

                drink.size = sizePicker.SelectedItem.ToString();


                if (flavorPicker.SelectedItem != null)
                {
                    drink.flavor = flavorPicker.SelectedItem.ToString();
                }

                if (milkPicker.SelectedItem != null)
                {
                    drink.milk = milkPicker.SelectedItem.ToString();
                }

                if (espressoPicker.SelectedItem != null)
                {
                    drink.espressoShots = "Espresso Shots: " + espressoPicker.SelectedItem.ToString();
                }


                Crud.GetInstance().Put(drink, "cart/"+ drink.id);
                await Navigation.PushAsync(cartPage);
            }

        }

        async void cancel_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopAsync();
        }


    }
}
