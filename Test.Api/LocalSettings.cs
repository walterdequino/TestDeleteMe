using TestApp.Domain.Filters;
using TestApp.Domain.Interfaces;

namespace Test.Api
{
    /// <summary>
    /// Local Settings
    /// </summary>
    public class LocalSettings : ILocalSettings
    {
        /// <summary>
        /// Configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public LocalSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Get Specifig Setting
        /// </summary>
        /// <typeparam name="T">Type of Setting</typeparam>
        /// <param name="name"> Name of Setting </param>
        /// <returns></returns>
        public T Get<T>(string name)
        {
            try
            {
                return (T)Convert.ChangeType(_configuration.GetSection(name).Value!, typeof(T));
            }
            catch (Exception ex)
            {
                throw new InternalValidationException($"LocalSettings - Get: {ex.Message}");
            }
        }

        /// <summary>
        /// Get Complete Path for Specific File
        /// </summary>
        /// <param name="foldersContainer"> folderOne\\subFolder </param>
        /// <param name="fileName">someFileName.txt</param>
        /// <returns></returns>
        public string GetPathForFile(string foldersContainer, string fileName)
        {
            try
            {
                return Path.Combine(AppContext.BaseDirectory, $"{foldersContainer}\\{fileName}");
            }
            catch (Exception ex)
            {
                throw new InternalValidationException($"LocalSettings - GetPathForFile: {ex.Message}");
            }
        }

        /// <summary>
        /// Get Connection String
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="InternalValidationException"></exception>
        public string GetConnectionString(string name)
        {
            try
            {
                return _configuration.GetConnectionString(name) ?? string.Empty;
            }
            catch (Exception ex)
            {
                throw new InternalValidationException($"LocalSettings - GetConnectionString: {ex.Message}");
            }
        }
    }
}
