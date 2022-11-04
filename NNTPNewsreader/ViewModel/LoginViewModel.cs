using NNTPNewsreader.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNTPNewsreader.ViewModel
{
    public class LoginViewModel : Bindable
    {
        private string server;
        private string user;
        private string pass;

        #region Getter and Setters
        public string Server { get { return server; } set { server = value; propertyIsChanged(); } }
        public string User { get { return user; } set { user = value; propertyIsChanged(); } }
        public string Pass { get { return pass; } set { pass = value; propertyIsChanged(); } }
        #endregion

        /// <summary>
        /// Grabs the data used in the Login Screen to connect to the News Server
        /// </summary>
        /// <returns></returns>
        public bool ConnectToServer()
        {
            ILoginDataSaving loginData = new LoginDataInfo();
            loginData.SaveLogin(new LoginData(Server, User, Pass));

            return NNTPConnection.Connect(Server, User, Pass);
        }

        /// <summary>
        /// Going to be used for when data is saved for next session
        /// </summary>
        public void GrabSavedLoginData()
        {
            ILoginDataSaving loginData = new LoginDataInfo();
            LoginData login = loginData.GetLoginData();

            Server = login.Server;
            User = login.Username;
            Pass = login.Password;

        }

    }
}
