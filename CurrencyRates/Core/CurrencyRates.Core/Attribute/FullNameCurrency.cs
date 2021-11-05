namespace CurrencyRates.Core.Attribute
{
    public class FullNameCurrency : System.Attribute
    {
        /// <summary>
        /// Полное наименование для перечисления валюты
        /// </summary>
        public string FullName { get; set; }

        public FullNameCurrency(string value)
        {
            FullName = value;
        }
    }
}