using System;
using System.Linq.Expressions;

namespace ProductManager.Tests.Providers;

public class MockAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
{
    public MockAsyncEnumerable(IEnumerable<T> enumerable)
        : base(enumerable)
    {
    }

    public MockAsyncEnumerable(Expression expression)
        : base(expression)
    {
    }

    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        return new MockAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
    }

    IQueryProvider IQueryable.Provider => new MockAsyncQueryProvider<T>(this);
}