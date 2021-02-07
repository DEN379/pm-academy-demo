using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RequestProcessor.App.Logging;
using RequestProcessor.App.Services;

namespace RequestProcessor.App.Menu
{
    /// <summary>
    /// Main menu.
    /// </summary>
    internal class MainMenu : IMainMenu
    {
        private IRequestPerformer performer;
        private IOptionsSource options;
        private ILogger logger;
        /// <summary>
        /// Constructor with DI.
        /// </summary>
        /// <param name="options">Options source</param>
        /// <param name="performer">Request performer.</param>
        /// <param name="logger">Logger implementation.</param>
        public MainMenu(
            IRequestPerformer performer, 
            IOptionsSource options, 
            ILogger logger)
        {
            this.performer = performer;
            this.options = options;
            this.logger = logger;

            logger.Log("Initialisated MainMenu");
        }

        public async Task<int> StartAsync()
        {
            Console.WriteLine("Request Processor, made by Denys Sakadel");
            try
            {
                Console.WriteLine("Getting options...");
                logger.Log("Getting options...");
                var list = await options.GetOptionsAsync();
                logger.Log("Got options");
                Console.WriteLine("Got options");

                Console.WriteLine("Performing request...");
                logger.Log("Performing request...");
                var tasks = list.Select(opt => performer.PerformRequestAsync(opt.Item1, opt.Item2)).ToArray();

                var isRequestSuccess = Task.WhenAll(tasks);

                if (isRequestSuccess.Result[0])
                {
                    Console.WriteLine("Request was successfully performed");
                    logger.Log("Request was successfully performed");
                    return 0;
                }
                else
                {
                    Console.WriteLine("Request failed");
                    logger.Log("Request failed");
                    return -1;
                }
            } catch (Exception e)
            {
                Console.WriteLine(e + " something went wrong with performing request");
                logger.Log(e, " something went wrong with performing request");
                return -1;
            }
        }
    }
}
