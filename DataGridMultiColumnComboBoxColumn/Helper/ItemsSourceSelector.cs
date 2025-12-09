using Syncfusion.Maui.Core.Carousel;
using Syncfusion.Maui.DataGrid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using DataGridMultiColumnComboBoxColumn;

namespace DataGridMultiColumnComboBoxColumn
{
    internal class ItemsSourceSelector : IItemsSourceSelector
    {
        public IEnumerable GetItemsSource(object record, object dataContext)
        {
            if (record == null)
                return null;

            var orderinfo = record as Orders;
            var countryName = orderinfo.ShipCountry;

            var viewModel = dataContext as OrdersViewModel;

            //Returns ShipCity collection based on ShipCountry.
            if (viewModel.ShipCityItemsSources.ContainsKey(countryName))
            {
                ObservableCollection<ShipCityEntry> shipCities = null;
                viewModel.ShipCityItemsSources.TryGetValue(countryName, out shipCities);
                return shipCities.ToList();
            }

            return null;
        }
    }
}
