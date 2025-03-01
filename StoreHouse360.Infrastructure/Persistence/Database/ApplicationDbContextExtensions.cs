﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using StoreHouse360.Infrastructure.Persistence.Database.SeedData;
using StoreHouse360.Application.Services.Settings;

namespace StoreHouse360.Infrastructure.Persistence.Database
{
    public static class ApplicationDbContextExtensions
    {
        public static void ApplyGlobalFilters<TkInterface>(this ModelBuilder modelBuilder, Expression<Func<TkInterface, bool>> expresson)
        {
            var entityTypes = modelBuilder.Model.GetEntityTypes();
            var entities = entityTypes
                .Where(e => e.ClrType.GetInterface(typeof(TkInterface).Name) != null)
                .Select(e => e.ClrType);

            foreach (var entity in entities)
            {
                var newParameter = Expression.Parameter(entity);
                var newBody = ReplacingExpressionVisitor.Replace(expresson.Parameters.Single(), newParameter, expresson.Body);
                modelBuilder.Entity(entity).HasQueryFilter(Expression.Lambda(newBody, newParameter));
            }
        }

        public static Task ProcessDataSeeding(this ApplicationDbContext dbContext, ISeedToDatabase seedToDatabase, IAppSettingsProvider settingsProvider)
        {
            return seedToDatabase.Seed(dbContext, settingsProvider);
        }
    }
}
