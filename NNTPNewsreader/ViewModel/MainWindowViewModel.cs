using NNTPNewsreader.Model;
using NNTPNewsreader.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NNTPNewsreader.ViewModel
{
    public class MainWindowViewModel : Bindable
    {
        private ObservableCollection<Newsgroup>? _newsGroups;
        private ObservableCollection<Articles>? _articles;
        private Articles? _articleVar;

        #region Getters and Setters
        public ObservableCollection<Newsgroup> Newsgroups
        {
            get { return _newsGroups; }
            set { _newsGroups = value; propertyIsChanged(); }
        }

        public ObservableCollection<Articles> Articles
        {
            get { return _articles; }
            set { _articles = value; propertyIsChanged(); }
        }

        public Articles Article
        {
            get { return _articleVar; }
            set { _articleVar = value; propertyIsChanged(); }
        }
        #endregion

        public MainWindowViewModel()
        {

        }

        public void GetGroups()
        {
            Newsgroups = new ObservableCollection<Newsgroup>(NNTPConnection.GetNewsgroups());
        }


        public void GetGroupMeta(Newsgroup ng)
        {
            NNTPConnection.GetGroupMetaData(ng);
        }

        public void GetArticles(Newsgroup ng)
        {
            if (!ng.HasMetaData)
                GetGroupMeta(ng);
            Articles = new ObservableCollection<Articles>(NNTPConnection.GetArticlesInGroup(ng));
        }
        public void GetArticle(Articles a)
        {
            Article = NNTPConnection.GetArticles(a);
            MessageBox.Show(Article.Body, "Article " + Article.ID);
        }

        public bool showingPostWindow = false;
        public void PostArticleWindow(Newsgroup ng)
        {
            if (!showingPostWindow)
            {
                PostArticle postView = new(() => { showingPostWindow = false; }, "Posting in: " + ng.GroupName);
                postView.Show();
                showingPostWindow = true;
            }
        }
    }
}
