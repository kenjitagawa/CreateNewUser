using System;
using System.Security;

namespace WindowsAuto
{
    public class Program
    {
        static void Main(string[] args)
        {

            CreateUser userCreator = new CreateUser("Ken", "Administrators");

            userCreator.Password = userCreator.ConvertToSecureString("1234");
            
            userCreator.AddUser();
            userCreator.AddUserToGroup();

            Console.WriteLine("Ready to delete? (Y/N)");
            string response = Console.ReadLine().ToLower();
            if (response == "y")
            { 
                userCreator.RemoveUser();
            }

        }
    }
}
