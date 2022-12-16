using System.ComponentModel.DataAnnotations;

namespace GiftingApi.Models;


public record PersonItemResponse(string Id, string FirstName, string LastName);

public record PersonResponse(List<PersonItemResponse> Data);

public record PersonCreateRequest : IValidatableObject
{
    [Required, MaxLength(10)]
    public string FirstName { get; init; } = string.Empty;
    [Required]
    public string LastName { get; init; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (FirstName.Trim().ToUpperInvariant() == "DARTH" && LastName.Trim().ToUpperInvariant() == "VADER")
        {
            yield return new ValidationResult("We have a strict no Sith policy", new string[] { nameof(FirstName), nameof(LastName) });
        }
    }
}