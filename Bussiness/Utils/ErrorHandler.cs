namespace Bussiness.Utils;
public static class ErrorHandler
{
    public static Exception Error (Exception ex, string message)
    {
        Console.WriteLine(ex);

        if (ex is ArgumentException)
        {
            return new ArgumentException(ex.Message);
        } 

        return new Exception(message);
    }
}
