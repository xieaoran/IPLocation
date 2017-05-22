using System.ComponentModel;
using System.Runtime.CompilerServices;
using IPLocation.Annotations;

namespace IPLocation.Models
{
    public class AuthToken
    {
        public int UserId { get; set; }
        public string LicenseKey { get; set; }
    }
}
