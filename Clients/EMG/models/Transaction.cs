using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationPlatformFunctions.Clients.EMG.models
{
    class Transaction
    {
        public string Title { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
