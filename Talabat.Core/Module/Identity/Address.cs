using System.Text.Json.Serialization;

namespace Talabat.Core.Module.Identity
{
	public class Address : BaseEntity
	{
        public string FirstName { get; set; } 
        public string LastName { get; set; }
		public string Street { get; set; } 
        public string City { get; set; } 
		public string Country { get; set; }
		public string ApplicationUserId { get; set; } 
        public ApplicationUser User { get; set; }
			
    }
}