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
using NNTPNewsreader.ViewModel;

namespace NNTPNewsreader.View
{
    /// <summary>
    /// Interaction logic for PostArticle.xaml
    /// </summary>
    public partial class PostArticle : Window
    {
        PostArticleViewModel viewModel;
        public PostArticle(Action onClose, string group)
        {
            InitializeComponent();
            viewModel = new(group);
            this.DataContext = viewModel; //Makes sure that the PostArticleViewModel is used in this datacontext

        }

        private void onPostClick(object sender, RoutedEventArgs e)
        {
            int result = viewModel.PostArticle();
            if (result is 240)
                MessageBox.Show("Article was successfully posted.", "INFO");
            else
                MessageBox.Show("Posting failed.", "ERROR");
            Close();

        }

        private void onCancelClick(object sender, RoutedEventArgs e)
        {
            Close();

        }
    }
}
