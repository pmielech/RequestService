namespace RequestService.Models.GymMembershipType;

public class GymMembershipReadOnlyVm
{
    public uint Id { get; set; }
    public string Name { get; set; }

    public decimal PricePerMonth { get; set; }
    public string Date { get; set; }
    public decimal TotalPrice { get; set; }

    public decimal Span { get; set; }

}