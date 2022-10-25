namespace BudgetService;

public class AccountingService
{
    private readonly IBudgetRepo _budgetRepo;
    private List<Budget> _budgets;

    public AccountingService(IBudgetRepo budgetRepo)
    {
        _budgetRepo = budgetRepo;
    }

    public decimal Query(DateTime start, DateTime end)
    {
        if (DateService.IsValidInput(start, end))
        {
            return 0;
        }

        _budgets = GetAllBudgets();
        var startBudget = GetSingleBudget(start);
        var endBudget = GetSingleBudget(end);

        if (DateService.IsSameYearMonth(start, end))
        {
            return DateService.GetDays(start, end) *
                   AmountService.CalculateAmountPerDay(start, startBudget!.Amount);
        }

        var amountOfStart = AmountService.GetAmountOfStartMonth(start, startBudget);

        var amountOfEnd = AmountService.GetAmountOfEndMonth(end, endBudget);

        var totalMiddleAmount = AmountService.GetAmountOfMiddleMonth(start, end, _budgets);

        return amountOfStart + totalMiddleAmount + amountOfEnd;
    }


    public Budget? GetSingleBudget(DateTime dateTime)
    {
        var budget = _budgets.FirstOrDefault(x => x.YearMonth == dateTime.ToString("yyyyMM"));

        if (budget == null)
        {
            return new Budget();
        }

        return budget;
    }

    private List<Budget> GetAllBudgets()
    {
        return _budgetRepo.GetAll();
    }
}