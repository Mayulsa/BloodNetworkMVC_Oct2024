namespace BloodNetworkMVC_Oct2024.Models.Enums
{
    public enum BloodUnitStatus
    {
        Available,      // Ready for use
        Reserved,       // Reserved for a specific patient/procedure
        InTransit,      // Being moved between locations
        Quarantined,    // Under testing or quality check
        Used,          // Has been used
        Expired,       // Past expiration date
        Discarded      // Damaged or failed quality checks
    }
}
