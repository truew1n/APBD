using System;
using System.Diagnostics.Metrics;
using LegacyApp;

namespace UnitTests
{
    internal class Program
    {
        static int TestCounter = 0;
        static void Apply(bool Result, bool Truth)
        {
            if(Result == Truth)
            {
                Console.WriteLine("Test: " + TestCounter + " Passed!");
            }
            else
            {
                Console.WriteLine("Test: " + TestCounter + " Failed!");
            }
            TestCounter++;
        }

        static void Main(string[] args)
        {
            UserService userService = new UserService();
            Apply(userService.AddUser("John", "Doe", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 1), true);
            Apply(userService.AddUser("John", null, "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 1), false);
            Apply(userService.AddUser(null, "Doe", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 1), false);
            Apply(userService.AddUser(null, null, "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 1), false);
            Apply(userService.AddUser("John", "Doe", "johndoe@gmailcom", DateTime.Parse("1982-03-21"), 1), true);
            Apply(userService.AddUser("John", "Doe", "johndoegmail.com", DateTime.Parse("1982-03-21"), 1), true);
            Apply(userService.AddUser("John", "Doe", "johndoegmailcom", DateTime.Parse("1982-03-21"), 1), false);
            Apply(userService.AddUser("John", "Doe", "johndoe@gmail.com", DateTime.Parse("2007-03-21"), 1), false);
        }
    }
}
