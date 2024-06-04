namespace Warehouse.Infrastructure.DAL.Exceptions;

internal class DbOperationException : Exception
{
    public DbOperationException(string message, Exception inner) : base(message, inner)
    {

    }
}
