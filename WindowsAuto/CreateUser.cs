using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.PortableExecutable;
using System.Management.Automation;
using System.Security;

namespace WindowsAuto
{
    internal class CreateUser
    {

        public SecureString Password { get; set; }

        private string UserName { get; set; }

        private string UserGroup { get; set; }



        public CreateUser(string userName, string userGroup, SecureString password = null)
        {
            Password = password;
            UserName = userName;
            UserGroup = userGroup;
        }


        public void AddUser()
        {
            Console.WriteLine("Attempting to create user.");

            PowerShell ps = PowerShell.Create();

            try
            {

                if (Password != null)
                {
                    ps.AddCommand("New-LocalUser")
                    .AddParameter("Name", UserName)
                    .AddParameter("Password", Password)
                    .AddParameter("Description", "Administrative purposes! To be deleted soon...")
                    .Invoke();
                }
                else
                {
                    ps.AddCommand("New-LocalUser")
                    .AddParameter("Name", UserName)
                    .AddParameter("Description", "Administrative purposes! To be deleted soon...")
                    .AddParameter("NoPassword")
                    .Invoke();
                }
            }
            catch (Exception ex)
            { 
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error has occured : {ex.Message}");
                Console.ResetColor();
            }

        }

        public void AddUserToGroup()
        {
            Console.WriteLine("Attempting to make user administrator!");
            PowerShell ps = PowerShell.Create();

            try 
            {
                ps.AddCommand("Add-LocalGroupMember")
                    .AddParameter("Group", UserGroup)
                    .AddParameter("Member", UserName)
                    .Invoke();

                Console.WriteLine("User should now be an ADMIN! CAUTION, THIS USER HAS COMPLETE CONTROL!");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error has occured : {ex.Message}");
                Console.ResetColor();
            }

        }


        public void RemoveUser()
        {
            Console.WriteLine("Attempting the removal of an user!");

            PowerShell ps = PowerShell.Create();


            try
            {
                ps.AddCommand("Remove-LocalUser")
                    .AddParameter("Name", UserName)
                    .Invoke();

                Console.WriteLine("The user has been removed!");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error has occured : {ex.Message}");
                Console.ResetColor();
            }
        }

        public SecureString ConvertToSecureString(string password)
        {
            if (password == null)
                throw new ArgumentNullException("password");

            var securePassword = new SecureString();

            foreach (char c in password)
                securePassword.AppendChar(c);

            securePassword.MakeReadOnly();
            return securePassword;
        }


    }
}
