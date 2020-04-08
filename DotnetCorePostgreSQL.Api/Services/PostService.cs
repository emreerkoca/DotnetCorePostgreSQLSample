using AutoMapper;
using DotnetCorePostgreSQL.Api.Data;
using DotnetCorePostgreSQL.Api.Data.DTO;
using DotnetCorePostgreSQL.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DotnetCorePostgreSQL.Api.Services
{
    public class PostService : IPostService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IMapper _mapper;

        public List<Post> Posts { get; set; }

        public PostService(AppDbContext appDbContext, IHttpClientFactory clientFactory, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _clientFactory = clientFactory;
            _mapper = mapper;
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

        public async Task<List<PostDto>> GetData()
        {
            List<Post> allPost = await _appDbContext.Post.ToListAsync();

            return _mapper.Map<List<Post>, List<PostDto>>(allPost);
        }

        public async Task<int> GetCount()
        {
            return await _appDbContext.Post.CountAsync();
        }
    }
}
