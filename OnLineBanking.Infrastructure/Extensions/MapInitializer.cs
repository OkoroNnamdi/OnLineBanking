using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Infrastructure.Extensions.Automapper
{
    public  class MapInitializer:Profile
    {
        public Mapper regMapper { get; set; }
        public MapInitializer()
        {
            
        }
    }
}
