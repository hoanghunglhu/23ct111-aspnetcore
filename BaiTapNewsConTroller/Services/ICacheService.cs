using System;

public interface ICacheService
{
    T GetOrCreate<T>(string key, Func<T> factory, int durationSeconds = 60);

    void Remove(string key);
}