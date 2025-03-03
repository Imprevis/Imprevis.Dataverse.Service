namespace Imprevis.Dataverse.Resolvers.Http.UnitTests.Mocks;

using Microsoft.AspNetCore.Http;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

internal class MockCookieCollection : IRequestCookieCollection
{
    private readonly Dictionary<string, string> cookies = [];

    public void Add(string key, string value)
    {
        cookies.Add(key, value);
    }

    public string? this[string key] => cookies[key];

    public int Count => cookies.Count;

    public ICollection<string> Keys => cookies.Keys;

    public bool ContainsKey(string key)
    {
        return cookies.ContainsKey(key);
    }

    public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
    {
        return cookies.GetEnumerator();
    }

    public bool TryGetValue(string key, [NotNullWhen(true)] out string? value)
    {
        return cookies.TryGetValue(key, out value);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
