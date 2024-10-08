using ecommercemobile.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
namespace ecommercemobile
{
    public partial class MainPage : ContentPage
    {
        public List<Product> Products { get; set; }

        public MainPage()
        {
            InitializeComponent();
            LoadProducts();
            BindingContext =this;
        }

        private async void ShowProducts_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.ProductListPage());
        }

        private async Task LoadProducts()
        {
            try
            {
                using var htttpClient = new HttpClient();


                var response = await htttpClient.GetStringAsync("http://localhost:9090/api/products");


                var productList = JsonSerializer.Deserialize<List<Product>>(response);

                foreach (var product in productList)
                {
                    Console.WriteLine($"Product: {product.Name}, Price : {product.Price}");
                }

                ProductionCollection.ItemsSource = productList;
            }
            catch (HttpRequestException httpEx)
            {
                // Eğer HTTP isteğinde bir sorun varsa, detaylı hata mesajını göstermek için.
                await DisplayAlert("HTTP Error", $"Could not load products: {httpEx.Message}", "OK");
            }
            catch (JsonException jsonEx)
            {
                // JSON parse hataları için.
                await DisplayAlert("JSON Error", $"Could not parse products: {jsonEx.Message}", "OK");
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error loading products: {ex.Message}");
                await DisplayAlert("Error", $"Cloud not load products : {ex.Message}", "OK");


            }
        }



    } 
}
