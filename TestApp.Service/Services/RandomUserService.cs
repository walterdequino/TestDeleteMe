using System.Text;

using AutoMapper;

using TestApp.Domain.Entities;
using TestApp.Domain.Entities.RandomUser;
using TestApp.Domain.Filters;
using TestApp.Domain.Interfaces;
using TestApp.Domain.ViewModels.RandomUser;

namespace TestApp.Domain.Services
{
    /// <summary>
    /// Random User Service
    /// </summary>
    public class RandomUserService : IRandomUserService
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
        public RandomUserService(ISerializer serializer, IMapper mapper)
        {
            _serializer = serializer;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<RandomUserViewModel> GetRandomUser()
        {
            var random = await GenerateRandomUser();

            return _mapper.Map<RandomUserViewModel>(random);
        }

        /// <inheritdoc/>
        public async Task<string> ExecuteHook()
        {
            var randonUser = await GenerateRandomUser();

            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Surtechnology", "6E3F37EF-2DBC-4062-B974-5812DCB0B2AC");

            var toCreate = new { randonUser.Name, randonUser.Login };

            var completeUrl = $"https://webhook.link/{randonUser.Id?.Value}";

            var httpContent = new StringContent(_serializer.Serialize(toCreate), Encoding.UTF8, "application/json");

            var result = await client.PostAsync(completeUrl, httpContent);

            var content = await result.Content.ReadAsStringAsync();

            if (!result.IsSuccessStatusCode)
            {
                throw new HttpCustomException(_serializer.Serialize(new ExceptionHttpLog("POST", completeUrl, completeUrl, content)));
            }

            return content;
        }

        /// <summary>
        /// Generate Random User
        /// </summary>
        /// <returns></returns>
        /// <exception cref="HttpCustomException"></exception>
        private async Task<RandomUser> GenerateRandomUser()
        {
            const string completeUrl = "https://randomuser.me/api";

            var client = new HttpClient();

            var result = await client.GetAsync(completeUrl);

            var content = await result.Content.ReadAsStringAsync();

            if (!result.IsSuccessStatusCode)
            {
                throw new HttpCustomException(_serializer.Serialize(new ExceptionHttpLog("GET", completeUrl, completeUrl, content)));
            }

            var response = _serializer.TryDeserialize<RandomUserResponse>(content);

            var random = response.Results.FirstOrDefault() ?? new RandomUser();

            return random;
        }
    }
}
