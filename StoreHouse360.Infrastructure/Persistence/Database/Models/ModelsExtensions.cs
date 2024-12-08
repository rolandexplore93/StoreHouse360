﻿using System.ComponentModel.DataAnnotations;

namespace StoreHouse360.Infrastructure.Persistence.Database.Models
{
    public static class ModelsExtensions
    {
        public static object Id(this IDatabaseModel model)
        {
            var field = model.GetType().GetField("Id");

            if (field == null)
            {
                throw new ValidationException(model.GetType() + " has no Id field");
            }
            return field.GetValue(model)!;
        }
    }
}