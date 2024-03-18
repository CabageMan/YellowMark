using System.ComponentModel.DataAnnotations;

namespace YellowMark.Contracts;

public class CreateUserRequest
{
    /// <summary>
    /// User's first name.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// User's middle name.
    /// </summary>
    public string MiddleName { get; set; }

    /// <summary>
    /// User's last name.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// User's email. 
    /// </summary>
    [EmailAddress(ErrorMessage = "Enter a valid email address")]
    public string Email { get; set; }

    /// <summary>
    /// User's phone. 
    /// </summary>
    [Phone(ErrorMessage = "Enter a valid phone number")]
    public string Phone { get; set; }
}
