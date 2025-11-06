namespace Shop.BLL.Infrastructure;

public static class CoreValidator
{
    public static bool CheckIfStringIsNullOrEmpty(string input)
    {
        return string.IsNullOrEmpty(input);
    }
}