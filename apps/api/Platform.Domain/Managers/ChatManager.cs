using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Platform.Domain;

/// <summary>
/// Manages business operations for Chat entities
/// </summary>
internal sealed class ChatManager : IDisposable
{
    private readonly ServiceLocatorBase _serviceLocator;
    private DataFacade? _dataFacade;
    private DataFacade DataFacade => _dataFacade ??= new DataFacade(_serviceLocator.CreateConfigurationProvider().GetDbConnectionString());
    private PersonaManager? _personaManager;
    private PersonaManager PersonaManager => _personaManager ??= new PersonaManager(_serviceLocator);
    private TrainingStorageManager? _storageManager;
    private TrainingStorageManager StorageManager => _storageManager ??= new TrainingStorageManager();

    public ChatManager(ServiceLocatorBase serviceLocator)
    {
        _serviceLocator = serviceLocator;
    }

    /// <summary>
    /// Creates a new Chat
    /// </summary>
    /// <param name="chat">The Chat entity to create</param>
    /// <returns>The created Chat</returns>
    public async Task<Chat> CreateChat(Chat chat)
    {
        ChatValidator.Validate(chat);
        
        return await DataFacade.AddChat(chat).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets a Chat by ID
    /// </summary>
    /// <param name="id">The ID of the Chat to get</param>
    /// <returns>The Chat if found, null otherwise</returns>
    public async Task<Chat?> GetChatById(Guid id)
    {
        return await DataFacade.GetChatById(id).ConfigureAwait(false);
    }

    /// <summary>
    /// Searches for Chats
    /// </summary>
    /// <param name="personaId">Optional persona ID to filter by</param>
    /// <param name="userId">Optional user ID to filter by</param>
    /// <param name="title">Optional title to search for</param>
    /// <param name="pageNumber">Page number for pagination</param>
    /// <param name="pageSize">Page size for pagination</param>
    /// <returns>A paginated list of Chats</returns>
    public async Task<PaginatedResult<Chat>> SearchChats(Guid? personaId, Guid? userId, string? title, int pageNumber, int pageSize)
    {
        return await DataFacade.SearchChats(personaId, userId, title, pageNumber, pageSize).ConfigureAwait(false);
    }

    /// <summary>
    /// Updates a Chat
    /// </summary>
    /// <param name="chat">The Chat entity with updated data</param>
    /// <returns>The updated Chat</returns>
    public async Task<Chat> UpdateChat(Chat chat)
    {
        ChatValidator.Validate(chat);
        
        return await DataFacade.UpdateChat(chat).ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes a Chat
    /// </summary>
    /// <param name="id">The ID of the Chat to delete</param>
    /// <returns>True if the Chat was deleted, false if not found</returns>
    public async Task<bool> DeleteChat(Guid id)
    {
        return await DataFacade.DeleteChat(id).ConfigureAwait(false);
    }

    public void Dispose()
    {
        _personaManager?.Dispose();
        // DataFacade doesn't implement IDisposable, so no disposal needed
    }
}

