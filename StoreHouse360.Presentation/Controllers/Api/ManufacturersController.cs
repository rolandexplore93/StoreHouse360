﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Commands.Manufacturers;
using StoreHouse360.Application.Queries.Manufacturers;
using StoreHouse360.DTO.Common;
using StoreHouse360.DTO.Manufacturers;
using StoreHouse360.DTO.Pagination;
using StoreHouse360.Presentation.DTO.Common.Responses;

namespace StoreHouse360.Controllers.Api
{
    [Authorize]
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
        public async Task<ActionResult<BaseResponse<PaginationVM<ManufacturerVM>>>> GetManufacturers([FromQuery] ManufacturersQueryParams request)
        {
            var manufacturers = await Mediator.Send(request.AsQuery<GetAllManufacturersQuery>(_mapper));
            return Ok(manufacturers.ToViewModels<ManufacturerVM>(_mapper));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<ManufacturerVM>>> GetManufacturer(int id)
        {
            var manufacturers = await Mediator.Send(new GetManufacturerQuery { Id = id });
            return Ok(manufacturers.ToViewModel<ManufacturerVM>(_mapper));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<ManufacturerVM>>> UpdateManufacturer(int id, UpdateManufacturerRequestDTO request)
        {
            var command = _mapper.Map<UpdateManufacturerCommand>(request);
            command.Id = id;
            var resultId = await Mediator.Send(command);
            return await GetManufacturer(resultId);
        }

        [HttpDelete("{id}")]
        public async Task DeleteManufacturer(int id)
        {
            await Mediator.Send(new DeleteManufacturerCommand() { key = id });
        }
    }
}