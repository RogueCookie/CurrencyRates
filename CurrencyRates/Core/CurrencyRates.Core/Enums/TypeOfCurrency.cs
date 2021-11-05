using System.ComponentModel;
using CurrencyRates.Core.Attribute;

namespace CurrencyRates.Core.Enums
{
    /// <summary>
    /// Kinds of available currency shortcut. Can be extended at any time
    /// </summary>
    public enum TypeOfCurrency
    {
        /// <summary>
        /// Australia dollar
        /// </summary>
        [Description("Australia")]
        [FullNameCurrency("Australia dollar")]
        AUD = 1,

        /// <summary>
        /// Brazil real
        /// </summary>
        [Description("Brazil")]
        [FullNameCurrency("Brazil real")]
        BRL = 2,

        /// <summary>
        /// Bulgaria lev
        /// </summary>
        [Description("Bulgaria")]
        [FullNameCurrency("Bulgaria lev")]
        BGN = 3,

        /// <summary>
        /// Canada dollar
        /// </summary>
        [Description("Canada")]
        [FullNameCurrency("Canada dollar")]
        CAD = 4,

        /// <summary>
        /// China renminbi
        /// </summary>
        [Description("China")]
        [FullNameCurrency("China renminbi")]
        CNY = 5,

        /// <summary>
        /// Croatia kuna
        /// </summary>
        [Description("Croatia")]
        [FullNameCurrency("Croatia kuna")]
        HRK = 6,

        /// <summary>
        /// Denmark krone
        /// </summary>
        [Description("Denmark")]
        [FullNameCurrency("Denmark krone")]
        DKK = 7,

        /// <summary>
        /// EMU euro
        /// </summary>
        [Description("EMU")]
        [FullNameCurrency("EMU euro")]
        EUR = 8,

        /// <summary>
        /// Hongkong dollar
        /// </summary>
        [Description("Hongkong")]
        [FullNameCurrency("Hongkong dollar")]
        HKD = 9,

        /// <summary>
        /// Hungary forint
        /// </summary>
        [Description("Hungary")]
        [FullNameCurrency("Hungary forint")]
        HUF = 10,

        /// <summary>
        /// Iceland krona
        /// </summary>
        [Description("Iceland")]
        [FullNameCurrency("Iceland krona")]
        ISK = 11,

        /// <summary>
        /// IMF SDR
        /// </summary>
        [Description("IMF")]
        [FullNameCurrency("IMF SDR")]
        XDR = 12,

        /// <summary>
        /// India rupee
        /// </summary>
        [Description("India")]
        [FullNameCurrency("India rupee")]
        INR = 13,

        /// <summary>
        /// Indonesia rupiah
        /// </summary>
        [Description("Indonesia")]
        [FullNameCurrency("Indonesia rupiah")]
        IDR = 14,

        /// <summary>
        /// Israel new shekel
        /// </summary>
        [Description("Israel")]
        [FullNameCurrency("Israel new shekel")]
        ILS = 15,

        /// <summary>
        /// Japan yen
        /// </summary>
        [Description("Japan")]
        [FullNameCurrency("Japan yen")]
        JPY = 16,

        /// <summary>
        /// Malaysia ringgit
        /// </summary>
        [Description("Malaysia")]
        [FullNameCurrency("Malaysia ringgit")]
        MYR = 17,

        /// <summary>
        /// Mexico peso
        /// </summary>
        [Description("Mexico")]
        [FullNameCurrency("Mexico peso")]
        MXN = 18,

        /// <summary>
        /// New Zealand dollar
        /// </summary>
        [Description("New Zealand")]
        [FullNameCurrency("New Zealand dollar")]
        NZD = 19,

        /// <summary>
        /// Norway krone
        /// </summary>
        [Description("Norway")]
        [FullNameCurrency("Norway krone")]
        NOK = 20,

        /// <summary>
        /// Philippines peso
        /// </summary>
        [Description("Philippines")]
        [FullNameCurrency("Philippines peso")]
        PHP = 21,

        /// <summary>
        /// Poland zloty
        /// </summary>
        [Description("Poland")]
        [FullNameCurrency("Poland zloty")]
        PLN = 22,

        /// <summary>
        /// Romania leu
        /// </summary>
        [Description("Romania")]
        [FullNameCurrency("Romania leu")]
        RON = 23,

        /// <summary>
        /// Russia rouble
        /// </summary>
        [Description("Russia")]
        [FullNameCurrency("Russia rouble")]
        RUB = 24,

        /// <summary>
        /// Singapore dollar
        /// </summary>
        [Description("Singapore")]
        [FullNameCurrency("Singapore dollar")]
        SGD = 25,

        /// <summary>
        /// South Africa rand
        /// </summary>
        [Description("South Africa")]
        [FullNameCurrency("South Africa rand")]
        ZAR = 26,

        /// <summary>
        /// South Korea won
        /// </summary>
        [Description("South Korea")]
        [FullNameCurrency("South Korea won")]
        KRW = 27,

        /// <summary>
        /// Sweden krona
        /// </summary>
        [Description("Sweden")]
        [FullNameCurrency("Sweden krona")]
        SEK = 28,

        /// <summary>
        /// Switzerland franc
        /// </summary>
        [Description("Switzerland")]
        [FullNameCurrency("Switzerland franc")]
        CHF = 29,

        /// <summary>
        /// Thailand baht
        /// </summary>
        [Description("Thailand")]
        [FullNameCurrency("Thailand baht")]
        THB = 30,

        /// <summary>
        /// Turkey lira
        /// </summary>
        [Description("Turkey")]
        [FullNameCurrency("Turkey lira")]
        TRY = 31,

        /// <summary>
        /// United Kingdom pound
        /// </summary>
        [Description("United Kingdom")]
        [FullNameCurrency("United Kingdom pound")]
        GBP = 32,

        /// <summary>
        /// USA dollar
        /// </summary>
        [Description("USA")]
        [FullNameCurrency("USA dollar")]
        USD = 33,

        /// <summary>
        /// Czech crown 
        /// </summary>
        [Description("Czech Republic")]
        [FullNameCurrency("Czech crown")]
        CZH = 35
    }
}