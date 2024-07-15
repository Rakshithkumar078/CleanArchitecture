﻿using ProtEquity.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ProtEquity.Application.UnitTests;
public class ConnectionFactory : IDisposable
{
    #region IDisposable Support
    private bool disposedValue = false; // To detect redundant calls

    public ApplicationDbContext CreateContextForInMemory()
    {
        var option = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "Test_Database").Options;

        ApplicationDbContext context = new ApplicationDbContext(option);
        if (context != null)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            return context;
        }
        return context;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
    }
    #endregion
}
