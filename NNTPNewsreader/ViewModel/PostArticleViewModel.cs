using NNTPNewsreader.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNTPNewsreader.ViewModel
{
    public class PostArticleViewModel : Bindable
    {
        private string subject;
        private string body;
        private string chosenGroup;

        public string Subject { get { return subject; } set { subject = value; propertyIsChanged(); } }
        public string Body { get { return body; } set { body = value; propertyIsChanged(); } }
        public string ChosenGroup { get { return chosenGroup; } set { chosenGroup = value; propertyIsChanged(); } }

        public PostArticleViewModel(string group)
        {
            ChosenGroup = group;
        }

        public int PostArticle()
        {
            return NNTPConnection.PostArticle(ChosenGroup, Subject, "lars16n6@easv.dk", Body);
        }
    }
}
