using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdmundsApi.Requests
{
    public enum State 
    {
        New,
        Used,
        Future,
    }

    public enum View
    {
        Basic,
        Full,
    }

    public enum Category
    {
        Hatchback4Dr,
        Coupe,
        Convertible,
        Sedan,
        Hatchback2Dr,
        Wagon,
        RegularCabPickup,
        ExtendedCabPickup,
        CrewCabPickup,
        Suv2Dr,
        Suv4Dr,
        SuvConvertible,
        CargoVan,
        PassengerVan,
        CargoMinivan,
        PassengerMinivan
    }
}
