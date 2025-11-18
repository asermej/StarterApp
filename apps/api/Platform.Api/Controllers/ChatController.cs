using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Platform.Domain;
using Platform.Api.ResourcesModels;
using Platform.Api.Mappers;
using Platform.Api.Common;

namespace Platform.Api.Controllers;

/// <summary>
/// Controller for Chat management operations
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
[Authorize]
public class ChatController : ControllerBase
{
    private readonly DomainFacade _domainFacade;

    public ChatController(DomainFacade domainFacade)
    {
        _domainFacade = domainFacade;
    }

    /// <summary>
    /// Creates a new chat
    /// </summary>
    /// <param name="resource">The chat data</param>
    /// <returns>The created chat with its ID</returns>
    /// <response code="201">Returns the newly created chat</response>
    /// <response code="400">If the resource is invalid</response>
    /// <response code="401">If the user is not authenticated</response>
    [HttpPost]
    [ProducesResponseType(typeof(ChatResource), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    public async Task<ActionResult<ChatResource>> Create([FromBody] CreateChatResource resource)
    {
        var chat = ChatMapper.ToDomain(resource);
        var createdChat = await _domainFacade.CreateChat(chat);

        var response = ChatMapper.ToResource(createdChat);
        
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    /// <summary>
    /// Gets a chat by ID
    /// </summary>
    /// <param name="id">The ID of the chat</param>
    /// <returns>The chat if found</returns>
    /// <response code="200">Returns the chat</response>
    /// <response code="404">If the chat is not found</response>
    /// <response code="401">If the user is not authenticated</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ChatResource), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(401)]
    public async Task<ActionResult<ChatResource>> GetById(Guid id)
    {
        var chat = await _domainFacade.GetChatById(id);
        if (chat == null)
        {
            return NotFound($"Chat with ID {id} not found");
        }

        var response = ChatMapper.ToResource(chat);
        
        return Ok(response);
    }

    /// <summary>
    /// Searches for chats with pagination
    /// </summary>
    /// <param name="request">The search criteria</param>
    /// <returns>A paginated list of chats</returns>
    /// <response code="200">Returns the paginated results</response>
    /// <response code="401">If the user is not authenticated</response>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<ChatResource>), 200)]
    [ProducesResponseType(401)]
    public async Task<ActionResult<PaginatedResponse<ChatResource>>> Search([FromQuery] SearchChatRequest request)
    {
        var result = await _domainFacade.SearchChats(
            request.PersonaId, 
            request.UserId, 
            request.Title, 
            request.PageNumber, 
            request.PageSize);

        var response = new PaginatedResponse<ChatResource>
        {
            Items = ChatMapper.ToResource(result.Items),
            TotalCount = result.TotalCount,
            PageNumber = result.PageNumber,
            PageSize = result.PageSize
        };

        return Ok(response);
    }

    /// <summary>
    /// Updates a chat
    /// </summary>
    /// <param name="id">The ID of the chat</param>
    /// <param name="resource">The updated chat data</param>
    /// <returns>The updated chat</returns>
    /// <response code="200">Returns the updated chat</response>
    /// <response code="404">If the chat is not found</response>
    /// <response code="401">If the user is not authenticated</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ChatResource), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(401)]
    public async Task<ActionResult<ChatResource>> Update(Guid id, [FromBody] UpdateChatResource resource)
    {
        // Get existing chat first
        var existingChat = await _domainFacade.GetChatById(id);
        if (existingChat == null)
        {
            return NotFound($"Chat with ID {id} not found");
        }

        // Map update to domain object
        var chatToUpdate = ChatMapper.ToDomain(resource, existingChat);
        var updatedChat = await _domainFacade.UpdateChat(chatToUpdate);

        var response = ChatMapper.ToResource(updatedChat);
        
        return Ok(response);
    }

    /// <summary>
    /// Deletes a chat
    /// </summary>
    /// <param name="id">The ID of the chat</param>
    /// <returns>No content on success</returns>
    /// <response code="204">If the chat was deleted</response>
    /// <response code="404">If the chat is not found</response>
    /// <response code="401">If the user is not authenticated</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(401)]
    public async Task<ActionResult> Delete(Guid id)
    {
        var deleted = await _domainFacade.DeleteChat(id);
        if (!deleted)
        {
            return NotFound($"Chat with ID {id} not found");
        }

        return NoContent();
    }

}

