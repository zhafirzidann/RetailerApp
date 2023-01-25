﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Retailer_App.Views.Home;

namespace Retailer_App.Views.Home
{
    /// <summary>
    /// Interaction logic for LoginDialog.xaml
    /// </summary>
    public partial class LoginDialog : Window
    {
        public LoginDialog()
        {
            vm = new UserViewModel();
            InitializeComponent();
            vm.OnCallBack += Close;
            DataContext = vm;

            private readonly UsersViewModel vm;

            private void BtnClose_Click(object sender, RoutedEventArgs e)
        {

        }

            private void TxtPass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            vm.Model.Keypass = TxtPass.Password;
        }
        }
    }

