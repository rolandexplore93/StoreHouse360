﻿using StoreHouse360.Application.Commands.Common;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Commands.Manufacturers
{
    [Authorize(Method = Method.Delete, Resource = Resource.Manufacturers)]
    public class DeleteManufacturerCommand : DeleteEntityCommand<int>
    {

    }
    public class DeleteManufacturerCommandHandler : DeleteEntityCommandHandler<DeleteManufacturerCommand, Manufacturer, int, IManufacturerRepository>
    {
        public DeleteManufacturerCommandHandler(IManufacturerRepository repository) : base(repository)
        {
        }
    }
}
