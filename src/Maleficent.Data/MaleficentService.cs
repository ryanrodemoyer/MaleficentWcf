using System;
using System.Threading;
using Maleficent.Messages;
using Maleficent.Shared;

namespace Maleficent.Data
{
    public class MaleficentService : IMaleficentService
    {
        public DateTime GetSystemDateTime()
        {
            Logger.Log(new LogMessage {Time = DateTime.Now, ThreadId = Thread.CurrentThread.ManagedThreadId});

            return MaleficentServiceManager.GetSystemDateTime();
        }
    }

    internal static class MaleficentServiceManager
    {
        internal static DateTime GetSystemDateTime()
        {
            return DateTime.Now;
        }
    }
}
