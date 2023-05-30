namespace TheBillController.Api;

public static class ApiEndpoints
{
    private const string ApiBase = "api"; 

    public static class ExpenseType
    {
        private const string Base = $"{ApiBase}/expensetype";

        public const string Create = Base;
        public const string Get = $"{Base}/{{idOrName}}";
        public const string GetAll = Base;
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
    }

    public static class Expense
    {
        private const string Base = $"{ApiBase}/expense";

        public const string Create = Base;
        public const string Get = $"{Base}/{{id:guid}}";
        public const string GetMore = Base;
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
    }
}
