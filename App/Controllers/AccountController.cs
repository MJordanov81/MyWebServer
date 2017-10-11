namespace App.Controllers
{
    using Models;
    using MyWebServer.Server.HTTP.Contracts;
    using MyWebServer.Server.HTTP.Response;
    using Services;
    using Services.Contracts;
    using System;
    using Views.ViewModels.User;
    using Constants;

    public class AccountController : Controller
    {
        private readonly IUserService users;

        public AccountController()
        {
            this.users = new UserService();
        }

        public IHttpResponse Login()
        {
            return this.HtmlViewResponse("/User/Login");
        }

        public IHttpResponse LoginPost(IHttpContext httpContext, LoginUserViewModel model)
        {
            if (this.users.UserExists(model.Username, model.Password))
            {
                this.AddCurrentUserInSession(model.Username, httpContext);

                return new RedirectResponse("/");
            }
            
            this.ShowError(ErrorConstants.InvalidUserDetails);
            return this.HtmlViewResponse("User/Login");

        }

        public IHttpResponse Register()
        {
            return this.HtmlViewResponse("/User/Register");
        }

        public IHttpResponse RegisterPost(IHttpContext httpContext, RegisterUserViewModel model)
        {
            string errorMessage;

            bool areRegistrationTokensValid = this.CheckRegistrationTokens(model, out errorMessage);

            if (!areRegistrationTokensValid)
            {
                this.ShowError(errorMessage);
                return this.HtmlViewResponse("/User/Register");
            }

            bool isSuccessfullyRegistered = this.users.Add(model.Username, model.Password);

            if (!isSuccessfullyRegistered)
            {
                this.ShowError(ErrorConstants.UnavailableUsername);
                return this.HtmlViewResponse("/User/Register");
            }

            this.AddCurrentUserInSession(model.Username, httpContext);

            return new RedirectResponse("/");
        }

        public IHttpResponse Profile(IHttpContext httpContext)
        {
            string username = httpContext.Request.Session.GetParameter(SessionParamsConstants.CurrentUser).ToString();

            UserProfileViewModel user = this.users.GetProfile(username);

            this.ViewData[ViewDataConstants.Username] = user.Username;
            this.ViewData[ViewDataConstants.RegistrationDate] = user.RegistrationDate;
            this.ViewData[ViewDataConstants.OrdersCount] = user.OrdersCount;

            return this.HtmlViewResponse("/user/profile");
        }

        private bool CheckRegistrationTokens(RegisterUserViewModel model, out string errorMessage)
        {
            errorMessage = String.Empty;

            if (model.Username.Length < 3 || model.Username.Length > 30)
            {
                errorMessage = ErrorConstants.InvalidUsername;
                return false;
            }
            if (model.Password.Length < 3 || model.Password.Length > 50)
            {
                errorMessage = ErrorConstants.InvalidPassword;
                return false;
            }

            if (model.Password != model.ConfirmPassword)
            {
                errorMessage = ErrorConstants.PasswordMismatch;
                return false;
            }

            return true;
        }

        private void AddCurrentUserInSession(string username, IHttpContext context)
        {
            context.Request.Session.Add(SessionParamsConstants.CurrentUser, username);
            context.Request.Session.Add(SessionParamsConstants.Cart, new Cart());
        }
    }
}
