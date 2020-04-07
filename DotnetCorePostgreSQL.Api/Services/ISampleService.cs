using DotnetCorePostgreSQL.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCorePostgreSQL.Api.Services
{
    public interface ISampleService
    {
        Task<List<Post>> GetJsonDatasFromRemoteServer();
        Task<bool> InsertDataToDatabase(List<Post> posts);
    }
}
