namespace TestApp.Domain.ViewModels
{
    /// <summary>
    /// Object View Model
    /// </summary>
    public class ObjectViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        public required dynamic Data { get; set; }
    }
}
