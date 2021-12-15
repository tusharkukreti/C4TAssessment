using System.Collections.Generic;
using System.Threading.Tasks;

namespace C4TAssessment.BusinessAbstraction
{
    /// <summary>
    /// Responsible for performing cloud operations
    /// </summary>
    public interface IAzureService
    {
        /// <summary>
        /// This method pushes a list of messages to the Azure Service Bus Queue
        /// </summary>
        /// <param name="messages">List of string messages</param>
        /// <returns></returns>
        Task PushMessageToServiceBusQueue(List<string> messages);
    }
}
