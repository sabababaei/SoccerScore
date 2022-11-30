using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Soccer.Test.Soccer.Application.Test.Common;

public class DataBaseContextFactory
{
    public static DataBaseContext Create()
    {
        var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase("SoccerScore")
            .Options;

        var context = new DataBaseContext(options);
        context.Database.EnsureCreated();
        return context;
    }

    public static void Destroy(DataBaseContext context)
    {
        context.Dispose();
    }
}