using System.ComponentModel.DataAnnotations;

namespace ApiProtection.Models;

public class UserModel
{
    [Required]
    [Range(0, int.MaxValue)]
    public int ID { get; set; }


    [Required]
    [MinLength(5)]
    [MaxLength(10)]
    public string FirstName { get; set; }


    [Required]
    [MinLength(5)]
    [MaxLength(10)]
    public string LastName { get; set; }

    [EmailAddress]
    public string EmailAddress { get; set; }

    [Phone]
    public string PhoneNumber { get; set; }

    [Url]
    public string HomePage { get; set; }

    [Range(0, 5)]
    public int NumberofVehicles { get; set; }
}
