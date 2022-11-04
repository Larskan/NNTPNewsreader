using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNTPNewsreader.Model
{
    public class Articles
    {
        //From Putty experimentation: Articles contain minimum = ID, Subject, From and Body
        private int _id;
        private string? _subject;
        private string? _from;
        private string? _body;

        #region Getters and Setters
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }

        public string From
        {
            get { return _from; }
            set { _from = value; }
        }

        public string Body
        {
            get { return _body; }
            set { _body = value; }
        }
        #endregion
    }
}
