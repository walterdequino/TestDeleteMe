namespace TestApp.Domain.ViewModels.RandomUser
{
    /// <summary>
    /// Random User View Model
    /// </summary>
    public class RandomUserViewModel
    {
        /// <summary>
        /// Name
        /// </summary>
        public RandomUserNameViewModel? Name { get; set; }

        /// <summary>
        /// Login Info
        /// </summary>
        public RandomUserLoginInfoViewModel? Login { get; set; }
    }
}
