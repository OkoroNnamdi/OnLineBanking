using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OnLineBanking.Core.Enum
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public  enum Role
    {
        Admin=0,
        Manager=1,
        Customer=2,
        CustomerServices =3,
        Cashier =4,
    }
}
