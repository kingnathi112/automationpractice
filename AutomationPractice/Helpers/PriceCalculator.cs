namespace AutomationPractice.Helpers;

public class PriceCalculator
{
    public static double CalculateTotal(double unitPrice, long quantity)
    {
        return unitPrice * quantity;
    }
}