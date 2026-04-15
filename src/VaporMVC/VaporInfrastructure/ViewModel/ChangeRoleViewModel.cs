using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace VaporInfrastructure.ViewModel
{
    public class ChangeRoleViewModel
    {
        public required string UserId { get; set; }
        public required string UserEmail { get; set; }
        public List<IdentityRole<int>> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
        public ChangeRoleViewModel()
        {
            AllRoles = new List<IdentityRole<int>>();
            UserRoles = new List<string>();
        }
    }
}