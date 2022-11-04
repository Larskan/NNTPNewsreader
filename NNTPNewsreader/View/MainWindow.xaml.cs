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
using System.Windows.Navigation;
using System.Windows.Shapes;
using NNTPNewsreader.Model;
using NNTPNewsreader.ViewModel;

namespace NNTPNewsreader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel viewModel;
       
        public MainWindow()
        {
            InitializeComponent();

            viewModel = new MainWindowViewModel();
            this.DataContext = viewModel; //Makes sure that the MainWindowViewModel is used in this datacontext
        }

    
        private void onGetNewsgroupsClick(object sender, RoutedEventArgs e)
        {
            viewModel.GetGroups();

        }

        private void onGetArticlesClick(object sender, RoutedEventArgs e)
        {
            //Picks the group, you the user, has selected on the Newsgroup ListView
            Newsgroup ng = (Newsgroup)ListNewsgroup.SelectedItem; 
            if (ng is null) //Assuming you try to show articles without selecting a group
            {
                MessageBox.Show("Select a Newsgroup.", "INFO");
                return;
            }
            viewModel.GetArticles(ng);

        }

        private void onGetArticleClick(object sender, RoutedEventArgs e)
        {
            Articles a = (Articles)ListArticles.SelectedItem; //Grabs the singular article from the list of articles within a group
            if (a is null)
            {
                MessageBox.Show("Select an Article", "INFO");
                return;
            }
            viewModel.GetArticle(a);

        }

        private void onUploadArticleClick(object sender, RoutedEventArgs e)
        {
            Newsgroup ng = (Newsgroup)ListNewsgroup.SelectedItem; //Selected group where you want to upload an article
            if (ng is null)
            {
                MessageBox.Show("Select a news group.", "INFO");
                return;
            }
            viewModel.PostArticleWindow(ng); //Launches the PostArticle.xaml
        }

        private bool find(string str)
        {
            foreach (ListViewItem item in ListNewsgroup.Items)
            {
                if (item.Content.Equals(str))
                    return true;
            }
            return false;
        }
        private void select(string str)
        {
            foreach(ListViewItem item in ListNewsgroup.Items)
            {
                if (item.Content.Equals(str))
                    item.IsSelected = true;
                else
                    item.IsSelected = false;
            }

        }

        //Need to look into wildmat patterns, to see if I can search through the connection with wildmat
        private void onSearchClick(object sender, RoutedEventArgs e)
        {
            Newsgroup ng = new(SearchBox.Text);
            /*
            if (find(SearchBox.Text))
                select(SearchBox.Text);
            else
                MessageBox.Show("Not found");
            */

            //Does not work with Newsgroup.cs
            //foreach (ListViewItem lvi in ListNewsgroup.Items)
                if (ng.Equals(SearchBox.Text))
                {
                    

                }
              
                    
            

        }

        private void onInfoClick(object sender, RoutedEventArgs e)
        {
            //Fun button
            MessageBox.Show("Why did you click this button?\r\nThis is a Newsreader connecting to Sunsite\r\nMade by Lars");
        }
    }
}
