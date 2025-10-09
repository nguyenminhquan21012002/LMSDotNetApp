using Course.Core.Application.DTOs;
using MediatR;

namespace Course.Core.Application.Commands
{
    public class CreateResourceCommand : IRequest<ResourceDTO>
    {
        public CreateResourceDTO ResourceData { get; set; }

        public CreateResourceCommand(CreateResourceDTO resourceData)
        {
            ResourceData = resourceData;
        }
    }
}
