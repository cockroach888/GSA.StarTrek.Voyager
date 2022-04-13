namespace MQTT2MVC4WebApp1.Common;

/// <summary>
/// 日期时间辅助类
/// </summary>
public static class DateTimeHelper
{
    private static readonly DateTime _utc1970 = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    private static readonly Dictionary<TimestampType, Func<DateTime, Task<long>>> _dictToTimestamp = new(2)
    {
        [TimestampType.TotalSeconds] = time => Task.Run(() => (long)time.Subtract(_utc1970).TotalSeconds),
        [TimestampType.TotalMilliseconds] = time => Task.Run(() => (long)time.Subtract(_utc1970).TotalMilliseconds)
    };

    private static readonly Dictionary<TimestampType, Func<long, Task<DateTime>>> _dictToDateTime = new(2)
    {
        [TimestampType.TotalSeconds] = value => Task.Run(() => DateTimeOffset.FromUnixTimeSeconds(value).DateTime),
        [TimestampType.TotalMilliseconds] = value => Task.Run(() => DateTimeOffset.FromUnixTimeMilliseconds(value).DateTime)
    };


    /// <summary>
    /// 获取当前系统时间的时间戳
    /// </summary>
    /// <param name="type">时间戳类型</param>
    /// <returns>时间戳</returns>
    public static long GetTimestamp(TimestampType type = TimestampType.TotalMilliseconds)
    {
        long result = -1;

        switch (type)
        {
            case TimestampType.TotalSeconds:
                result = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                break;
            case TimestampType.TotalMilliseconds:
                result = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                break;
        }

        return result;
    }

    /// <summary>
    /// 计算指定时间的时间戳
    /// </summary>
    /// <param name="value">需要计算的时间</param>
    /// <param name="type">时间戳类型</param>
    /// <returns>时间戳</returns>
    public static async Task<long> GetTimestampAsync(DateTime value, TimestampType type = TimestampType.TotalMilliseconds)
        => await _dictToTimestamp[type](value.ToUniversalTime()).ConfigureAwait(false);

    /// <summary>
    /// 计算指定时间戳所表示的时间
    /// </summary>
    /// <param name="value">需要计算的时间戳</param>
    /// <param name="type">时间戳类型</param>
    /// <returns>计算结果</returns>
    public static async Task<DateTime> GetDateTimeAsync(long value, TimestampType type = TimestampType.TotalMilliseconds)
        => await _dictToDateTime[type](value).ConfigureAwait(false);

    /// <summary>
    /// 计算指定时间戳所表示的时间，并转换为当前所在的本地时间。
    /// </summary>
    /// <param name="value">需要计算的时间戳</param>
    /// <param name="type">时间戳类型</param>
    /// <returns>计算结果</returns>
    public static async Task<DateTime> GetDateTimeOfLocalTimeAsync(long value, TimestampType type = TimestampType.TotalMilliseconds)
    {
        DateTime result = await GetDateTimeAsync(value, type).ConfigureAwait(false);
        return result.ToLocalTime();
    }
}


/// <summary>
/// 时间戳类型
/// </summary>
public enum TimestampType
{
    /// <summary>
    /// 总毫秒数
    /// </summary>
    TotalMilliseconds,

    /// <summary>
    /// 总秒数
    /// </summary>
    TotalSeconds
}
