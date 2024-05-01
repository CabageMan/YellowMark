namespace YellowMark.Contracts.UsersInfos;

/// <summary>
/// Request data model for getting users by name (first, midle, last names).
/// </summary>
public class UserInfoByNameRequest
{
    /// <summary>
    /// User first, last or middle name.
    /// </summary>
    public string Name { get; set; }
}
