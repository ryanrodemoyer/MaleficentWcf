using System;
using System.ServiceModel;
using System.Threading;
using System.Web.Http;
using Maleficent.Messages;
using Maleficent.Shared;

namespace MaleficentWeb.Controllers
{
    public class DefaultController : ApiController
    {
        // GET: api/Default
        public DateTime Get()
        {
            return DefaultBizComponent.GetDateTime();
        }

        [HttpGet]
        [Route("api/standard")]
        public DateTime Standard()
        {
            return DefaultBizComponent.GetDateTimeStandard();
        }
    }

    internal static class DefaultBizComponent
    {
        internal static DateTime GetDateTime()
        {
            using (var factory = new ChannelFactory<IMaleficentService>("MaleficentService"))
            {
                var service = factory.CreateChannel();

                return service.GetSystemDateTime();
            }
        }

        internal static DateTime GetDateTimeStandard()
        {
            Logger.Log(new LogMessage {ThreadId=Thread.CurrentThread.ManagedThreadId, Time=DateTime.Now});

            return DateTime.Now;
        }
    }
}
