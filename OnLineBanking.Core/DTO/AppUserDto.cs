﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.DTO
{
public  class AppUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string  Gender { get; set; }
        public bool IsActive { get; set; }
        public string Avatar { get; set; }
    }
}
