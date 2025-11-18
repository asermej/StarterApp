using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Platform.Api.Common;

namespace Platform.Api.ResourcesModels;

/// <summary>
/// Represents a Persona in API responses
/// </summary>
public class PersonaResource
{
    /// <summary>
    /// The unique identifier of the Persona
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The first name of the persona (optional)
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// The last name of the persona (optional)
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// The display name of the persona (required, unique)
    /// </summary>
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// The profile image URL of the persona (optional)
    /// </summary>
    public string? ProfileImageUrl { get; set; }

    /// <summary>
    /// When this Persona was created
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// When this Persona was last updated
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}

/// <summary>
/// Request model for creating a new Persona
/// </summary>
public class CreatePersonaResource
{
    /// <summary>
    /// The first name of the persona (optional)
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// The last name of the persona (optional)
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// The display name of the persona (required, unique)
    /// </summary>
    [Required(ErrorMessage = "DisplayName is required")]
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// The profile image URL of the persona (optional)
    /// </summary>
    public string? ProfileImageUrl { get; set; }
}

/// <summary>
/// Request model for updating an existing Persona
/// </summary>
public class UpdatePersonaResource
{
    /// <summary>
    /// The first name of the persona
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// The last name of the persona
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// The display name of the persona
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// The profile image URL of the persona
    /// </summary>
    public string? ProfileImageUrl { get; set; }
}

/// <summary>
/// Request model for searching Personas
/// </summary>
public class SearchPersonaRequest : PaginatedRequest
{
    /// <summary>
    /// Filter by FirstName
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Filter by LastName
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Filter by DisplayName
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// Filter by creator (if true, only return personas created by the authenticated user)
    /// </summary>
    public bool? CreatedByMe { get; set; }

    /// <summary>
    /// Filter by date range
    /// </summary>
    public DateTimeRange? CreatedAtRange { get; set; }

    /// <summary>
    /// Filter by date range
    /// </summary>
    public DateTimeRange? UpdatedAtRange { get; set; }

    // Note: SortBy is inherited from PaginatedRequest
    // Valid values: "popularity" (chat count), "alphabetical", "recent" (default)
}

/// <summary>
/// Request model for updating persona training content
/// </summary>
public class UpdatePersonaTrainingResource
{
    /// <summary>
    /// The training content for the persona (max 5,000 characters)
    /// </summary>
    [MaxLength(5000, ErrorMessage = "Training content cannot exceed 5,000 characters")]
    public string TrainingContent { get; set; } = string.Empty;
}

/// <summary>
/// Response model for persona training content
/// </summary>
public class PersonaTrainingResource
{
    /// <summary>
    /// The training content for the persona
    /// </summary>
    public string TrainingContent { get; set; } = string.Empty;
}

