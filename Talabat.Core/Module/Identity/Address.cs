using System.Text.Json.Serialization;

namespace Talabat.Core.Module.Identity
{
	public class Address : BaseEntity
	{
        public string FName { get; set; } 
        public string LName { get; set; }
		public string street { get; set; } 
        public string city { get; set; } 
		public string country { get; set; }
        public ApplicationUser User { get; set; }
		public string ApplicationUserId { get; set; } 
			
    }
}