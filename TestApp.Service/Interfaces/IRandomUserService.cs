using TestApp.Domain.ViewModels.RandomUser;

namespace TestApp.Domain.Interfaces
{
    /// <summary>
    /// Interface of Random User Service
    /// </summary>
    public interface IRandomUserService
    {
        /// <summary>
        /// Get Random User
        /// </summary>
        /// <returns></returns>
        Task<RandomUserViewModel> GetRandomUser();

        /// <summary>
        /// Execute Hook
        /// </summary>
        /// <returns></returns>
        Task<string> ExecuteHook();
    }
}
