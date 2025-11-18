using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Platform.Domain;

public sealed partial class DomainFacade
{
    public async Task<Persona> CreatePersona(Persona persona)
    {
        return await PersonaManager.CreatePersona(persona);
    }

    public async Task<Persona?> GetPersonaById(Guid id)
    {
        return await PersonaManager.GetPersonaById(id);
    }

    public async Task<PaginatedResult<Persona>> SearchPersonas(string? firstName, string? lastName, string? displayName, string? createdBy, string? sortBy, int pageNumber, int pageSize)
    {
        return await PersonaManager.SearchPersonas(firstName, lastName, displayName, createdBy, sortBy, pageNumber, pageSize);
    }

    public async Task<Persona> UpdatePersona(Persona persona)
    {
        return await PersonaManager.UpdatePersona(persona);
    }

    public async Task<bool> DeletePersona(Guid id)
    {
        return await PersonaManager.DeletePersona(id);
    }

    public async Task<Persona> UpdatePersonaTraining(Guid personaId, string trainingContent)
    {
        return await PersonaManager.UpdatePersonaTraining(personaId, trainingContent);
    }

    public async Task<string> GetPersonaTraining(Guid personaId)
    {
        return await PersonaManager.GetPersonaTraining(personaId);
    }
}

