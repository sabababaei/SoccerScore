using Persistence;

namespace Soccer.Test.Soccer.Application.Test.Common;

public class CommandTestBase:IDisposable
{
    protected readonly DataBaseContext _context;
    public CommandTestBase()
    {
        _context = DataBaseContextFactory.Create();
    }

    public void Dispose()
    {
        DataBaseContextFactory.Destroy(_context);
    }
}