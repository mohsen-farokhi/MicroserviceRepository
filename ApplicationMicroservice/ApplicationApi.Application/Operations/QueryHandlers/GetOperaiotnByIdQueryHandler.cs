using System.Threading;
using System.Threading.Tasks;
using ApplicationApi.Application.Operations.Queries;
using ApplicationApi.Persistence;
using ApplicationApi.ViewModels.Operations;

namespace ApplicationApi.Application.Operations.QueryHandlers
{
    public class GetOperaiotnByIdQueryHandler :
        Framework.Mediator.IRequestHandler<GetOperaiotnByIdQuery, OperationViewModel>
    {
        public GetOperaiotnByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; }

        public async Task<Framework.Result<OperationViewModel>> Handle
            (GetOperaiotnByIdQuery request, CancellationToken cancellationToken)
        {
            var result =
                new Framework.Result<OperationViewModel>();

            if (request.Id.HasValue == false)
            {
                result.WithError(errorMessage: Resources.Messages.Errors.BadRequest);

                return result;
            }

            // **************************************************
            var foundedOperation =
                await UnitOfWork.OperationRepository.GetByIdAsync
                (id: request.Id.Value);

            if (foundedOperation == null)
            {
                var errorMessage = string.Format
                    (Resources.Messages.Validations.NotFound,
                    Resources.DataDictionary.Operation);

                result.WithError(errorMessage: errorMessage);

                return result;
            }
            // **************************************************

            result.WithValue(value: foundedOperation);

            return result;

        }
    }
}
