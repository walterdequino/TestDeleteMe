using TestApp.Domain.Requests;
using TestApp.Domain.ViewModels;

namespace TestApp.Domain.Interfaces
{
    /// <summary>
    /// Interface of Object Service
    /// </summary>
    public interface IObjectService
    {
        /// <summary>
        /// Get All Objects
        /// </summary>
        /// <returns>List of All Objects</returns>
        Task<IList<ObjectViewModel>> GetAll();

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="id">Id to filter</param>
        /// <returns>Object by Id</returns>
        Task<ObjectViewModel> GetById(string id);

        /// <summary>
        /// Create Object
        /// </summary>
        /// <param name="request">Request creation</param>
        /// <returns>Id of Object Created</returns>
        Task<string> Create(CreateOrUpdateObjectRequest request);

        /// <summary>
        /// Update Object
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task Update(string id, CreateOrUpdateObjectRequest request);
    }
}
