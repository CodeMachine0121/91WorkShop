namespace BudgetService;

public class DateService
{
    public static int GetDaysInFirstMonth(DateTime start)
    {
        return DateTime.DaysInMonth(start.Year, start.Month) - start.Day + 1;
    }

    public static int GetDays(DateTime start, DateTime end)
    {
        return ((end - start).Days + 1);
    }

    public static bool IsValidInput(DateTime start, DateTime end)
    {
        return start > end;
    }

    public static bool IsSameYearMonth(DateTime start, DateTime end)
    {
        return start.ToString("yyyyMM") == end.ToString("yyyyMM");
    }
}