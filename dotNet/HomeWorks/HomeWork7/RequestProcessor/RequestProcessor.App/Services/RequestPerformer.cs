using System;
using System.Threading.Tasks;
using RequestProcessor.App.Exceptions;
using RequestProcessor.App.Logging;
using RequestProcessor.App.Models;

namespace RequestProcessor.App.Services
{
    /// <summary>
    /// Request performer.
    /// </summary>
    internal class RequestPerformer : IRequestPerformer
    {
        private IRequestHandler requestHandler;
        private IResponseHandler responseHandler;
        private ILogger logger;
        /// <summary>
        /// Constructor with DI.
        /// </summary>
        /// <param name="requestHandler">Request handler implementation.</param>
        /// <param name="responseHandler">Response handler implementation.</param>
        /// <param name="logger">Logger implementation.</param>
        public RequestPerformer(
            IRequestHandler requestHandler, 
            IResponseHandler responseHandler,
            ILogger logger)
        {
            this.requestHandler = requestHandler;
            this.responseHandler = responseHandler;
            this.logger = logger;

            this.logger.Log("Initialisation RequestPerformer");
        }

        /// <inheritdoc/>
        public async Task<bool> PerformRequestAsync(
            IRequestOptions requestOptions, 
            IResponseOptions responseOptions)
        {
            IResponse response = new Response(false, 504, null);
            logger.Log("Sending request...");
            try
            {
                response = await requestHandler.HandleRequestAsync(requestOptions);
            }
            catch (AggregateException e)
            {
                logger.Log(e, "AggregateException was thrown in RequestPerformer");
                if(e.InnerException is TaskCanceledException)
                {
                    logger.Log(e.InnerException, "TaskCanceledException was thrown in RequestPerformer");
                    return false;
                } else throw new PerformException(e.InnerException.Message + " PerformException was thrown in RequestPerformer", e.InnerException);

            }
            catch (Exception e)
            {
                logger.Log(e, e.StackTrace.ToString() + " some Exception was thrown in RequestPerformer");
                throw new PerformException(e.Message + e.InnerException + " was thrown in RequestPerformer as PerformException", e);
            }
            finally
            {
                await responseHandler.HandleResponseAsync(response, requestOptions, responseOptions);
            }
            logger.Log("Send request and got response");
            return true;
        }
    }
}
