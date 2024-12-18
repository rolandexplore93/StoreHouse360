﻿using Microsoft.AspNetCore.Identity;
using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Infrastructure.Persistence.Database.Models
{
    public class ApplicationIdentityUser : IdentityUser<int>, IMapFrom<User>, IDatabaseModel
    {
    }
}
