namespace BudgetService;

public class AmountService
{
    public static int GetAmountOfMiddleMonth(DateTime start, DateTime end, List<Budget> budgets)
    {
        var current = start.AddMonths(1);
        var totalMiddleAmount = 0;
        while (current < new DateTime(end.Year, end.Month, 1))
        {
            var currentBudget = budgets.FirstOrDefault(x => x.YearMonth == current.ToString(
                "yyyyMM"));
            if (currentBudget != null)
            {
                totalMiddleAmount += currentBudget.Amount;
            }

            current = current.AddMonths(1);
        }
        return totalMiddleAmount;
    }

    public static int GetAmountOfEndMonth(DateTime end, Budget? endDateBudget)
    {
        return end.Day*(CalculateAmountPerDay(end, endDateBudget!.Amount));
    }

    public static int GetAmountOfStartMonth(DateTime start, Budget? startBudget)
    {
        return DateService.GetDaysInFirstMonth(start)*
               CalculateAmountPerDay(start, startBudget!.Amount);
    }

    public static int CalculateAmountPerDay(DateTime start, int amount)
    {
        return amount / DateTime.DaysInMonth(start.Year, start.Month);
    }
}