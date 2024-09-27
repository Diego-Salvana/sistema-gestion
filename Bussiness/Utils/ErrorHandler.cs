namespace Bussiness.Utils;
public class ErrorHandler
{
    public static Exception Error (Exception ex, string message)
    {
        if (ex is ArgumentException)
        {
            return new Exception(ex.Message);
        }

        Console.WriteLine(ex);
        return new Exception(message);
    }
}
