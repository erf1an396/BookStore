using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsActive { get; set; }

        public string BirthDay_Date {  get; set; }

        public GenderEnum Gender { get; set; }

        public bool Deleted { get; set; }

        public bool IsAdmin { get; set; }

        public virtual ICollection<ApplicationUserToken> ApplicationUserTokens { get; set; }
        public virtual ICollection<ApplicationUserLogin> ApplicationUserLogins { get; set; }

        public virtual ICollection<ApplicationUserClaim> ApplicationUserClaims { get; set; }

        public virtual ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }

        public ICollection<UserPhoto> UserPhoto {  get; set; }


    }
}
