using System;
using System.ComponentModel;

namespace DataGridMultiColumnComboBoxColumn;

public class ShipCityEntry : INotifyPropertyChanged
{
    #region Fields

    private string? shipCity;        
    private string? shipCountry;     
    private int indexInCountry;      
    private bool isPrimary;

    #endregion

    /// <summary>
    /// Represents the method that will handle the <see cref="E:System.ComponentModel.INotifyPropertyChanged.PropertyChanged"></see> event raised when a property is changed on a component
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    #region Public Properties
    /// <summary>
    /// Gets or sets the value of ShipCity and notifies user when value gets changed
    /// </summary>
    public string? ShipCity
    {
        get
        {
            return this.shipCity;
        }

        set
        {
            this.shipCity = value;
            this.RaisePropertyChanged("ShipCity");
        }
    }

    /// <summary>
    /// Gets or sets the value of ShipCountry and notifies user when value gets changed
    /// </summary>
    public string? ShipCountry
    {
        get
        {
            return this.shipCountry;
        }

        set
        {
            this.shipCountry = value;
            this.RaisePropertyChanged("ShipCountry");
        }
    }

    /// <summary>
    /// Gets or sets the value of IndexInCountry and notifies user when value gets changed
    /// </summary>
    public string? IndexInCountry
    {
        get
        {
            return this.indexInCountry;
        }

        set
        {
            this.indexInCountry = value;
            this.RaisePropertyChanged("IndexInCountry");
        }
    }

    /// <summary>
    /// Gets or sets the value of IsPrimary and notifies user when value gets changed
    /// </summary>
    public string? IsPrimary
    {
        get
        {
            return this.isPrimary;
        }

        set
        {
            this.isPrimary = value;
            this.RaisePropertyChanged("IsPrimary");
        }
    }

#endregion

    #region INotifyPropertyChanged implementation

    /// <summary>
    /// Triggers when Items Collections Changed.
    /// </summary>
    /// <param name="name">string type parameter name</param>
    private void RaisePropertyChanged(string name)
    {
        if (this.PropertyChanged != null)
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }

    #endregion
}
