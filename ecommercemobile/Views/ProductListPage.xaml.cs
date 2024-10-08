using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using ecommercemobile.Models;




namespace ecommercemobile.Views
{
    
    public partial class ProductListPage : ContentPage
    {
        public ObservableCollection<Product> Products { get; set; }
        public ProductListPage()
        {
            InitializeComponent();
            Products = new ObservableCollection<Product>();
            ProductsCollectionView.ItemsSource = Products;
            LoadProducts();
        }
        private async void LoadProducts()
        {
            try
            {
                using var httpClient = new HttpClient();
                var response = await httpClient.GetStringAsync("http://localhost:9090/api/products");
                var productList = JsonSerializer.Deserialize<ObservableCollection<Product>>(response);

                foreach (var product in productList)
                {
                    Products.Add(product);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Could not load products: {ex.Message}", "OK");
            }
        }
    
}
}