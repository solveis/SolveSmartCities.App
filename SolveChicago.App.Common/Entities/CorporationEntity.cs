using SolveChicago.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveChicago.App.Common.Entities
{
    public class CorporationEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Province { get; set; }
        [Required]
        public string Country { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string Email { get; set; }
        
        public virtual IEnumerable<MemberCorporationEntity> MemberCorporations { get; set; }
        public virtual IEnumerable<UserProfileEntity> UserProfiles { get; set; }

        public CorporationEntity Map(Corporation model)
        {
            this.Id = model.Id;
            this.Name = model.Name;
            this.Phone = model.Phone;
            this.Address1 = model.Address1;
            this.Address2 = model.Address2;
            this.City = model.City;
            this.Province = model.Province;
            this.Country = model.Country;
            this.CreatedDate = model.CreatedDate;
            this.Email = model.Email;

            this.MemberCorporations = model.MemberCorporations.Select(x => new MemberCorporationEntity().Map(x));
            this.UserProfiles = model.UserProfiles.Select(x => new UserProfileEntity().Map(x));

            return this;
        }
    }
}
