using AutoMapper;
using SocialNetwork.ProfileManager.BusinessObjects;
using SocialNetwork.ProfileManager.Exceptions;
using SocialNetwork.ProfileManager.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetwork.ProfileManager.Services
{
    public class MemberService : IMemberService
    {
        private readonly IProfileUnitOfWork _profileUnitOfWork;
        private readonly IMapper _mapper;

        public MemberService(IProfileUnitOfWork profileUnitOfWork, IMapper mapper)
        {
            _profileUnitOfWork = profileUnitOfWork;
            _mapper = mapper;
        }

        public IList<Member> GetAllMembers()
        {
            var memberEntites = _profileUnitOfWork.Members.GetAll();
            var members = new List<Member>();

            foreach (var entity in memberEntites)
            {
                members.Add(_mapper.Map<Member>(entity));
            }

            return members;
        }

        public void CreateMember(Member member)
        {
            if (member == null)
                throw new InvalidParameterException("Member Was not Provided");

            if (IsUserNameAlreadyUsed(member.Name))
                throw new DuplicateValueException("User name already exists");

            _profileUnitOfWork.Members.Add(
                _mapper.Map<Entities.Member>(member));

            _profileUnitOfWork.Save();
        }

        public (IList<Member> records, int total, int totalDisplay) GetMembers(int pageIndex, int pageSize,
            string searchText, string sortText)
        {
            var memberData = _profileUnitOfWork.Members.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? null : x => x.Name.Contains(searchText),
                sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from member in memberData.data
                              select _mapper.Map<Member>(member)).ToList();

            return (resultData, memberData.total, memberData.totalDisplay);
        }

        public Member GetMember(int id)
        {
            var member = _profileUnitOfWork.Members.GetById(id);

            if (member == null) return null;

            return _mapper.Map<Member>(member);
        }

        public void UpdateMember(Member member)
        {
            if (member == null)
                throw new InvalidOperationException("Member is missing");

            if (IsUserNameAlreadyUsed(member.Name, member.Id))
                throw new DuplicateValueException("Member Name already used in other member.");

            var memberEntity = _profileUnitOfWork.Members.GetById(member.Id);

            if (memberEntity != null)
            {
                _mapper.Map(member, memberEntity);

                _profileUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Couldn't find member");
        }

        public void DeleteMember(int id)
        {
            _profileUnitOfWork.Members.Remove(id);
            _profileUnitOfWork.Save();
        }

        private bool IsUserNameAlreadyUsed(string name) =>
            _profileUnitOfWork.Members.GetCount(x => x.Name == name) > 0;

        private bool IsUserNameAlreadyUsed(string name, int id) =>
            _profileUnitOfWork.Members.GetCount(x => x.Name == name && x.Id != id) > 0;
    }
}

