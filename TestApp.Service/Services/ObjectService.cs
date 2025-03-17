using System.Text;

using AutoMapper;

using TestApp.Domain.Entities;
using TestApp.Domain.Filters;
using TestApp.Domain.Interfaces;
using TestApp.Domain.Requests;
using TestApp.Domain.ViewModels;

namespace TestApp.Domain.Services
{
    /// <summary>
    /// Object Service
    /// </summary>
    public class ObjectService : IObjectService
    {
        /// <summary>
        /// Serializer
        /// </summary>
        private readonly ISerializer _serializer;

        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="mapper"></param>
        public ObjectService(ISerializer serializer, IMapper mapper)
        {
            _serializer = serializer;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<IList<ObjectViewModel>> GetAll()
        {
            const string completeUrl = "https://api.restful-api.dev/objects";

            var client = new HttpClient();

            var result = await client.GetAsync(completeUrl);

            var content = await result.Content.ReadAsStringAsync();

            if (!result.IsSuccessStatusCode)
            {
                throw new HttpCustomException(_serializer.Serialize(new ExceptionHttpLog("GET", completeUrl, completeUrl, content)));
            }

            var objects = _serializer.TryDeserialize<List<ObjectBase<dynamic>>>(content);

            return _mapper.Map<List<ObjectViewModel>>(objects);
        }

        /// <inheritdoc/>
        public async Task<ObjectViewModel> GetById(string id)
        {
            const string baseUrl = "https://api.restful-api.dev/objects";

            var completeUrl = $"{baseUrl}/{id}";

            var client = new HttpClient();

            var result = await client.GetAsync(completeUrl);

            var content = await result.Content.ReadAsStringAsync();

            if (!result.IsSuccessStatusCode)
            {
                throw new HttpCustomException(_serializer.Serialize(new ExceptionHttpLog("GET", completeUrl, completeUrl, content)));
            }

            return _mapper.Map<ObjectViewModel>(_serializer.TryDeserialize<ObjectBase<dynamic>>(content));
        }

        /// <inheritdoc/>
        public async Task<string> Create(CreateOrUpdateObjectRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ArgumentNullException(nameof(request));
                //throw new BusinessException("Name is required");
            }

            var toCreate = _mapper.Map<CreateOrUpdateObjectBase>(request);

            const string completeUrl = "https://api.restful-api.dev/objects";

            var client = new HttpClient();

            var httpContent = new StringContent(_serializer.Serialize(toCreate), Encoding.UTF8, "application/json");

            var result = await client.PostAsync(completeUrl, httpContent);

            var content = await result.Content.ReadAsStringAsync();

            if (!result.IsSuccessStatusCode)
            {
                throw new HttpCustomException(_serializer.Serialize(new ExceptionHttpLog("POST", completeUrl, request, content)));
            }

            var responseObjectData = _serializer.TryDeserialize<ObjectCreationResult>(content);

            return responseObjectData.Id;
        }

        /// <inheritdoc/>
        public async Task Update(string id, CreateOrUpdateObjectRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ArgumentNullException(nameof(request));
                //throw new BusinessException("Name is required");
            }

            // Maybe GetById and new BusinessException(Not found with id);

            var toUpdate = _mapper.Map<CreateOrUpdateObjectBase>(request);

            var completeUrl = $"https://api.restful-api.dev/objects/{id}";

            var client = new HttpClient();

            var httpContent = new StringContent(_serializer.Serialize(toUpdate), Encoding.UTF8, "application/json");

            var result = await client.PutAsync(completeUrl, httpContent);

            var content = await result.Content.ReadAsStringAsync();

            if (!result.IsSuccessStatusCode)
            {
                throw new HttpCustomException(_serializer.Serialize(new ExceptionHttpLog("PUT", completeUrl, request, content)));
            }
        }
    }
}
