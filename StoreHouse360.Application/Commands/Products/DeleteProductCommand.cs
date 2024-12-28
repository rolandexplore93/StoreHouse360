﻿using StoreHouse360.Application.Commands.Common;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Commands.Products
{
    public class DeleteProductCommand : DeleteEntityCommand<int>
    {

    }
    public class DeleteProductCommandHandler : DeleteEntityCommandHandler<DeleteProductCommand, Product, int, IProductRepository>
    {
        public DeleteProductCommandHandler(IProductRepository repository) : base(repository)
        {
        }
    }
}