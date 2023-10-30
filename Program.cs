
//--------------------------
//---Money Mashine-----------
//---Program.CS ---


using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Channels;
using System.Threading.Tasks;
using MM_project;
using System.Xml.Linq;
using System.Security.Principal;
using System.Net.Http.Headers;
using MM_project;
//
//(using Accounts;

namespace MM_project
{
    class Program

    {
        static void Main()

        {

            //List<Accounts> accountItems = new List<Accounts>(); // List contining accounts
            List<AccountClasses> accountItems = new List<AccountClasses>(); // List contining accounts

            accountItems.Add(new AccountClasses(1, "Adam", 125, new DateTime(2023, 12, 01), "In"));
            accountItems.Add(new AccountClasses(2, "Bertil", 125, new DateTime(2023, 12, 02), "In"));
            //accountItems.Add(account1);



            //accountItems.Add(new AccountClasses(1, "Adam", 125, new DateTime(2023, 12, 01), "In"));
            //accountItems.Add(new AccountClasses(2, "Bertil", 125, new DateTime(2023, 12, 02), "In"));
            //}


            while (true)
            {

                Display.DisplayMenu();

                string choice = Console.ReadLine();

                switch (choice)
                // Using switch/case 
                {

                    case "1":
                        {
                            AccountClasses.AddAccount(accountItems); //  method for adding accounts
                            break;
                        }

                    case "2":
                        {
                            AccountClasses.ShowAccount(accountItems); // method for showwing accounts
                            break;
                        }

                    case "3":
                        {
                            AccountClasses.ChangeAmountOnAccount(accountItems); // method for adding or reducing the amount of the  accounts
                            break;
                        }

                    case "4":
                        {
                            AccountClasses.EditorDeleteAccount(accountItems); // method for adding or reducing the amount of the  accounts
                            break;
                        }

                    case "5":
                        {
                            AccountClasses.LoadTransactionsFromFile(accountItems); // method for adding or reducing the amount of the  accounts
                            break;
                        }

                    case "6":
                        {
                            AccountClasses.SaveAccountsToFile(accountItems); // method for adding or reducing the amount of the  accounts
                            break;
                        }

                    case "7":
                        {
                            AccountClasses.EditDeleteAccountsFromFile(accountItems);
//                            AccountClasses.SaveAccountsToFile(accountItems); // method for adding or reducing the amount of the  accounts
                            break;
                        }
                    case ("q" or "8"):
                        {
                            //Accounts.ShowAccount(accountItems); // method for showwing accounts
                            //Console.ReadKey();
                            AccountClasses.SaveAccountsToFile(accountItems); // method for saving to file
                            return; // Exit the program
                        }

                    default:
                        {
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                        }
                }
            }
        }

    }

}
