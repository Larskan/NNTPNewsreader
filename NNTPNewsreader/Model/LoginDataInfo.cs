using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NNTPNewsreader.Model
{
    public class LoginDataInfo : ILoginDataSaving
    {
        //Server -> Username -> Password
        /// <summary>
        /// If data for login is saved, it through the Interface, grabs the login data
        /// </summary>
        /// <returns></returns>
        public LoginData GetLoginData()
        {
            //If login data does not exist, return new logindata as blanks
            if (!File.Exists("login.config"))
                return new LoginData("", "", "");

            //ReadAllText opens a File and reads all the text, then closes the file. Splits according to new lines(Server, user,pass)
            //Making it an array makes it so we can use "coordinates" to process it
            string[] content = File.ReadAllText("login.config").Split('\n');
            //If login data exists, return the 3 splits [0](Server),[1](User),[1](Pass)
            return new LoginData(content[0], content[1], content[2]);
        }

        /// <summary>
        /// Interaction with ILoginDataSaving
        /// </summary>
        /// <param name="ld"></param> LoginData
        public void SaveLogin(LoginData ld)
        {
            string data = ld.Server + "\n" + ld.Username + "\n" + ld.Password + "\n";
            //Through Interface, we save the Server, User and Pass in a File that temporarily gets called login.config
            File.WriteAllText("login.config", data);
        }
    }
}
