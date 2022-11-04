using NNTPNewsreader;
using NNTPNewsreader.Model;
using NNTPNewsreader.View;
using NNTPNewsreader.ViewModel;


namespace Testing
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestGetConnection()
        {
            //Act
            bool expected = true;

            bool actual = NNTPConnection.Connect("news.sunsite.dk","lars16n6@easv365.dk", "528a61");
            //Assert
            Assert.AreEqual(expected, actual);            
        }

        /*
        [Test]
        public void TestPostArticle()
        {
            //Act
            int expected = 240;

            int actual = new PostArticleViewModel("dk.test").PostArticle();

            //Assert
            Assert.AreEqual(actual == expected);

        }
        */
    }
}