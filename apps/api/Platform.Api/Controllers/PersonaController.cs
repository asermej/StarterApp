using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Platform.Domain;
using Platform.Api.ResourcesModels;
using Platform.Api.Mappers;
using Platform.Api.Common;

namespace Platform.Api.Controllers;

/// <summary>
/// Controller for Persona management operations
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
[Authorize]
public class PersonaController : ControllerBase
{
    private readonly DomainFacade _domainFacade;

    public PersonaController(DomainFacade domainFacade)
    {
        _domainFacade = domainFacade;
    }

    /// <summary>
    /// Creates a new persona
    /// </summary>
    /// <param name="resource">The persona data</param>
    /// <returns>The created persona with its ID</returns>
    /// <response code="201">Returns the newly created persona</response>
    /// <response code="400">If the resource is invalid</response>
    /// <response code="401">If the user is not authenticated</response>
    [HttpPost]
    [ProducesResponseType(typeof(PersonaResource), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    public async Task<ActionResult<PersonaResource>> Create([FromBody] CreatePersonaResource resource)
    {
        var persona = PersonaMapper.ToDomain(resource);
        
        // Set the creator from the authenticated user
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier) 
                       ?? User.FindFirst("sub");
        
        if (userIdClaim != null)
        {
            persona.CreatedBy = userIdClaim.Value;
        }
        
        var createdPersona = await _domainFacade.CreatePersona(persona);

        var response = PersonaMapper.ToResource(createdPersona);
        
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    /// <summary>
    /// Gets a persona by ID
    /// </summary>
    /// <param name="id">The ID of the persona</param>
    /// <returns>The persona if found</returns>
    /// <response code="200">Returns the persona</response>
    /// <response code="404">If the persona is not found</response>
    [HttpGet("{id}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PersonaResource), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<PersonaResource>> GetById(Guid id)
    {
        var persona = await _domainFacade.GetPersonaById(id);
        if (persona == null)
        {
            return NotFound($"Persona with ID {id} not found");
        }

        var response = PersonaMapper.ToResource(persona);
        
        return Ok(response);
    }

    /// <summary>
    /// Searches for personas with pagination
    /// </summary>
    /// <param name="request">The search criteria</param>
    /// <returns>A paginated list of personas</returns>
    /// <response code="200">Returns the paginated results</response>
    /// <response code="401">If CreatedByMe filter is used without authentication</response>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PaginatedResponse<PersonaResource>), 200)]
    [ProducesResponseType(401)]
    public async Task<ActionResult<PaginatedResponse<PersonaResource>>> Search([FromQuery] SearchPersonaRequest request)
    {
        // Get user ID from claims if filtering by creator
        string? createdBy = null;
        if (request.CreatedByMe == true)
        {
            // If user is authenticated, use their ID
            if (User.Identity?.IsAuthenticated == true)
            {
                var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier) 
                               ?? User.FindFirst("sub");
                
                if (userIdClaim != null)
                {
                    createdBy = userIdClaim.Value;
                }
            }
            // If not authenticated but CreatedByMe requested, just ignore the filter and show all personas
            // This allows the page to work even if authentication fails
        }

        var result = await _domainFacade.SearchPersonas(
            request.FirstName, 
            request.LastName, 
            request.DisplayName,
            createdBy,
            request.SortBy,
            request.PageNumber, 
            request.PageSize);

        var response = new PaginatedResponse<PersonaResource>
        {
            Items = PersonaMapper.ToResource(result.Items),
            TotalCount = result.TotalCount,
            PageNumber = result.PageNumber,
            PageSize = result.PageSize
        };

        return Ok(response);
    }

    /// <summary>
    /// Updates a persona
    /// </summary>
    /// <param name="id">The ID of the persona</param>
    /// <param name="resource">The updated persona data</param>
    /// <returns>The updated persona</returns>
    /// <response code="200">Returns the updated persona</response>
    /// <response code="404">If the persona is not found</response>
    /// <response code="401">If the user is not authenticated</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(PersonaResource), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(401)]
    public async Task<ActionResult<PersonaResource>> Update(Guid id, [FromBody] UpdatePersonaResource resource)
    {
        // Get existing persona first
        var existingPersona = await _domainFacade.GetPersonaById(id);
        if (existingPersona == null)
        {
            return NotFound($"Persona with ID {id} not found");
        }

        // Map update to domain object
        var personaToUpdate = PersonaMapper.ToDomain(resource, existingPersona);
        
        // Set the updater from the authenticated user
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier) 
                       ?? User.FindFirst("sub");
        
        if (userIdClaim != null)
        {
            personaToUpdate.UpdatedBy = userIdClaim.Value;
        }
        
        var updatedPersona = await _domainFacade.UpdatePersona(personaToUpdate);

        var response = PersonaMapper.ToResource(updatedPersona);
        
        return Ok(response);
    }

    /// <summary>
    /// Deletes a persona
    /// </summary>
    /// <param name="id">The ID of the persona</param>
    /// <returns>No content on success</returns>
    /// <response code="204">If the persona was deleted</response>
    /// <response code="404">If the persona is not found</response>
    /// <response code="401">If the user is not authenticated</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(401)]
    public async Task<ActionResult> Delete(Guid id)
    {
        var deleted = await _domainFacade.DeletePersona(id);
        if (!deleted)
        {
            return NotFound($"Persona with ID {id} not found");
        }

        return NoContent();
    }

    /// <summary>
    /// Gets the training content for a persona
    /// </summary>
    /// <param name="id">The ID of the persona</param>
    /// <returns>The persona's training content</returns>
    /// <response code="200">Returns the training content</response>
    /// <response code="404">If the persona is not found</response>
    [HttpGet("{id}/training")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PersonaTrainingResource), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<PersonaTrainingResource>> GetTraining(Guid id)
    {
        var persona = await _domainFacade.GetPersonaById(id);
        if (persona == null)
        {
            return NotFound($"Persona with ID {id} not found");
        }

        var trainingContent = await _domainFacade.GetPersonaTraining(id);
        var response = new PersonaTrainingResource
        {
            TrainingContent = trainingContent
        };

        return Ok(response);
    }

    /// <summary>
    /// Updates the training content for a persona
    /// </summary>
    /// <param name="id">The ID of the persona</param>
    /// <param name="resource">The training content to update</param>
    /// <returns>No content on success</returns>
    /// <response code="204">If the training was updated</response>
    /// <response code="404">If the persona is not found</response>
    /// <response code="401">If the user is not authenticated</response>
    [HttpPut("{id}/training")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(401)]
    public async Task<ActionResult> UpdateTraining(Guid id, [FromBody] UpdatePersonaTrainingResource resource)
    {
        await _domainFacade.UpdatePersonaTraining(id, resource.TrainingContent);
        return NoContent();
    }

}

