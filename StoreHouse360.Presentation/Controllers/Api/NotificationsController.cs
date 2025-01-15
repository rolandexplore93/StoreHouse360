using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Queries.Notifications;
using StoreHouse360.DTO.Common;
using StoreHouse360.DTO.Notifications;
using StoreHouse360.DTO.Pagination;
using StoreHouse360.Presentation.DTO.Common.Responses;

namespace StoreHouse360.Controllers.Api
{
    public class NotificationsController : ApiControllerBase
    {
        public NotificationsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<PaginationVM<NotificationVM>>>> GetAll([FromQuery] GetNotificationsQueryParams request)
        {
            var query = _mapper.Map<GetAllNotificationsQuery>(request);
            var notificationsPage = await Mediator.Send(query);
            return Ok(notificationsPage.ToViewModel<NotificationVM>(_mapper));
        }
    }
}
