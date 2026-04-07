namespace IS_Task.Shared.Constants
{
    public static class DataConstants
    {
        public const string ProductNameLength = "Name length must be between 0 and 30.";
        public const string ProductDescriptionLength = "Description length must be between 0 and 100.";
        public const string PriceRange = "Price must be greater than 0.";

        public const string CategoryNameLength = "Name length must be between 0 and 20.";
        public const string CategoryDescriptionLength = "Description length must be between 0 and 100.";

        public const string AdminConstant = "Admin";

        public const string CartSessionKey = "CartToken";
    }
}
