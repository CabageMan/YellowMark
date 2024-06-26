﻿namespace YellowMark.Contracts.Account;

/// <summary>
/// Request data model for registration.
/// </summary>
public class CreateAccountRequest
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
    /// User's birth date.
    /// </summary>
    public DateOnly BirthDate { get; set; }

    /// <summary>
    /// Account email.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Account Phone number.
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// Account password.
    /// </summary>
    public string Password { get; set; } // TODO: Remove in future authentication with password and authentication with email/phone. 
}
