using Course.Core.Application.DTOs;
using MediatR;

namespace Course.Core.Application.Commands
{
    public class UpdateResourceCommand : IRequest<ResourceDTO?>
    {
        public string Id { get; set; }
        public UpdateResourceDTO ResourceData { get; set; }

        public UpdateResourceCommand(string id, UpdateResourceDTO resourceData)
        {
            Id = id;
            ResourceData = resourceData;
        }
    }
}
