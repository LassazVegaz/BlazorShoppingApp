namespace UsersService.Options;

public class UserOptions
{
    public int EmailUpdateIntervalInDays { get; set; }

    /// <summary>
    /// Credits to be given to a new user
    /// </summary>
    public double InitialCredits { get; set; }
}
