using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Queries.Notifications;
using StoreHouse360.Application.Queries.Products;
using StoreHouse360.DTO.Notifications;
using StoreHouse360.DTO.Pagination;
using StoreHouse360.DTO.Products;
using StoreHouse360.Presentation.DTO.Common.Responses;

namespace StoreHouse360.Controllers.Api
{
    public class NotificationsController : ApiControllerBase
    {
        public NotificationsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<PaginationVM<NotificationVM>>>> GetAll([FromQuery] GetNotificationsQueryParams request)
        {
            var query = _mapper.Map<GetAllNotificationsQuery>(request);
            var notificationsPage = await Mediator.Send(query);

            var productsQuery = new GetAllProductsQuery();
            var products = (await Mediator.Send(productsQuery))
                .AsEnumerable()
                .Where(product => notificationsPage.Any(notify => notify.ObjectId == product.Id))
                .Select(productEntity => _mapper.Map<ProductVM>(productEntity));

            var notificationsVM = notificationsPage
                .Select(notification => new NotificationVM
                {
                    Id = notification.Id,
                    NotificationType = notification.NotificationType,
                    Product = products.First(product => product.Id == notification.ObjectId),
                    IsValid = notification.IsValid
                });


            var notificationsVMPage = new PaginationVM<NotificationVM>
            {
                Data = notificationsVM,
                CurrentPage = notificationsPage.CurrentPage,
                PageSize = notificationsPage.PageSize,
                PagesCount = notificationsPage.PagesCount,
                RowsCount = notificationsPage.RowsCount,
            };

            return Ok(notificationsVMPage);
        }
    }
}
