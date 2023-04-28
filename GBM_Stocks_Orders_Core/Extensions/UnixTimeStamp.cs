namespace GBM_Stocks_Orders_Core.Extensions
{
    public static class UnixTimeStamp
    {
        public static DateTime UnixTimeStampToDateTime(this int unixTimeStamp) 
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }
    }
}
