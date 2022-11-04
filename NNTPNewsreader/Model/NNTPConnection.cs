using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace NNTPNewsreader.Model
{
    public static class NNTPConnection
    {
        static TcpClient? tcpClient; //? = null conditional operator, basically: Return null if TcpClient is null
        static NetworkStream? stream;

        /// <summary>
        /// Connects to news.sunsite.dk with the login
        /// </summary>
        /// <param name="host"></param> news.sunsite.dk
        /// <param name="user"></param> lars16n6@easv365.dk
        /// <param name="pass"></param> 528a61
        /// <returns></returns>
        public static bool Connect(string host, string user, string pass)
        {
            //1) Create the socket that is connected to server on specified port, host being sunsite
            tcpClient = new TcpClient(host, 119);

            //2) Get the streams
            stream = tcpClient.GetStream();

            //Use the Stream reader and prevents Code 281 from ending in Groups Listview
            StreamReader sr = new StreamReader(stream);
            string? line = sr.ReadLine();

            //If connection works, we can proceed to login
            Authenticate(user, pass);
            return true;
        }

        /// <summary>
        /// Checks if our login details are correct
        /// </summary>
        /// <param name="user"></param> lars16n6@easv365.dk
        /// <param name="pass"></param> 528a61
        public static void Authenticate(string user, string pass)
        {
            StreamWriter sw = new StreamWriter(stream);
            StreamReader sr = new StreamReader(stream);

            //Write the username
            sw.Write("authinfo user " + user + "\r\n"); //\r\n means new line that works for all Windows, \n would be for Unix
            sw.Flush(); //Flush clears buffers, and causes buffered data to be written to underlying system

            //Prevents the Code 381 from being added as a newsgroup in Listview
            string? line = sr.ReadLine();

            //Write the password
            sw.Write("authinfo pass " + pass + "\r\n"); //\r\n means new line that works for all Windows, \n would be for Unix
            sw.Flush(); //Flush clears buffers, and causes buffered data to be written to underlying system

            //Prevents the Code 280 from being added as a newsgroup in Listview
            line = sr.ReadLine();

        }

        /// <summary>
        /// When an article is selected, this method shows the body of the articles
        /// </summary>
        /// <param name="articles"></param>
        /// <returns></returns>
        public static Articles GetArticles(Articles articles)
        {
            StreamReader sr = new StreamReader(stream);
            StreamWriter sw = new StreamWriter(stream);

            //Ask server for the body of the article
            sw.Write("Body " + articles.ID + "\r\n");
            sw.Flush();
            while (!stream.DataAvailable) ;

            //Get body from article
            string body = "";
            while (true)
            {
                string? line = sr.ReadLine();

                //Stop the reading at the end of body(the dot) or Code 423(error)
                if (line.StartsWith(".") || line.StartsWith("423"))
                    break;

                body += line;
            }      
            articles.Body = body;
            return articles;
        }

        /// <summary>
        /// Placed articles from a Newsgroup in a Listview
        /// </summary>
        /// <param name="ng"></param> Newsgroup
        /// <returns></returns>
        public static List<Articles> GetArticlesInGroup(Newsgroup ng)
        {
            StreamWriter sw = new StreamWriter(stream);
            StreamReader sr = new StreamReader(stream);

            List<Articles> articles = new List<Articles>();

            //Select group
            sw.Write("Group " + ng.GroupName + "\r\n");
            sw.Flush();

            //Grab all articles from the first one to the last one
            for (int i = ng.ArticleFirst; i < ng.ArticleLast; i++)
            {
                sw.Write("head \r\n"); //Grab the Subject
                sw.Write("next \r\n"); //Continue to next article
                sw.Flush();

                Articles a = new Articles();
                while (true)
                {
                    string? line = sr.ReadLine();
                    if (line.StartsWith("."))
                        break;
                    if (line.StartsWith("221")) //Means that command was successful, so add ID to a
                        a.ID = Int32.Parse(line.Split(' ')[1]);
                    if (line.StartsWith("Subject")) //Needed to show to Listview, added to a
                        a.Subject = line.Substring(9);
                    if (line.StartsWith("From")) //Needed to show to Listview, added to a
                        a.From = line.Substring(6);
                }
                articles.Add(a); //adds the ID, Subject, From of all articles to articles listview
            }
            return articles;
        }

        /// <summary>
        /// MetaData is data within data, basically checks if the groups are empty or not, if they are empty, no need to show anything
        /// </summary>
        /// <param name="ng"></param> Newsgroup
        /// <returns></returns>
        public static Newsgroup GetGroupMetaData(Newsgroup ng)
        {
            //Send request
            StreamWriter sw = new StreamWriter(stream);
            sw.Write("Group " + ng.GroupName + "\r\n");
            sw.Flush();

            //Wait for response
            while (!stream.DataAvailable) ;
            StreamReader sr = new StreamReader(stream);

            //Handle response
            string[]? line = sr.ReadLine()?.Split(' '); //Splits based on spaces between the total, first and last article numbers

            int total = Int32.Parse(line[1]);
            int first = Int32.Parse(line[2]);
            int last = Int32.Parse(line[3]);

            ng.SetGroupInfo(total, first, last);

            return ng;
        }

        /// <summary>
        /// Listview of Newsgroups
        /// </summary>
        /// <returns></returns>
        public static List<Newsgroup> GetNewsgroups()
        {
            //Send request
            StreamWriter sw = new StreamWriter(stream);
            sw.Write("List\r\n");
            sw.Flush();

            StreamReader sr = new StreamReader(stream);
            List<Newsgroup> groups = new List<Newsgroup>();

            while (true)
            {
                string? line = sr.ReadLine();
                if (line.StartsWith("."))
                    break;

                Newsgroup ng = new Newsgroup(line.Split(' ')[0]);
                groups.Add(ng);
            }
            return groups;
        }

        /// <summary>
        /// Post Article to a group
        /// </summary>
        /// <param name="ng"></param> Newsgroup: dk.test
        /// <param name="subject"></param> Subject
        /// <param name="from"></param> From = lars16n6@easv365.dk
        /// <param name="body"></param> What the article contains
        /// <returns></returns>
        /// <exception cref="InvalidDataException"></exception>
        public static int PostArticle(string ng, string subject, string from, string body)
        {
            StreamWriter sw = new StreamWriter(stream);
            StreamReader sr = new StreamReader(stream);

            sw.Write("post\r\n"); //command needed in NNTP
            sw.Flush();

            //Read the current lines
            string? line = sr.ReadLine();

            sw.Write("from: " + from + "\r\n");
            sw.Write("subject: " + subject + "\r\n");
            sw.Write("Newsgroups: " + "dk.test" + "\r\n");
            sw.Write("\r\n");
            sw.Write(body);
            sw.Write("\r\n.\r\n"); //Cant end without the dot
            sw.Flush();


            //Handle responses, we want Code 240
            line = sr.ReadLine();
            Debug.WriteLine(line);
            if (line.StartsWith("240"))
                return 240;
            else
                return -1;
        }
    }
}
