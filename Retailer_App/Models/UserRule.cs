// File UserRule.cs
namespace Retailer_App.Models
{
    public class UserRule
    {
        public string Uid { get; set; }
        public User User { get; set; } = new User();
        public string Menu { get; set; }
        public string Access { get; set; }
    }
}
