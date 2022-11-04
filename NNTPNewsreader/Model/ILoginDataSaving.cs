using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNTPNewsreader.Model
{
    //Interface creates a default implementation for the classes that uses it
    //This interface is focused entirely on saving login data
    public interface ILoginDataSaving
    {
        void SaveLogin(LoginData loginData);
        LoginData GetLoginData();
    }
}
