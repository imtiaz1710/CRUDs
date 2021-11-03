using Autofac;
using SocialNetwork.ProfileManager.BusinessObjects;
using SocialNetwork.ProfileManager.Services;
using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Areas.Admin.Models.MemberModels
{
    public class CreateMemberModel
    {
        [Required, MaxLength(200, ErrorMessage = "Name should be less than 100 characters")]
        public string Name { get; set; }
        [Required, Range(typeof(DateTime), "1/1/1900", "12/12/2025")]
        public DateTime DateOfBirth { get; set; }
        [MaxLength(200, ErrorMessage = "Address should be less than 200 characters")]
        public string Address { get; set; }

        private readonly IMemberService _memberService;

        public CreateMemberModel()
        {
            _memberService = Startup.AutofacContainer.Resolve<IMemberService>();
        }

        public CreateMemberModel(IMemberService memberService)
        {
            _memberService = memberService;
        }

        internal void CreateMember()
        {
            var member = new Member
            {
                Name = Name,
                DateOfBirth = DateOfBirth.Date,
                Address = Address
            };

            _memberService.CreateMember(member);
        }
    }
}
