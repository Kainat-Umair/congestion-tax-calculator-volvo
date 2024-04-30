namespace CongestionTaxCalculator.WebAPI.Entities
{
    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public bool IsSingleChargeRuleCity { get; set; }
    }
}
