namespace BloodNetworkMVC_Oct2024.Helpers
{
    public static class BloodInventoryHelper
    {
        public static string GetExpirationStatusClass(DateTime expirationDate)
        {
            var daysUntilExpiration = (expirationDate - DateTime.Now).Days;

            return daysUntilExpiration switch
            {
                <= 0 => "table-danger",
                <= 7 => "table-warning",
                <= 14 => "table-info",
                _ => ""
            };
        }

        public static string GetStorageUtilizationClass(double utilizationPercentage)
        {
            return utilizationPercentage switch
            {
                >= 90 => "bg-danger",
                >= 75 => "bg-warning",
                >= 50 => "bg-info",
                _ => "bg-success"
            };
        }

    }
}
