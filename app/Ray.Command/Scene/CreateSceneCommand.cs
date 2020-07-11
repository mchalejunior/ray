using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ray.Serialize.Scene;

namespace Ray.Command.Scene
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
