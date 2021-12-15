using C4TAssessment.BusinessAbstraction;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C4TAssessment.BusinessImplementation
{
    ///<inheritdoc/>
    public class AzureService : IAzureService
    {
        private readonly IQueueClient _queueClient;

        /// <summary>
        /// Initialization
        /// </summary>
        public AzureService(IQueueClient queueClient)
        {
            _queueClient = queueClient;
        }

        ///<inheritdoc/>
        public async Task PushMessageToServiceBusQueue(List<string> messages)
        {
            foreach (string message in messages)
            {
                var queueMessage = new Message(Encoding.UTF8.GetBytes(message))
                {
                    MessageId = Guid.NewGuid().ToString(),
                    ContentType = "application/json"
                };
                await _queueClient.SendAsync(queueMessage).ConfigureAwait(false);
            }
        }
    }
}
