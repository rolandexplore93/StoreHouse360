using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Commands.Manufacturers;
using StoreHouse360.Application.Queries.Manufacturers;
using StoreHouse360.DTO.Common;
using StoreHouse360.DTO.Manufacturers;
using StoreHouse360.DTO.Pagination;
using StoreHouse360.Presentation.DTO.Common.Responses;

namespace StoreHouse360.Controllers.Api
{
    public class ManufacturersController : ApiControllerBase
    {
        public ManufacturersController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }
        [HttpPost]
        public async Task<ActionResult<NoDataResponse>> CreateManufacturer(CreateManufacturerRequestDTO request)
        {
            var command = _mapper.Map<CreateManufacturerCommand>(request);
            var result = await Mediator.Send(command);
            return Ok(result);
            //var manufacturer = await Mediator.Send(new GetManufacturerQuery { Id = result });
            //return Ok(manufacturer.ToViewModel<ManufacturerVM>(_mapper));
        }
        [HttpGet]
        public async Task<ActionResult<BaseResponse<PaginationVM<ManufacturerVM>>>> GetManufacturers([FromQuery] PaginationRequestParams request)
        {
            var manufacturers = await Mediator.Send(request.AsQuery(new GetAllManufacturersQuery()));
            return Ok(manufacturers.ToViewModels<ManufacturerVM>(_mapper));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<ManufacturerVM>>> GetManufacturer(int id)
        {
            var manufacturers = await Mediator.Send(new GetManufacturerQuery
            {
                Id = id
            });
            return Ok(manufacturers.ToViewModel<ManufacturerVM>(_mapper));
        }
    }
}