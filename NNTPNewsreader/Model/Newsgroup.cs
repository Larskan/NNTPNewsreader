using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNTPNewsreader.Model
{
    public class Newsgroup
    {
        //Example of group: 211 890 150694 151583 dk.test | Code articleTotal articleFirst articleLast groupName
        private string? _groupName;
        private int _articleTotal;
        private int _articleFirst;
        private int _articleLast;
        private bool _hasMetaData;

        #region Getters and Setters
        public string GroupName
        {
            get { return _groupName; }
            private set { _groupName = value; }
        }

        public int ArticleTotal
        {
            get { return _articleTotal; }
            private set { _articleTotal = value; }
        }

        public int ArticleFirst
        {
            get { return _articleFirst; }
            private set { _articleFirst = value; }
        }
        public int ArticleLast
        {
            get { return _articleLast; }
            private set { _articleLast = value; }
        }

        public bool HasMetaData
        {
            get { return _hasMetaData; }
            private set { _hasMetaData = value; }

        }
        #endregion

        public Newsgroup(string group)
        {
            GroupName = group;
        }

        /// <summary>
        /// Used for the Listview, MetaData is to confirm that it isn't empty
        /// </summary>
        /// <param name="total"></param> Total Articles within a group
        /// <param name="first"></param> First Article within a group
        /// <param name="last"></param> Last Article within a group
        public void SetGroupInfo(int total, int first, int last)
        {
            ArticleTotal = total;
            ArticleFirst = first;
            ArticleLast = last;
            HasMetaData = true;
        }
    }
}
