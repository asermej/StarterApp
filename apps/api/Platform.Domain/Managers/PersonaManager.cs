using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Platform.Domain;

/// <summary>
/// Manages business operations for Persona entities
/// </summary>
internal sealed class PersonaManager : IDisposable
{
    private readonly ServiceLocatorBase _serviceLocator;
    private DataFacade? _dataFacade;
    private DataFacade DataFacade => _dataFacade ??= new DataFacade(_serviceLocator.CreateConfigurationProvider().GetDbConnectionString());
    private TrainingStorageManager? _storageManager;
    private TrainingStorageManager StorageManager => _storageManager ??= new TrainingStorageManager();

    public PersonaManager(ServiceLocatorBase serviceLocator)
    {
        _serviceLocator = serviceLocator;
    }

    /// <summary>
    /// Creates a new Persona
    /// </summary>
    /// <param name="persona">The Persona entity to create</param>
    /// <returns>The created Persona</returns>
    public async Task<Persona> CreatePersona(Persona persona)
    {
        PersonaValidator.Validate(persona);
        
        // Check for duplicate display name
        var existingPersonaWithDisplayName = await DataFacade.SearchPersonas(null, null, persona.DisplayName, null, null, 1, 1).ConfigureAwait(false);
        if (existingPersonaWithDisplayName.Items.Any())
        {
            throw new PersonaDuplicateDisplayNameException($"A persona with display name '{persona.DisplayName}' already exists.");
        }
        
        return await DataFacade.AddPersona(persona).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets a Persona by ID
    /// </summary>
    /// <param name="id">The ID of the Persona to get</param>
    /// <returns>The Persona if found, null otherwise</returns>
    public async Task<Persona?> GetPersonaById(Guid id)
    {
        return await DataFacade.GetPersonaById(id).ConfigureAwait(false);
    }

    /// <summary>
    /// Searches for Personas
    /// </summary>
    /// <param name="firstName">Optional first name to search for</param>
    /// <param name="lastName">Optional last name to search for</param>
    /// <param name="displayName">Optional display name to search for</param>
    /// <param name="createdBy">Optional creator ID to filter by</param>
    /// <param name="sortBy">Sort order: "popularity" (chat count), "alphabetical", "recent" (default)</param>
    /// <param name="pageNumber">Page number for pagination</param>
    /// <param name="pageSize">Page size for pagination</param>
    /// <returns>A paginated list of Personas</returns>
    public async Task<PaginatedResult<Persona>> SearchPersonas(string? firstName, string? lastName, string? displayName, string? createdBy, string? sortBy, int pageNumber, int pageSize)
    {
        return await DataFacade.SearchPersonas(firstName, lastName, displayName, createdBy, sortBy, pageNumber, pageSize).ConfigureAwait(false);
    }

    /// <summary>
    /// Updates a Persona
    /// </summary>
    /// <param name="persona">The Persona entity with updated data</param>
    /// <returns>The updated Persona</returns>
    public async Task<Persona> UpdatePersona(Persona persona)
    {
        PersonaValidator.Validate(persona);
        
        // Check for duplicate display name (excluding the current persona)
        var existingPersonaWithDisplayName = await DataFacade.SearchPersonas(null, null, persona.DisplayName, null, null, 1, 1).ConfigureAwait(false);
        var duplicatePersona = existingPersonaWithDisplayName.Items.FirstOrDefault();
        if (duplicatePersona != null && duplicatePersona.Id != persona.Id)
        {
            throw new PersonaDuplicateDisplayNameException($"A persona with display name '{persona.DisplayName}' already exists.");
        }
        
        return await DataFacade.UpdatePersona(persona).ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes a Persona
    /// </summary>
    /// <param name="id">The ID of the Persona to delete</param>
    /// <returns>True if the Persona was deleted, false if not found</returns>
    public async Task<bool> DeletePersona(Guid id)
    {
        return await DataFacade.DeletePersona(id).ConfigureAwait(false);
    }

    /// <summary>
    /// Updates the general training data for a persona
    /// </summary>
    /// <param name="personaId">The persona ID</param>
    /// <param name="trainingContent">The training content (max 5,000 characters)</param>
    /// <returns>The updated Persona</returns>
    public async Task<Persona> UpdatePersonaTraining(Guid personaId, string trainingContent)
    {
        var persona = await GetPersonaById(personaId);
        if (persona == null)
        {
            throw new PersonaNotFoundException($"Persona with ID {personaId} not found.");
        }

        // Delete old training file if it exists
        if (!string.IsNullOrWhiteSpace(persona.TrainingFilePath))
        {
            StorageManager.DeleteTraining(persona.TrainingFilePath);
        }

        // Save new training content and get URL
        persona.TrainingFilePath = await StorageManager.SaveGeneralTraining(personaId, trainingContent);

        // Update persona with new training file path
        return await DataFacade.UpdatePersona(persona).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets the general training content for a persona
    /// </summary>
    /// <param name="personaId">The persona ID</param>
    /// <returns>The training content, or empty string if not found</returns>
    public async Task<string> GetPersonaTraining(Guid personaId)
    {
        var persona = await GetPersonaById(personaId);
        if (persona == null)
        {
            return string.Empty;
        }

        return await StorageManager.GetTrainingFromUrl(persona.TrainingFilePath);
    }

    public void Dispose()
    {
        // DataFacade doesn't implement IDisposable, so no disposal needed
    }
}

