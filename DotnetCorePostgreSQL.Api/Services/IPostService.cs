using DotnetCorePostgreSQL.Api.Data.DTO;
using DotnetCorePostgreSQL.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCorePostgreSQL.Api.Services
{
    public interface IPostService
    {
        Task<List<Post>> GetJsonDatasFromRemoteServer();
        Task<bool> InsertDataToDatabase(List<Post> posts);
        Task<List<PostDto>> GetData();
        Task<int> GetCount();
    }
}
