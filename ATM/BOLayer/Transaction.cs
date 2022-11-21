using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOLayer
{
    public class Transaction
    {
        // Data members for Transaction class
        public int AccountNo { get; set; }
        public string Username { get; set; }
        public string HoldersName { get; set; }
        public string TransactionType { get; set; }
        public int TransactionAmount { get; set; }
        public string Data { get; set; }
        public int Balance { get; set; }

    }
}
