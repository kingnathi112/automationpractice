namespace AutomationPractice.Helpers;

public static class PriceHelper
{
    public static double ToPriceDouble(this string amount)
    { 
        return Convert.ToDouble(Double.Parse(amount, System.Globalization.NumberStyles.Currency));
    }
}