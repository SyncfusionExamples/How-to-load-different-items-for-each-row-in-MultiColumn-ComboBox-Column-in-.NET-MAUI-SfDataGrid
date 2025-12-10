using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using DataGridMultiColumnComboBoxColumn;
using System.Diagnostics;

namespace DataGridMultiColumnComboBoxColumn
{
    /// <summary>
    /// ViewModel used to generate records for datagrid and MultiColumnComboBox Column
    /// </summary>
    public class OrdersViewModel : INotifyPropertyChanged
    {
        #region Fields

        private ObservableCollection<Orders> orders;
        private string[] Customers = new string[]
        {
            "ALFKI",
            "ANATR",
            "ANTON",
            "AROUT",
            "BERGS",
            "BLAUS",
            "BLONP",
            "BOLID",
            "BONAP",
            "BOTTM",
            "BSBEV",
            "CACTU",
            "CENTC",
            "CHOPS",
            "COMMI",
            "CONSH",
            "DRACD",
            "DUMON",
            "EASTC",
            "ERNSH"
        };

        private string[] ShipCountry = new string[]
        {
            "Argentina",
            "Austria",
            "Belgium",
            "Brazil",
            "Canada",
            "Denmark",
            "Finland",
            "France",
            "Germany",
            "Ireland",
            "Italy",
            "Mexico",
            "Norway",
            "Poland",
            "Portugal",
            "Spain",
            "Sweden",
            "Switzerland",
            "UK",
            "USA",
            "Venezuela"
        };

        private void SetShipCity()
        {
            ShipCity.Clear();

            string[] argentina = new string[] { "Buenos Aires" };

            string[] austria = new string[] { "Graz", "Salzburg" };

            string[] belgium = new string[] { "Bruxelles", "Charleroi" };

            string[] brazil = new string[] { "Campinas", "Resende", "Rio de Janeiro", "São Paulo" };

            string[] canada = new string[] { "Montréal", "Tsawassen", "Vancouver" };

            string[] denmark = new string[] { "Århus", "København" };

            string[] finland = new string[] { "Helsinki", "Oulu" };

            string[] france = new string[] { "Lille", "Lyon", "Marseille", "Nantes", "Paris", "Reims", "Strasbourg", "Toulouse", "Versailles" };

            string[] germany = new string[] { "Aachen", "Berlin", "Brandenburg", "Cunewalde", "Frankfurt a.M.", "Köln", "Leipzig", "Mannheim", "München", "Münster", "Stuttgart" };

            string[] ireland = new string[] { "Cork" };

            string[] italy = new string[] { "Bergamo", "Reggio Emilia", "Torino" };

            string[] mexico = new string[] { "México D.F." };

            string[] norway = new string[] { "Stavern" };

            string[] poland = new string[] { "Warszawa" };

            string[] portugal = new string[] { "Lisboa" };

            string[] spain = new string[] { "Barcelona", "Madrid", "Sevilla" };

            string[] sweden = new string[] { "Bräcke", "Luleå" };

            string[] switzerland = new string[] { "Bern", "Genève" };

            string[] uk = new string[] { "Colchester", "Hedge End", "London" };

            string[] usa = new string[] { "Albuquerque", "Anchorage", "Boise", "Butte", "Elgin", "Eugene", "Kirkland", "Lander", "Portland", "San Francisco", "Seattle", "Walla Walla" };

            string[] venezuela = new string[] { "Barquisimeto", "Caracas", "I. de Margarita", "San Cristóbal" };

            ShipCity.Add("Argentina", argentina);
            ShipCity.Add("Austria", austria);
            ShipCity.Add("Belgium", belgium);
            ShipCity.Add("Brazil", brazil);
            ShipCity.Add("Canada", canada);
            ShipCity.Add("Denmark", denmark);
            ShipCity.Add("Finland", finland);
            ShipCity.Add("France", france);
            ShipCity.Add("Germany", germany);
            ShipCity.Add("Ireland", ireland);
            ShipCity.Add("Italy", italy);
            ShipCity.Add("Mexico", mexico);
            ShipCity.Add("Norway", norway);
            ShipCity.Add("Poland", poland);
            ShipCity.Add("Portugal", portugal);
            ShipCity.Add("Spain", spain);
            ShipCity.Add("Sweden", sweden);
            ShipCity.Add("Switzerland", switzerland);
            ShipCity.Add("UK", uk);
            ShipCity.Add("USA", usa);
            ShipCity.Add("Venezuela", venezuela);
        }

        #endregion

        #region ItemsSource
        public OrdersViewModel()
        {
            SetShipCity();
            BuildShipCityItemsSources();
            orders = BuildOrders();
        }

        public Dictionary<string, string[]> ShipCity = new Dictionary<string, string[]>();

        public Dictionary<string, ObservableCollection<ShipCityEntry>> ShipCityItemsSources { get; private set; } = new();

        private void BuildShipCityItemsSources()
        {
            ShipCityItemsSources.Clear();
            foreach (var kvp in ShipCity)
            {
                var country = kvp.Key;
                var cities = kvp.Value ?? Array.Empty<string>();
                var list = new ObservableCollection<ShipCityEntry>();
                for (int i = 0; i < cities.Length; i++)
                {
                    list.Add(new ShipCityEntry
                    {
                        ShipCountry = country,
                        ShipCity = cities[i],
                        IndexInCountry = i,
                        IsPrimary = i == 0
                    });
                }
                ShipCityItemsSources[country] = list;
            }
            OnPropertyChanged(nameof(ShipCityItemsSources));
        }
        
        public ObservableCollection<ShipCityEntry> GetShipCityItems(string shipCountry)
        {
            if (string.IsNullOrWhiteSpace(shipCountry))
                return new ObservableCollection<ShipCityEntry>();
            return ShipCityItemsSources.TryGetValue(shipCountry, out var list)
                ? list
                : new ObservableCollection<ShipCityEntry>();
        }

        /// <summary>
        /// 100 unique Orders for the main grid.
        /// </summary>
        public ObservableCollection<Orders> Orders
        {
            get => orders;
            private set
            {
                if (orders != value)
                {
                    orders = value;
                    OnPropertyChanged(nameof(Orders));
                }
            }
        }

        private ObservableCollection<Orders> BuildOrders()
        {
            var baseDate = new DateTime(2021, 01, 01);

            var list = new ObservableCollection<Orders>();
            for (int i = 0; i < 100; i++)
            {
                var country = ShipCountry[i % ShipCountry.Length];
                var city = ShipCity.TryGetValue(country, out var cities) && cities.Length > 0 ? cities[0] : "Unknown";

                list.Add(new Orders
                {
                    OrderID = 10001 + i,
                    SupplierID = 700 + (i % 25),
                    CustomerID = Customers[(i * 7) % Customers.Length],
                    ShipCity = city,
                    ShipCountry = country,
                    Freight = 10.0 + (i % 20) * 2.5,
                    ShippingDate = baseDate.AddDays(i),
                    Delivered = (i % 2 == 0),
                });
            }

            return list;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
#endregion