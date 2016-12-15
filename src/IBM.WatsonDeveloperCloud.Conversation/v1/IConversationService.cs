using IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace IBM.WatsonDeveloperCloud.Conversation.v1
{
    public interface IConversationService
    {
        /// <summary>
        /// Get a response to a user's input.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace</param>
        /// <param name="request">A MessageRequest object that provides the input text and optional context</param>
        /// <returns></returns>
        MessageResponse Message(string workspaceId, MessageRequest request);
    }
}