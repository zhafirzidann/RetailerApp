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
    internal class UserRuleViewModel : INotifyPropertyChanged
    {
        public UserRuleViewModel()
        {
            usrucoll = new ObservableCollection<UserRule>();
            dbconn = new Db_Connection();
            model = new UserRule();

            InsertCommand = new RelayCommand(async () => await InsertDataAsync());
            UpdateCommand = new RelayCommand(async () => await UpdateDataAsync());
            DeleteCommand = new RelayCommand(async () => await DeleteDataAsync());
            SelectCommand = new RelayCommand(async () => await ReadDataAsync());
            SelectCommand.Execute(null);
        }

        public RelayCommand InsertCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand SelectCommand { get; set; }

        public ObservableCollection<UserRule> usrucoll
        {
            get
            {
                return usrucoll;
            }
            set
            {
                usrucoll = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(null));
            }
        }

        public UserRule Model
        {
            get
            {
                return Model;
            }
            set
            {

                if (value != null)
                {
                    UsruChecked = (value.Status == "Active") ? true : false;
                }
                model = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(null));
            }
        }

        public bool UsruChecked
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

        public event PropertyChangedEventHandler PropertyChanged;
        public event Action OnCallBack;

        private readonly Db_Connection dbconn;
        private ObservableCollection<UserRule> usrucoll;
        private Product model;
        private bool check;

        private async Task ReadDataAsync()
        {
            dbconn.OpenConnection();

            await Task.Delay(0);
            var query = "SELECT * FROM usersrule";
            var sqlcmd = new SqlCommand(query, dbconn.SqlConnect);
            var sqlresult = sqlcmd.ExecuteReader();

            if (sqlresult.HasRows)
            {
                usrucoll.Clear();
                while (sqlresult.Read())
                {
                    usrucoll.Add(new UserRule
                    {
                        Uid = sqlresult[0].ToString(),
                        User = sqlresult[1].ToString(),
                        Menu = sqlresult[2].ToString(),
                        Access = sqlresult[3].ToString(),
                    });
                }
            }
            dbconn.CloseConnection();
            OnCallBack?.Invoke();
        }

        private async Task InsertDataAsync()
        {
            dbconn.OpenConnection();
            var state = check ? "1" : "0";
            var query = $"INSERT INTO usersrule VALUES (" +
                        $"'{model.User}', " +
                        $"'{model.Menu}', " +
                        $"'{model.Access}', " +
                        $"'{state}')";
            var sqlcmd = new SqlCommand(query, dbconn.SqlConnect);
            sqlcmd.ExecuteNonQuery();
            dbconn.CloseConnection();
            await ReadDataAsync();
        }

        private async Task UpdateDataAsync()
        {
            if (IsValidating())
            {
                dbconn.OpenConnection();
                var state = check ? "1" : "0";
                var query = $"UPDATE usersrule SET " +
                            $"name = '{model.User}', " +
                            $"'{model.Menu}', " +
                            $"'{model.Access}', " +
                            $"status = '{state}' " +
                            $"WHERE uid = '{model.Uid}'";
                var sqlcmd = new SqlCommand(query, dbconn.SqlConnect);
                sqlcmd.ExecuteNonQuery();
                dbconn.CloseConnection();
                await ReadDataAsync();
            }
        }

        private async Task DeleteDataAsync()
        {
            if (IsValidating())
            {
                var msg = MessageBox.Show("Apakah Kamu Yakin", "Question",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (msg == MessageBoxResult.Yes)
                {
                    dbconn.OpenConnection();
                    var query = $"DELETE FROM usersrule WHERE uid = '{model.Uid}'";
                    var sqlcmd = new SqlCommand(query, dbconn.SqlConnect);
                    sqlcmd.ExecuteNonQuery();
                    dbconn.CloseConnection();
                }
                await ReadDataAsync();
            }
        }

        private bool IsValidating()
        {
            var flag = true;
            if (model.User == null)
            {
                MessageBox.Show("Teks 1 tidak boleh kosong!!", "Warning",
                                MessageBoxButton.OK,
                                MessageBoxImage.Exclamation);
                flag = false;
            }
            return flag;
        }



    }
}
