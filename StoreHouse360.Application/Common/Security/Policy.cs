﻿namespace StoreHouse360.Application.Common.Security
{
    public class Policy
    {
        public Resource Resource {  get; set; } 
        public Method Method {  get; set; }

        public Policy(Resource resource, Method method)
        {
            Resource = resource;
            Method = method;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Policy policy)
                return policy.Resource.Equals(Resource) && policy.Method.Equals(Method);

            return false;
        }
    }

    public enum Resource
    {
        Users, 
        Accounts,
        Categories,
        Countries,
        Manufacturers,
        Units,
        Products,
        Warehouses,
        Currencies,
        Roles,
        Invoices,
        Journals,
    }

    public enum Method
    {
        Read, Write, Delete, Update
    }
}
