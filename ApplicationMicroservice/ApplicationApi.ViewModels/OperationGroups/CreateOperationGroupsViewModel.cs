namespace ApplicationApi.ViewModels.OperationGroups
{
    public class CreateOperationGroupsViewModel
    {
        public int? ApplicationId { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}
