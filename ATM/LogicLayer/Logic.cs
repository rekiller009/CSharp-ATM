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

    }
}