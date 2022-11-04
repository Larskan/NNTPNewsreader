using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNTPNewsreader.Model
{
    public class LoginData
    {
        //init keyword = accessor method, assigns value to data ONLY during construction, once initialized, it cant be changed
        //If init was someone born in 1993, then you could not change it later to 2022, it would be stuck at 1993
        //Which is why init can be used to save logindata

        #region Getters and inits
        public string Server { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }
        #endregion

        /// <summary>
        /// Basically what is needed to login
        /// </summary>
        /// <param name="server"></param> news.sunsite.dk
        /// <param name="username"></param> mine is: XXXXX@easv365.dk
        /// <param name="password"></param> mine is: ?????
        public LoginData(string server, string username, string password)
        {
            Server = server;
            Username = username;
            Password = password;
        }
    }
}
