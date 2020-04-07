using DotnetCorePostgreSQL.Api.Data;
using DotnetCorePostgreSQL.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DotnetCorePostgreSQL.Api.Services
{
    public class SampleService : ISampleService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly AppDbContext _appDbContext;

        public List<Post> Posts { get; set; }

        public SampleService(AppDbContext appDbContext, IHttpClientFactory clientFactory)
        {
            _appDbContext = appDbContext;
            _clientFactory = clientFactory;
        }

        public async Task<List<Post>> GetJsonDatasFromRemoteServer()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://jsonplaceholder.typicode.com/posts");
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();

                Posts = await JsonSerializer.DeserializeAsync<List<Post>>(responseStream);
            }


            return Posts;
        }

        public async Task<bool> InsertDataToDatabase(List<Post> posts)
        {
            _appDbContext.Post.AddRange(posts);

            var saveResult = await _appDbContext.SaveChangesAsync();

            return saveResult > 0;
        }
    }
}
