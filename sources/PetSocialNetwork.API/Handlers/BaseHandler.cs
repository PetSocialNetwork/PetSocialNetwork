using MediatR;
using PetSocialNetwork.Data;

namespace PetSocialNetwork.API.Handlers
{
    public abstract class BaseHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        protected readonly PetSocialNetworkDbContext _context;
        protected BaseHandler(PetSocialNetworkDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken); 
    }
}
