﻿namespace YellowMark.Contracts.UsersInfos;

/// <summary>
/// Request data model for user creation.
/// </summary>
public class CreateUserInfoRequest
{
    /// <summary>
    /// Account id related to the current user info.
    /// </summary>
    public Guid AccountId { get; set; }
    
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
    /// User phone. 
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// Set user phone visibility to other users in ads. Default is false.
    /// </summary>
    public bool ShowPhone { get; set; } = false;

    /// <summary>
    /// User's birth date.
    /// </summary>
    public DateOnly BirthDate { get; set; }
}
