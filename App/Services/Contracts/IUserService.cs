namespace App.Services.Contracts
{
    using Views.ViewModels.User;

    public interface IUserService
    {
        bool Add(string username, string password);

        bool UserExists(string username, string password);

        UserProfileViewModel GetProfile(string username);
    }
}
