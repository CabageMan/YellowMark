namespace YellowMark.Contracts.UsersInfos;

/// <summary>
/// Request data model for getting users by name.
/// </summary>
public class UserInfoByNameRequest
{
    /// <summary>
    /// User name.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Search users older 18.
    /// </summary>
    public bool BeOver18 { get; set; }
}
