using Retailer_App.Models;
using Retailer_App.Setup;
using System.Data.SqlClient;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retailer_App.ViewModels
{
    class InventoryViewModel
    {
        public bool ProChecked
        {
            get
            {
                return check;
            }
            set
            {
                check = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(null));
            }
        }
    }
}
