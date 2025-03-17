namespace TestApp.Domain.Requests
{
    /// <summary>
    /// Create Object Request
    /// </summary>
    public class CreateOrUpdateObjectRequest
    {
        /// <summary>
        /// Name
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Values
        /// </summary>
        public required Dictionary<string, dynamic> Values { get; set; }
    }
}
