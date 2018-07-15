using System.Web.Http.ExceptionHandling;
using CustomLog4netLibrary;
using System.Linq;
namespace API.TraceLogger
{
    public class CustomExceptionLogger : ExceptionLogger
    {
        private readonly ILogger _logger;
        public CustomExceptionLogger(ILogger logger)
        {
            this._logger = logger;
        }
        public override void Log(ExceptionLoggerContext context)
        {
            var Guid = context.Request.Headers.GetValues("TrackId").FirstOrDefault();
            _logger.Log("Track Id : " + Guid + " - ERROR : {" + context.Exception.Message + "}", LogType.Error);
            base.Log(context);
        }
    }
}