namespace CleanArchitecture.Application.Common.Extensions;
public static class DateTimeExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static DateTime UtcToLocal(this DateTime source)
    {
        return TimeZoneInfo.ConvertTimeFromUtc(source, TimeZoneInfo.Local);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static DateTime LocalToUtc(this DateTime source)
    {
        source = DateTime.SpecifyKind(source, DateTimeKind.Unspecified);
        return TimeZoneInfo.ConvertTimeToUtc(source, TimeZoneInfo.Local);
    }
}
