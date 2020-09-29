using System.Runtime.Serialization;

namespace Sales.EntityFrameworkCore.PaymentProviders.Mobbex.Models
{
    public class SubscriptionOptionsDto
    {
        [DataMember(Name = "theme")]
        public SubscriptionThemeDto Theme { get; set; } = new SubscriptionThemeDto();
    }

    public class SubscriptionThemeDto
    {
        [DataMember(Name = "header")]
        public SubscriptionThemeHeaderDto Header { get; set; } = new SubscriptionThemeHeaderDto();

        [DataMember(Name = "colors")]
        public SubscriptionThemeColorsDto Colors { get; set; } = new SubscriptionThemeColorsDto();
    }

    public class SubscriptionThemeColorsDto
    {
        [DataMember(Name = "primary")]
        public string Primary => "#FF0000";
    }

    public class SubscriptionThemeHeaderDto
    {
        [DataMember(Name = "name")]
        public string Name => "Verónica Lercari";

        [DataMember(Name = "logo")]
        public string Logo => "https://res.mobbex.com/images/icons/def_store.png";
    }
}
