using System.ComponentModel.DataAnnotations.Schema;

namespace Platform.Domain;

/// <summary>
/// Represents a Persona in the domain
/// </summary>
[Table("personas")]
public class Persona : Entity
{
    [Column("first_name")]
    public string? FirstName { get; set; }

    [Column("last_name")]
    public string? LastName { get; set; }

    [Column("display_name")]
    public string DisplayName { get; set; } = string.Empty;

    [Column("profile_image_url")]
    public string? ProfileImageUrl { get; set; }

    [Column("training_file_path")]
    public string? TrainingFilePath { get; set; }
}

