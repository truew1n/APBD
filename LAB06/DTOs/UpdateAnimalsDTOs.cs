using System.ComponentModel.DataAnnotations;

namespace LAB06.DTOs
{
    public record UpdateAnimalRequest(
        [Required][MaxLength(200)] string Name,
        [Required][MaxLength(200)] string Description,
        [Required][MaxLength(200)] string Category,
        [Required][MaxLength(200)] string Area
    );

}
