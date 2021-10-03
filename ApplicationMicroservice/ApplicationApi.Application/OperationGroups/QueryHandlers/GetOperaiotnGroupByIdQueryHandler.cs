using System.Threading;
using System.Threading.Tasks;
using ApplicationApi.Application.OperationGroups.Queries;
using ApplicationApi.Persistence;
using ApplicationApi.ViewModels.OperationGroups;

namespace ApplicationApi.Application.OperationGroups.QueryHandlers
{
    public class GetOperaiotnGroupByIdQueryHandler :
        Framework.Mediator.IRequestHandler<GetOperaiotnGroupByIdQuery, OperationGroupViewModel>
    {
        public GetOperaiotnGroupByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork { get; }

        public async Task<Framework.Result<OperationGroupViewModel>> Handle
            (GetOperaiotnGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var result =
                new Framework.Result<OperationGroupViewModel>();

            if (request.Id.HasValue == false)
            {
                result.WithError(errorMessage: Resources.Messages.Errors.BadRequest);

                return result;
            }

            var foundedOperationGroup =
                await UnitOfWork.OperationGroupRepository
                .GetByIdAsync(id: request.Id.Value);

            if (foundedOperationGroup == null)
            {
                var errorMessage = string.Format
                    (Resources.Messages.Validations.NotFound,
                    Resources.DataDictionary.OperationGroup);

                result.WithError(errorMessage: errorMessage);

                return result;
            }

            result.WithValue(value: foundedOperationGroup);

            return result;
        }
    }
}
