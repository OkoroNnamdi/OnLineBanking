﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.Domain
{
    public  class Account:BaseEntity 
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public bool IsActive { get; set; } = true;
        public string Account_Number { get; set; }= string.Empty;
        public string PublicId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
        public ICollection <Transaction > Transactions { get; set; }
        public ICollection <Customer> Customers { get; set; }
        public ICollection <Address >Addresses { get; set; }
    }
}
