//---AccountClasses.cs---


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;
using System.ComponentModel.Design;
using MM_project;


namespace MM_project
{
    internal class AccountClasses
    {

        static List<AccountClasses> accountItems = new List<AccountClasses>();

        public AccountClasses(int accountNumber, string ownerName, double amount, DateTime time, string inOut)
        {
            AccountNumber = accountNumber;
            OwnerName = ownerName;
            Amount = amount;
            Time = time;
            InOut = inOut;  // 'in' for income, 'out' for expense
        }

        public int AccountNumber { get; set; }
        public string OwnerName { get; set; }
        public double Amount { get; set; }
        public DateTime Time { get; set; }
        public string InOut { get; set; }

        public static void AddAccount(List<AccountClasses> AccountList)
        {
            string tempInOut;

            Console.Write("Enter Account Number: ");
            int accountNumber = int.Parse(Console.ReadLine());

            if (AccountList.Any(account => account.AccountNumber == accountNumber))
            {
                Console.WriteLine("An account with the same account number already exists.");
                return;
            }

            Console.Write("Account owner: ");
            string ownerName = Console.ReadLine();

            Console.Write("Amount: ");
            double amount = Convert.ToDouble(Console.ReadLine());

            if (amount > 0)
                tempInOut = "in";
            else
                tempInOut = "out";

            DateTime time = DateTime.Now;
            AccountClasses Account = new AccountClasses(accountNumber, ownerName, amount, time, tempInOut);
            AccountList.Add(Account);
            Console.WriteLine("Account added successfully.");
        }


        public static void ShowAccount(List<AccountClasses> AccountList)
        {
            Console.WriteLine("Saved accounts");
            Console.WriteLine("==============\n");

            foreach (AccountClasses testAccount in AccountList)
            {
                Console.WriteLine($"{testAccount.AccountNumber} : {testAccount.OwnerName} : {testAccount.Amount} : {testAccount.Time} : {testAccount.InOut}");
            }

            Console.WriteLine("Press a button to continue");
            Console.ReadLine();
        }


        public static void ChangeAmountOnAccount(List<AccountClasses> AccountList)
        {
            Console.Write("Enter account number: ");
            int searchForAccount = Convert.ToInt32(Console.ReadLine());

            foreach (AccountClasses testAccount in AccountList)
            {
                if (searchForAccount == testAccount.AccountNumber)
                {
                    Console.WriteLine($"{testAccount.AccountNumber} : {testAccount.OwnerName} : {testAccount.Amount} : {testAccount.Time} : {testAccount.InOut}");
                    Console.WriteLine();
                    Console.Write("Enter change of amount (positive or negative): ");
                    double changeOfAmount = Convert.ToDouble(Console.ReadLine());
                    testAccount.Amount = changeOfAmount + testAccount.Amount;
                    if (changeOfAmount < 0)
                        testAccount.InOut = "Out";
                    else
                        testAccount.InOut = "In";


                    Console.WriteLine();
                    Console.WriteLine("The new balance of the account is: " + testAccount.Amount);
                    break;
                }
                else
                {
                    Console.WriteLine("Account does not exist");
                }
            }
        }
        public static void EditorDeleteAccount(List<AccountClasses> AccountList)
        {
            Console.Write("Do you want to change (c) the name of the account owner or delete (d) the account?");
            string selection = Console.ReadLine();

            if (selection == "c" || selection == "d")
            {
                Console.Write("Enter the account number: ");
                int searchForAccount = Convert.ToInt32(Console.ReadLine());
                AccountClasses testAccount = AccountList.FirstOrDefault(a => a.AccountNumber == searchForAccount);

                if (testAccount != null)
                {
                    Console.WriteLine($"{testAccount.AccountNumber} : {testAccount.OwnerName} : {testAccount.Amount} : {testAccount.Time} : {testAccount.InOut}");
                    Console.WriteLine();

                    if (selection == "c")
                    {
                        Console.Write("Change the name of the owner: ");
                        string newName = Console.ReadLine();
                        testAccount.OwnerName = newName;
                    }
                    else if (selection == "d")
                    {
                        Console.Write("Account will be deleted. Are you sure of this?");
                        Console.Write("Type (d)elete to delete: ");
                        string deleteAccount = Console.ReadLine();
                        //int index = int.Parse(Console.ReadLine());
                        if ((deleteAccount == "delete") || (deleteAccount == "d"))
                        {
                            Console.WriteLine("Deleting account: " + testAccount.AccountNumber);
                            //  if(index >= 0 && ) 
                            {
                                //AccountList.RemoveAt();
                                AccountList.Remove(testAccount);

                            }

                        }
                    }
                }
                else
                {
                    Console.WriteLine("Account does not exist");
                }
            }
            else
            {
                Console.Write("Make another selection");
            }
        }

        public static void EditDeleteAccountsFromFile(List<AccountClasses> AccountList)
        {

            //ListaTransactions(AccountList);

            string dataFilePath = @"C:\MoneyMashine\MoneyMashine.txt";
            AccountClasses account = null; // Declare 'account' before the 'if' statement
            int searchForAccount = 0;// Declare 'searchForAccount' before the 'if' statement

            if (File.Exists(dataFilePath))
            {
                Console.Write("Enter the account number you want to edit or delete from the file or quit: ");
                string readSelection = Console.ReadLine();
                if (readSelection == "q")
                {
                    return;
                }
                else
                {
                    searchForAccount = Convert.ToInt32(readSelection);
                    // int searchForAccount = Convert.ToInt32(Console.ReadLine());
                    account = AccountList.FirstOrDefault(a => a.AccountNumber == searchForAccount);
                    //AccountClasses account = AccountList.FirstOrDefault(a => a.AccountNumber == readSelection); 
                }



                if (account != null)
                {

                    Console.WriteLine($"{account.AccountNumber} : {account.OwnerName} : {account.Amount} : {account.Time} : {account.InOut}");
                    EditorDeleteAccount(AccountList);
                    var lines = new List<string>();
                    using (StreamReader reader = new StreamReader(dataFilePath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.StartsWith(searchForAccount + " :"))
                            {
                                line = $"{account.AccountNumber} : {account.OwnerName} : {account.Amount} : {account.Time} : {account.InOut}";
                            }
                            lines.Add(line);
                        }
                    }



                    File.WriteAllLines(dataFilePath, lines);
                    Console.WriteLine("File saved with edits.");
                }
                else
                {
                    Console.WriteLine("Account not found in the file.");
                }
            }
            else
            {
                Console.WriteLine("File does not exist.");
            }
        }


        public static void SaveAccountsToFile(List<AccountClasses> AccountList)
        {
            Console.WriteLine("Save accounts to file");
            Console.WriteLine("=====================\n");
            string dataFilePath = @"C:\MoneyMashine\MoneyMashine.txt";

            // Create the directory if it doesn't exist
            string directoryPath = Path.GetDirectoryName(dataFilePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using (StreamWriter writer = new StreamWriter(dataFilePath)) //"Writer" writes in the file, not the same as Conslole.Write()

            {
                writer.WriteLine("AccountNumber".PadRight(15) + "OwnerName".PadRight(15) + "Amount".PadRight(15) + "Time".PadRight(15) + "InOut");
                writer.WriteLine("-------------".PadRight(15) + "---------".PadRight(15) + "------".PadRight(15) + "----".PadRight(15) + "-----");

                foreach (AccountClasses testAccount in AccountList)
                {
                    // writer.WriteLine(testAccount.AccountNumber.ToString().PadRight(5) + testAccount.OwnerName.PadRight(5) + testAccount.Amount.ToString().PadRight(5) + DateTime.Now.ToString("MM/dd/yy").PadRight(5) + testAccount.InOut);
                    writer.WriteLine(testAccount.AccountNumber.ToString().PadRight(5) + " : " + testAccount.OwnerName.PadRight(5) + " : " + testAccount.Amount.ToString().PadRight(5) + " : " + DateTime.Now.ToString("MM/dd/yy").PadRight(5) + " : " + testAccount.InOut);

                }
            }

            Console.WriteLine("File saved");
        }

        //public static void ListaTransactions(List<AccountClasses> AccountList) // Lista Transactions 
        //{
        //    Console.WriteLine("AccountNumber".PadRight(15) + "OwnerName".PadRight(15) + "Amount".PadRight(15) + "Time".PadRight(15) + "InOut");
        //    Console.WriteLine("-------------".PadRight(15) + "---------".PadRight(15) + "------".PadRight(15) + "----".PadRight(15) + "-----");
        //    foreach (AccountClasses testAccount in AccountList)
        //    {

        //        Console.Write($"{testAccount.AccountNumber.ToString().PadRight(15)} {testAccount.OwnerName.PadRight(15)} {testAccount.Amount.ToString().PadRight(15)} {DateTime.Now.ToString("MM/dd/yy").PadRight(15)}{testAccount.InOut}\n");
        //    }
        //    Console.WriteLine();
        //}


        public static void LoadTransactionsFromFile(List<AccountClasses> AccountList)
        {
            string dataFilePath = @"C:\MoneyMashine\MoneyMashine.txt";
            if (File.Exists(dataFilePath))
            {
                AccountList.Clear();

                using (StreamReader reader = new StreamReader(dataFilePath))
                {
                    int lineCount = 0;
                    while (!reader.EndOfStream)
                    {
                        lineCount++;
                        string line = reader.ReadLine();
                        Console.WriteLine(line);

                        if (lineCount >= 3) // Skip the first two lines
                        {
                            string[] data = line.Split(':');
                            Console.WriteLine(data);
                            // Check if there are enough elements in the 'data' array


                            if (data.Length >= 0)
                            {

                                foreach (AccountClasses account in AccountList)
                                {
                                    Console.WriteLine($"AccountNumber: {account.AccountNumber}");
                                    Console.WriteLine($"OwnerName: {account.OwnerName}");
                                    Console.WriteLine($"Amount: {account.Amount}");
                                    Console.WriteLine($"Time: {account.Time}");
                                    Console.WriteLine($"InOut: {account.InOut}");
                                    Console.WriteLine();
                                }
                                string accountNumberStr = data[0].Trim();


                                string ownerName = data[1].Trim();
                                string amountStr = data[2].Trim();
                                string timeStr = data[3].Trim();
                                string inOut = data[4].Trim();

                                Console.WriteLine(accountNumberStr);
                                Console.WriteLine(ownerName);
                                Console.WriteLine(amountStr);
                                Console.WriteLine(timeStr);
                                Console.WriteLine(inOut);

                                if (int.TryParse(accountNumberStr, NumberStyles.Any, CultureInfo.InvariantCulture, out int accountNumber) &&
                                    double.TryParse(amountStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double amount) &&
                                    DateTime.TryParse(timeStr, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime time))
                                {
                                    AccountClasses account = new AccountClasses(accountNumber, ownerName, amount, time, inOut);
                                    AccountList.Add(account);
                                    Console.WriteLine("Account added: " + account.ToString());
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid data format in line: " + line);
                            }
                        }

                    }
                }
            }
        }
    }
}


//            using (StreamReader reader = new StreamReader(dataFilePath))
//            {
//                int lineCount = 0;
//                while (!reader.EndOfStream)
//                {
//                    lineCount++;
//                    string line = reader.ReadLine();
//                    if ((lineCount == 0) || (lineCount == 1))
//                    {
//                        Console.WriteLine(line);
//                    }
//                    else
//                    {
//                        Console.WriteLine(line);

//                        string[] data= line.Split(':');

//                        string accountNumberStr = data[0];
//                        string ownerName = data[1];
//                        string amountStr = data[2];
//                        string timeStr = data[3];
//                        string inOut = data[4];

//                        Console.WriteLine(accountNumberStr);
//                        Console.WriteLine(ownerName);
//                        Console.WriteLine(amountStr);
//                        Console.WriteLine(timeStr);
//                        Console.WriteLine(inOut);
//                        Console.ReadLine();

//                        if (int.TryParse(accountNumberStr, out int accountNumber) &&
//                                                double.TryParse(amountStr, out double amount) &&
//                                                DateTime.TryParse(timeStr, out DateTime time))
//                        {
//                            AccountClasses account = new AccountClasses(accountNumber, ownerName, amount, time, inOut);
//                            AccountList.Add(account);
//                            //osäker på om man kan skriva ut så här men vi får se
//                            Console.WriteLine("Account aded: " + account.ToString());
//                            Console.ReadLine();

//                        }
//                    }


//                }
//            }



//        string[] lines = File.ReadAllLines(dataFilePath);
//            File.WriteAllLines(dataFilePath, lines);

//        }
//        else 
//        { 
//            Console.WriteLine("File does not exist."); 
//        }


//    }


//    public static void AddAccountInFile(List<AccountClasses> AccountList)
//    {
//        string tempInOut;

//        Console.Write("Enter Account Number: ");
//        int accountNumber = int.Parse(Console.ReadLine());

//        if (AccountList.Any(account => account.AccountNumber == accountNumber))
//        {
//            Console.WriteLine("An account with the same account number already exists.");
//            return;
//        }

//        Console.Write("Account owner: ");
//        string ownerName = Console.ReadLine();

//        Console.Write("Amount: ");
//        double amount = Convert.ToDouble(Console.ReadLine());

//        if (amount > 0)
//            tempInOut = "in";
//        else
//            tempInOut = "out";

//        DateTime time = DateTime.Now;
//        AccountClasses Account = new AccountClasses(accountNumber, ownerName, amount, time, tempInOut);
//        AccountList.Add(Account);
//        Console.WriteLine("Account added successfully.");
//        }

//    }
//}
//}


