using System.ComponentModel;

namespace RealState.Domain.Enums;

public enum TransactionTypes
{
    [Description("CREATION")]
    Creation = 1,

    [Description("SALE")]
    Sale = 2,

    [Description("PRICE CHANGE")]
    PriceChange = 3,

    [Description("UPDATE")]
    Update = 4
}