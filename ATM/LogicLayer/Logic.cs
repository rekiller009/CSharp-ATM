using BOLayer;
using DataLayer;
using System;
using System.Collections.Generic;

namespace LogicLayer
{
    public class Logic
    {
        // Method to verify login of Admin
        public bool VerifyLogin(Admin admin)
        {
            Data adminData = new Data();
            return adminData.isInFile(admin);
        }

        // Method to verify, is username in the file
        public int isUserActive(string user)
        {
            Data data = new Data();
            return data.isUserActive(user);
        }

        // Method to verify login of customer
        public bool VerifyLogin(Customer customer)
        {
            Data customerData = new Data();
            return customerData.canLogin(customer);
        }

        // Method to check if Username is valid or not (Username can only contain A-Z, a-z, & 0-9)
        public bool isValidUsername(string s)
        {
            foreach(char c in s)
            {
                if((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9'))
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        // Method to check if Pin is valid or not (Pin is 5-digit & can only contain 0-9)
        public bool isValidPin(string s)
        {
            if(s.Length != 5)
            {
                return false;
            }
            foreach(char c in s)
            {
                if(c >= '0' && c <= '9')
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        // Encryption Method
        // For alphabets we swap A with Z, B with Y and so on.
        // A B C D E F G H I J K L M N O P Q R S T U V W X Y Z
        // Z Y X W V U T S R Q P O N M L K J I H G F E D C B A
        // For Number we have
        // 0123456789
        // 9876543210
        public string EncryptionDecrytion(string username)
        {
            string output = "";
            foreach(char c in username)
            {
                if(c >= 'A' && c <= 'Z')
                {
                    output += Convert.ToChar('Z' - (c - 'A'));
                }
                else if(c >= 'a' && c <= 'z')
                {
                    output += Convert.ToChar('z' - (c - 'a'));
                }
                else if(c >= '0' && c <= '9')
                {
                    output += (9 - char.GetNumericValue(c));
                }
            }
            return output;
        }

        // Disables an account
        public void DisableAccount(string username)
        {
            Data data = new Data();
            Customer customer = data.GetCustomer(username);
            // updating the status
            customer.Status = "Disabled";
            // saving back to file
            data.UpdateInFile(customer);
        }
        
        // Method to Create Account of a Customer
        public void CreateAccount()
        {
            Data data = new Data();
            Customer customer = new Customer();
            Console.WriteLine("---Creating New Account----\n" +
                "Enter User Details");
        getUsername:
            {
                Console.WriteLine("Username: ");
                string un = Console.ReadLine();

                // Checks if Username is valid or not (Username can only contain A-Z, a-z & 0-9)
                if (un == "" || !isValidUsername(un))
                {
                    Console.WriteLine("Enter valid Username (Username can only contain A-Z, a-z & 0-9)");
                    goto getUsername;
                }

                // Doing encrytion
                customer.Username = EncryptionDecrytion(un);
                // If username is already assigned to someone
                if (data.isInFile(customer.Username))
                {
                    Console.WriteLine("Username already exists!! Enter again");
                    goto getUsername;
                }
            }

        getPin:
            {
                Console.Write("5-digit Pin: ");
                customer.Pin = Console.ReadLine();

                // Checks if Pin is valid or not (Pin is 5-digit & can only contain 0-9)
                if (!isValidPin(customer.Pin))
                {
                    Console.WriteLine("Enter valid Pin (Pin is 5-digit & can only contain 0-9)");
                    goto getPin;
                }

                // Doing Encrytion
                customer.Pin = EncryptionDecrytion(customer.Pin);
            }
            // Gets Holders Name
            Console.Write("Holder's Name: ");
            string name = Console.ReadLine();
            if(!String.IsNullOrEmpty(name) || !String.IsNullOrWhiteSpace(name))
            {
                customer.Name = name;
            }

        getAccountType:
            {
                Console.Write("Account Type (Savings/Current): ");
                customer.AccountType = Console.ReadLine();

                // Checks if Account type is valid
                if(!(customer.AccountType == "Savings" || customer.AccountType == "Current"))
                {
                    Console.WriteLine("Wrong Input. Enter \"Savings\" & \"Current\"");
                    goto getAccountType;
                }
            }

        // Gets Staring Balance
        getBalance:
            {
                try
                {
                    Console.Write("Starting Balance: ");
                    customer.Balance = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Wrong Input. Enter numbers only.");
                    goto getBalance;
                }
            }

        // Gets Status of Account (Active/Disabled)
        getStatus:
            {
                Console.Write("Status (Active/Dsiabled): ");
                customer.Status = Console.ReadLine();

                // Checks if Status is valid
                if(!(customer.Status == "Active" || customer.Status == "Disabled"))
                {
                    Console.WriteLine("Wrong Input. Enter \"Active\" & \"Disabled\"");
                    goto getStatus;
                }
            }
            
            // Assiging Account Number
            customer.AccountNo = data.getLastAccountNumber() + 1;

            // Appending Customer to file
            data.AddToFile(customer);

            Console.WriteLine($"Account Successfully Created - the account number assigned is: {customer.AccountNo}");
        }

        // Deletes an account from file
        public void DeleteAccount()
        {
            // Get the account number from user throguh console
            int accNo = getAccNum();

            Data data = new Data();
            Customer customer = new Customer();

            if(data.isInFile(accNo,out customer)) // Checks if the account number is in file
            {
                Console.Write($"You wish to delete the account held by Mr {customer.Name}.\n" +
                    "If this information is correct please re-enter the account number: ");

                try
                {
                    int tempAccNo = Convert.ToInt32(Console.ReadLine());
                    // if user enters the same account number
                    if(tempAccNo == accNo)
                    {
                        data.DeleteFromFile(customer);
                        Console.WriteLine("Account Deleted Successfully");
                        return;
                    }
                    // if user enters different account number
                    else
                    {
                        Console.WriteLine("No Account was deleted!");
                        return;
                    }
                }
                // if user does not enter a number
                catch (Exception)
                {
                    Console.WriteLine("No Account was deleted!");
                }
            }
            else
            {
                Console.WriteLine($"Account number {accNo} does not exist!");
            }
        }

        // Update any account details
        public void UpdateAccount()
        {
            // Gets the account number from user through console
            int accNo = getAccNum();

            Data data = new Data();
            Customer account = new Customer();
            if(data.isInFile(accNo, out account))
            {
                // printing account details
                PrintAccountDetails(account);
                Console.WriteLine("\n Please enter in the fields you wish to update (leave blank otherwise): \n");

            getUsername:
                {
                    // Updating Username
                    string un = getUsername();
                    if (!string.IsNullOrEmpty(un))
                    {
                        account.Username = EncryptionDecrytion(un);
                        if (data.isInFile(account.Username))
                        {
                            Console.WriteLine("Username already exists!! Enter again");
                            goto getUsername;
                        }
                    }
                }

                // Update Pin
            }
        }

        // Method to get account number from user through console
        // Used in DeleteAccount() & UpdateAccount()
        public int getAccNum()
        {
            int accNo = 0;
        getAccNo:
            {
                Console.Write("Account number: ");
                try
                {
                    accNo = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid Input! Please try again.");
                    goto getAccNo;
                }
            }
            return accNo;
        }
    }
}