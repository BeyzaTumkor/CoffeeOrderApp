using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CoffeeOrder.Models;
using Newtonsoft.Json;
using Xamarin.Forms;


namespace CoffeeOrder.CrudOp
{
    public class Crud : ContentPage
    {
        static volatile Crud instance = null;
        static readonly object lockObject = new object();
        private List<Coffee.root> items;
        string url = "http://localhost:49473/api/";

        HttpClient client = new HttpClient();
        public static Crud GetInstance()
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new Crud();
                    }
                }
            }
            return instance;
        }
        public int GetSize(string extension)
        {
            GetInstance().Get(extension);
            Console.WriteLine(extension + items.Count);
            return items.Count;
        }

        public async void Get(string extension)
        {

            var uri = new Uri(url + extension);
            var response = await client.GetAsync(uri);
            items = new List<Coffee.root>();
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                items = JsonConvert.DeserializeObject<List<Coffee.root>>(content);
            }
        }

            public async void Get(CarouselView cv, string extension)
        {

            var uri = new Uri(url + extension);
            var response = await client.GetAsync(uri);
            items = new List<Coffee.root>();
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                items = JsonConvert.DeserializeObject<List<Coffee.root>>(content);
            }
            List<Models.Coffee.CoffeeViewModel> viewModels = new List<Models.Coffee.CoffeeViewModel>();
            foreach (var hd in items)
            {
                Models.Coffee.CoffeeViewModel cvm = new Models.Coffee.CoffeeViewModel(hd);
                viewModels.Add(cvm);

            }
            cv.ItemsSource = viewModels;
            if (items.Count == 0)
            {
                cv.IsVisible = false;
            }

        }


        public async void Get(ListView lw, string extension)
        {

            var uri = new Uri(url + extension);
            var response = await client.GetAsync(uri);
            items = new List<Coffee.root>();
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                items = JsonConvert.DeserializeObject<List<Coffee.root>>(content);
            }
            List<Models.Coffee.CoffeeViewModel> viewModels = new List<Models.Coffee.CoffeeViewModel>();
            foreach (var hd in items)
            {
                Models.Coffee.CoffeeViewModel cvm = new Models.Coffee.CoffeeViewModel(hd);
                viewModels.Add(cvm);

            }
           

            lw.ItemsSource = viewModels;
            
           

        }

        public async void GetnPost(string getExtension, string postExtension)
        {

            var uri = new Uri(url + getExtension);
            var response = await client.GetAsync(uri);
            items = new List<Coffee.root>();
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                items = JsonConvert.DeserializeObject<List<Coffee.root>>(content);
            }
            //Console.WriteLine(items.Count);
            foreach (var i in items)
            {
                GetInstance().Post(i, postExtension);
            }
            
            
        }

        public async void Post(Coffee.root item, string extension)
        {
            Uri uri = new Uri(url + extension);

            String json = JsonConvert.SerializeObject(item);
            StringContent strContent = new StringContent(json, Encoding.UTF8, "application/json");

            HttpRequestMessage request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.RequestUri = uri;
            request.Content = strContent;

            HttpResponseMessage response = await client.SendAsync(request);
            Console.WriteLine(response);

        }

        public async void Put(Coffee.root item, string extension)
        {

            Uri uri = new Uri(url + extension);

            String json = JsonConvert.SerializeObject(item);
            StringContent strContent = new StringContent(json, Encoding.UTF8, "application/json");

            HttpRequestMessage request = new HttpRequestMessage();
            request.Method = HttpMethod.Put;
            request.RequestUri = uri;
            request.Content = strContent;

            HttpResponseMessage response = await client.SendAsync(request);

           
        }

        public async void Delete(Coffee.root item, string extension)
        {
            Uri uri = new Uri(url + extension);

            String json = JsonConvert.SerializeObject(item);
            StringContent strContent = new StringContent(json, Encoding.UTF8, "application/json");

            HttpRequestMessage request = new HttpRequestMessage();
            request.Method = HttpMethod.Delete;
            request.RequestUri = uri;
            request.Content = strContent;

            HttpResponseMessage response = await client.SendAsync(request);

        }

        public async void GetnDelete(string getExtension, string delExtension)
        {

            var uri = new Uri(url + getExtension);
            var response = await client.GetAsync(uri);
            items = new List<Coffee.root>();
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                items = JsonConvert.DeserializeObject<List<Coffee.root>>(content);
            }
            
            foreach (var i in items)
            {
                GetInstance().Delete(i, delExtension);
            }


        }
    }
}



