﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.Utility
{
    public  class UserModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; } = string.Empty;
    }
}