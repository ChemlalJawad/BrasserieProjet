using System;
using System.Collections.Generic;
using System.Text;

namespace Brasserie.Core.Enums
{
    public static class ExceptionMessage
    {
        public const string BREWER_NOT_EXIST = "Brewer does not exist";
        public const string BEER_NOT_EXIST = "Beer does not exist";
        public const string WHOLESALER_NOT_EXIST = "Beer does not exist";
        public const string COMMAND_IS_NULL = "Command can't be null";            
        public const string DUPLICATE_ITEM = "You can't have duplicates items in your Order";
        public const string NEGATIVE_STOCK = "You can't add a negative stock";
        public const string ENOUGH_STOCK = "You don't have enough stocks!";
        public const string NAME_BEER_NOT_EXIST = "Name of beer does not exist";
        public const string PRICE_NULL_OR_NEGATIVE = "Price of beer is null or negative";
        public const string BEER_NOT_SELL = "Beer is not sell";
        public const string ALREADY_SELL = "Wholesaler already sell this beer";

    }
}
