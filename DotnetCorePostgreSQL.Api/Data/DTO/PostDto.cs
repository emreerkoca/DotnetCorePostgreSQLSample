using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCorePostgreSQL.Api.Data.DTO
{
    public class PostDto
    {
        public int RemoteId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
