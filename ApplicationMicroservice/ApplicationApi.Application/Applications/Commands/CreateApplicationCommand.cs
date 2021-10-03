namespace ApplicationApi.Application.Applications.Commands
{
    public class CreateApplicationCommand :
        ViewModels.Applications.CreateApplicationViewModel, Framework.Mediator.IRequest<int>
    {
    }
}
