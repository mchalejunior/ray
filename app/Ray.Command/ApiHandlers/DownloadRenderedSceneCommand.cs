using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Ray.Command.ApiHandlers
{
    public class DownloadRenderedSceneCommand : IRequest<byte[]>
    {
        public Guid CorrelationId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }

    public class DownloadRenderedSceneHandler : IRequestHandler<DownloadRenderedSceneCommand, byte[]>
    {
        public async Task<byte[]> Handle(DownloadRenderedSceneCommand request, CancellationToken cancellationToken)
        {
            // TODO: logging / error handling etc.

            return await System.IO.File.ReadAllBytesAsync(request.FilePath, cancellationToken);
        }
    }
}
