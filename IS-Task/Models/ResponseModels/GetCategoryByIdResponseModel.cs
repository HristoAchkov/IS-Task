using IS_Task.Shared.Enums;

namespace IS_Task.Models.ResponseModels
{
    public class GetCategoryByIdResponseModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public CategoryStatus Status { get; set; }
    }
}
