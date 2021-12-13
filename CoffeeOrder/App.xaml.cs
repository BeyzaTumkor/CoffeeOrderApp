using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoffeeOrder
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

          

            
            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.FromHex("#f7e7ce"),
                BarTextColor = Color.FromHex("#92623a"),

            };
            
        
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
