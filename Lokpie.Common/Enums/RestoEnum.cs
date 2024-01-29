using System;
using System.Collections.Generic;
using System.Text;

namespace Lokpie.Common.Enums
{
    public enum RestoEnum
    {
        Restaurant,
        Cafe
    }

    public enum TableStatus
    {
        Occupied,
        Reserved,
        Available
    }

    public enum ReservationType
    {
        Static,
        Dynamic
    }

    public enum ItemStatus
    {
        None,
        NewMenu,
        SpecialMenu,
        Limited,
        Hot,
        SpecialOffer
    }

    public enum ItemType
    {
        Food,
        Beverage
    }

    public enum ItemCategory
    {
        Others,
        Nasi,
        Mie,
        Kwetiau,
        Bihun,
        Gurami,
        Nila,
        Bebek,
        Ayam,
        Ingkung,
        SapiKambing,
        Cumi,
        Udang,
        Sambal,
        Sayur,
        Shabu,
        Camilan,
        Minuman,
        Jus
    }

    public enum OrderStatus
    {
        Sent,
        OnProcess,
        Done,
        Finish,
        Reject
    }
    
    public enum ReservationStatus
    {
        New,
        Confirmed,
        Reject
    }

    public enum PaymentMethod
    {
        Cash,
        Qris
    }
}
