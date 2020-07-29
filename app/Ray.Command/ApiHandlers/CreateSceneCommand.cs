using System;
using MediatR;
using Ray.Serialize.Scene;

namespace Ray.Command.ApiHandlers
{
    public class CreateSceneCommand : IRequest<Guid>
    {
        public SceneDto Scene { get; set; }
    }

    
    public class CreateSceneHandler : RequestHandler<CreateSceneCommand, Guid>
    {
        protected override Guid Handle(CreateSceneCommand request)
        {
            return Guid.NewGuid();
        }
    }
}
