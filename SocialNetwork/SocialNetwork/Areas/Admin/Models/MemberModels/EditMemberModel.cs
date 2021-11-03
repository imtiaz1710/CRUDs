using Autofac;
using SocialNetwork.ProfileManager.BusinessObjects;
using SocialNetwork.ProfileManager.Services;
using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Areas.Admin.Models.MemberModels
{
    public class EditMemberModel
    {
        public int? Id { get; set; }
        [Required, MaxLength(200, ErrorMessage = "Name should be less than 100 characters")]
        public string Name { get; set; }
        [Required, Range(typeof(DateTime), "1/1/1900", "12/12/2025")]
        public DateTime? DateOfBirth { get; set; }
        [MaxLength(200, ErrorMessage = "Address should be less than 200 characters")]
        public string Address { get; set; }

        private readonly IMemberService _memberService;

        public EditMemberModel()
        {
            _memberService = Startup.AutofacContainer.Resolve<IMemberService>();
        }

        public void LoadModelData(int id)
        {
            var member = _memberService.GetMember(id);
            Id = member?.Id;
            Name = member?.Name;
            DateOfBirth = member?.DateOfBirth;
            Address = member?.Address;
        }

        internal void Update()
        {
            var member = new Member
            {
                Id = Id.HasValue ? Id.Value : 0,
                Name = Name,
                DateOfBirth = DateOfBirth.HasValue ? DateOfBirth.Value : DateTime.MinValue,
                Address = Address
            };

            _memberService.UpdateMember(member);
        }

    }
}
