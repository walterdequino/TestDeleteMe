namespace TestApp.Domain.Interfaces
{
    /// <summary>
    /// Interface of Local Settings
    /// </summary>
    public interface ILocalSettings
    {
        /// <summary>
        /// Get Setting
        /// </summary>
        /// <typeparam name="T">Type of the Setting</typeparam>
        /// <param name="name">Name of the Setting</param>
        /// <returns></returns>
        T Get<T>(string name);

        /// <summary>
        /// Get Complete Path for Specific File
        /// </summary>
        /// <param name="foldersContainer"> folderOne\\subFolder </param>
        /// <param name="fileName">someFileName.txt</param>
        /// <returns></returns>
        string GetPathForFile(string foldersContainer, string fileName);

        /// <summary>
        /// Get Connection String
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        string GetConnectionString(string name);
    }
}