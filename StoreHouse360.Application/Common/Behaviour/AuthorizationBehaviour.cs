using MediatR;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Exceptions;
using StoreHouse360.Application.Services.Identity;
using System.Reflection;

namespace StoreHouse360.Application.Common.Behaviour
{
    public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IIdentityService _identityService;
        public AuthorizationBehaviour(ICurrentUserService currentUserService, IIdentityService identityService)
        {
            _currentUserService = currentUserService;
            _identityService = identityService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var authorizeAttributes = request?.GetType().GetCustomAttributes<AuthorizeAttribute>(); // Get the attribute (Method & resource) of the requested endpoint

            if (authorizeAttributes != null)
            {
                authorizeAttributes = authorizeAttributes.ToList();

                // User must be authenticated
                int? userId = _currentUserService.UserId;
                if (userId == null) 
                    throw new UnauthorizedAccessException();

                var authorizeAttributesWithPolicies = authorizeAttributes;

                // Create instance of Policy
                var policies = authorizeAttributesWithPolicies.Select(a => new Policy(a.Resource, a.Method))
                    .Distinct()
                    .ToList();
                    
                // User must be authorized
                var authorized = await _identityService.AuthorizeAsync((int)userId!, policies);

                if (!authorized) 
                    throw new ForbiddenAccessException();
            }

            return await next();
        }
    }
}
