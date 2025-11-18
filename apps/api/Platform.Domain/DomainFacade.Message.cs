using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Platform.Domain;

public sealed partial class DomainFacade
{
    /// <summary>
    /// Creates a new message
    /// </summary>
    public async Task<Message> CreateMessage(Message message)
    {
        return await MessageManager.CreateMessage(message);
    }

    /// <summary>
    /// Creates a user message and generates an AI response using LLM
    /// This is the main method for sending messages in a chat
    /// </summary>
    public async Task<Message> CreateUserMessageAndGetAIResponse(Message userMessage)
    {
        return await MessageManager.CreateUserMessageAndGetAIResponse(userMessage);
    }

    /// <summary>
    /// Gets a message by ID
    /// </summary>
    public async Task<Message?> GetMessageById(Guid id)
    {
        return await MessageManager.GetMessageById(id);
    }

    /// <summary>
    /// Searches for messages with optional filters
    /// </summary>
    public async Task<PaginatedResult<Message>> SearchMessages(Guid? chatId, string? role, string? content, int pageNumber, int pageSize)
    {
        return await MessageManager.SearchMessages(chatId, role, content, pageNumber, pageSize);
    }

    /// <summary>
    /// Deletes a message
    /// </summary>
    public async Task<bool> DeleteMessage(Guid id)
    {
        return await MessageManager.DeleteMessage(id);
    }
}

