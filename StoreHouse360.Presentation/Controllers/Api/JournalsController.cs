using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Queries.Accounting;
using StoreHouse360.Application.Settings;
using StoreHouse360.DTO.Common;
using StoreHouse360.DTO.Journals;
using StoreHouse360.DTO.Pagination;
using StoreHouse360.Presentation.DTO.Common.Responses;

namespace StoreHouse360.Controllers.Api
{
    [Authorize]
    public class JournalsController : ApiControllerBase
    {
        public JournalsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<JournalVM>>> GetJournals([FromQuery] GetAllJournalsQueryParams request)
        {
            var query = request.AsQuery<GetJournalEntriesQuery>(_mapper);
            var journals = await Mediator.Send(query);
            var viewmodel = journals.ToViewModel<JournalVM>(_mapper);
            return Ok(viewmodel);
        }

        [HttpGet("cashDrawer")]
        public async Task<ActionResult<BaseResponse<JournalVM>>> GetJournals([FromQuery] GetAllCashDrawerJournalsQueryParams request, [FromServices] AppSettings settings)
        {
            var query = request.AsQuery<GetJournalEntriesQuery>(_mapper);
            query.FromAccountId = settings.DefaultMainCashDrawerAccountId;
            var journals = await Mediator.Send(query);
            var viewmodel = journals.ToViewModel<JournalVM>(_mapper);
            return Ok(viewmodel);
        }
    }
}
