using NNTPNewsreader.ViewModel;
using System;
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

namespace NNTPNewsreader.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        LoginViewModel viewModel;


        public Login()
        {
            InitializeComponent();

            viewModel = new LoginViewModel();
            this.DataContext = viewModel; //Makes sure that the LoginViewModel is used in this datacontext

            viewModel.GrabSavedLoginData(); //Makes the saved login data the default thanks to the init keyword

        }

        private void onLoginClick(object sender, RoutedEventArgs e)
        {
            if (viewModel.ConnectToServer())
            {
                MainWindow mw = new();
                mw.Show();
                Close();
            }
            else
                MessageBox.Show("Failed Login.", "INFO");
        }

    }
}
