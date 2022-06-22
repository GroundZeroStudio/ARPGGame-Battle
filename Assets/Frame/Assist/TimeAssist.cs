using System;

namespace Knight.Core
{
    public static class TimeAssist
    {
        private static readonly long mEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
        private static int mTimeZoneSecondsOffset;
        private static long mClientServerOffset = 0;
        public static long TimeZoneEpoch { get; private set; } = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
        public static long SecondsPerDay = 86400;
        public static int TimeZoneSecondsOffset
        {
            get
            {
                return mTimeZoneSecondsOffset;
            }
            set
            {
                mTimeZoneSecondsOffset = value;
                TimeZoneEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(mTimeZoneSecondsOffset).Ticks;
            }
        }
        /// <summary>
        /// 服务器时间(一百纳秒)
        /// </summary>
        /// <returns></returns>
        public static long ServerNowTicks()
        {
            return DateTime.UtcNow.Ticks - mEpoch + mClientServerOffset;
        }
        /// <summary>
        /// 服务器时间(毫秒)
        /// </summary>
        /// <returns></returns>
        public static long ServerNow()
        {
            return (DateTime.UtcNow.Ticks - mEpoch + mClientServerOffset) / 10000;
        }
        /// <summary>
        /// 服务器时间(秒)
        /// </summary>
        /// <returns></returns>
        public static long ServerNowSeconds()
        {
            return (DateTime.UtcNow.Ticks - mEpoch + mClientServerOffset) / 10000000;
        }
        /// <summary>
        /// 计算客户端与服务器时间差值
        /// </summary>
        /// <param name="nServerTimeSeconds"></param>
        public static void CalcClientServerOffset(long nServerTimeSeconds)
        {
            var nClientTicks = DateTime.UtcNow.Ticks - mEpoch;
            var nServerTicks = nServerTimeSeconds * 10000000;
            mClientServerOffset = nServerTicks - nClientTicks;
        }
        /// <summary>
        /// 登陆前是客户端时间,登陆后是同步过的服务器时间
        /// </summary>
        /// <returns></returns>
        public static long Now()
        {
            return ServerNow();
        }
        /// <summary>
        /// 客户端时间戳(一百纳秒)
        /// </summary>
        /// <returns></returns>
        public static long ClientNowTicks()
        {
            return DateTime.UtcNow.Ticks - mEpoch;
        }
        /// <summary>
        /// 客户端时间戳(毫秒)
        /// </summary>
        /// <returns></returns>
        public static long ClientNow()
        {
            return (DateTime.UtcNow.Ticks - mEpoch) / 10000;
        }
        /// <summary>
        /// 客户端时间戳(秒)
        /// </summary>
        /// <returns></returns>
        public static long ClientNowSeconds()
        {
            return (DateTime.UtcNow.Ticks - mEpoch) / 10000000;
        }
    }
}