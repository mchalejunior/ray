using System;

namespace Ray.Web.Api.Data
{
    public class CreateSceneResponse
    {
        public Guid CorrelationId { get; set; }
        public string PollUrl { get; set; }
        public string Message { get; set; }
    }
}
