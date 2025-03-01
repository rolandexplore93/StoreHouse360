﻿using StoreHouse360.Domain.Exceptions;

namespace StoreHouse360.Application.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException() : this(null)
        {
        }

        public NotFoundException(string? message) : base(message, StatusCodes.NotFoundExceptionCode)
        {
        }

        public NotFoundException(string name, object key) : this($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }
}
