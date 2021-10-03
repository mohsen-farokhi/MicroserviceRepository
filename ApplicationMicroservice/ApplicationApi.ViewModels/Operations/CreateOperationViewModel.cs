namespace ApplicationApi.ViewModels.Operations
{
    public class CreateOperationViewModel
    {
        public int? ApplicationId { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public bool IsActive { get; set; }

        public int? AccessType { get; set; }
    }
}
