namespace TestApp.Domain.Entities
{
    /// <summary>
    /// Create or Update Object Base
    /// </summary>
    public class CreateOrUpdateObjectBase
    {
        public required string Name { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        public required dynamic Data { get; set; }
    }
}
