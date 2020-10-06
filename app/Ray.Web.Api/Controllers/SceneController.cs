using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ray.Command.ApiHandlers;
using Ray.Serialize.Scene;
using Ray.Web.Api.Data;
using Ray.Web.Api.Infrastructure;

namespace Ray.Web.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SceneController : ControllerBase
    {
        private readonly IBackgroundTaskQueue _taskQueue;
        private readonly IMediator _mediator;
        private readonly ILogger<SceneController> _logger;
        private readonly IHostEnvironment _env;
        private readonly CancellationToken _applicationStopping;

        public SceneController(IBackgroundTaskQueue taskQueue, IMediator mediator, 
            ILogger<SceneController> logger, IHostEnvironment env, IHostApplicationLifetime applicationLifetime)
        {
            _taskQueue = taskQueue;
            _mediator = mediator;
            _logger = logger;
            _env = env;
            _applicationStopping = applicationLifetime.ApplicationStopping;
        }

        [HttpGet("example")]
        public IActionResult Example()
        {
            return new JsonResult(SampleData.GetSphereCentralWithPlanesExample());
        }

        [HttpPost]
        public IActionResult Post(SceneDto scene)
        {
            // TODO: input sanitize e.g.
            // best to accumulate errors and return a not ok with violations
            if (scene?.LightSource?.Position == null || scene?.LightSource?.Intensity == null)
            {
                throw new ArgumentOutOfRangeException(nameof(scene.LightSource));
            }
            if (scene?.Camera?.From == null || scene?.Camera?.To == null || scene?.Camera?.Up == null)
            {
                throw new ArgumentOutOfRangeException(nameof(scene.Camera));
            }


            var renderSceneCommand = GetRenderSceneCommand(scene);

            _taskQueue.QueueBackgroundWorkItem(async token =>
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }

                await _mediator.Send(renderSceneCommand, token);
            });

            return Ok(new CreateSceneResponse
            {
                CorrelationId = renderSceneCommand.CorrelationId,
                Message = "Scene submitted to renderer. TODO: info and URL to poll for rendered image."
            });
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            // TODO: input sanitize + handle error from command handler e.g. file not found.

            var downloadRenderedSceneCommand = GetDownloadRenderedSceneCommand(id);
            var byteStream = await _mediator.Send(downloadRenderedSceneCommand, _applicationStopping);

            return File(byteStream, "application/octet-stream", downloadRenderedSceneCommand.FileName);
        }



        #region Helper methods

        private RenderSceneCommand GetRenderSceneCommand(SceneDto scene)
        {
            var correlationId = Guid.NewGuid();
            var command = new RenderSceneCommand
            {
                Scene = scene,
                CorrelationId = correlationId,
                FilePath = GetSceneBitmapFileInfo(correlationId).PhysicalPath
            };

            return command;
        }

        private DownloadRenderedSceneCommand GetDownloadRenderedSceneCommand(Guid correlationId)
        {
            var fileInfo = GetSceneBitmapFileInfo(correlationId);
            var command = new DownloadRenderedSceneCommand
            {
                CorrelationId = correlationId,
                FilePath = fileInfo.PhysicalPath,
                FileName = fileInfo.Name
            };

            return command;
        }

        private IFileInfo GetSceneBitmapFileInfo(Guid correlationId)
        {
            var physicalProvider = _env.ContentRootFileProvider;
            var physicalPath = physicalProvider.GetFileInfo(correlationId + ".bmp");
            return physicalPath;
        }

        #endregion



    }
}
