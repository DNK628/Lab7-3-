using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using RestaurantGuide.Models;
using RestaurantGuide.Data;
using MyApp.Data;

namespace RestaurantGuide
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            LoadRestaurants();
        }

        private void LoadRestaurants()
        {
            using var db = new AppDbContext();
            RestaurantList.ItemsSource = db.Restaurants.ToList();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var newRestaurant = new Restaurant
            {
                Name = "Новий ресторан",
                Address = "Адреса",
                AverageBill = 100
            };

            using var db = new AppDbContext();
            db.Restaurants.Add(newRestaurant);
            db.SaveChanges();
            LoadRestaurants();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (RestaurantList.SelectedItem is Restaurant selected)
            {
                using var db = new AppDbContext();
                var restaurant = db.Restaurants.FirstOrDefault(r => r.Id == selected.Id);
                if (restaurant != null)
                {
                    restaurant.Name = "Змінено";
                    restaurant.AverageBill += 10;
                    db.SaveChanges();
                }
                LoadRestaurants();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (RestaurantList.SelectedItem is Restaurant selected)
            {
                using var db = new AppDbContext();
                var restaurant = db.Restaurants.FirstOrDefault(r => r.Id == selected.Id);
                if (restaurant != null)
                {
                    db.Restaurants.Remove(restaurant);
                    db.SaveChanges();
                }
                LoadRestaurants();
            }
        }
    }
}
