using SocialNetwork.ProfileManager.BusinessObjects;
using System.Collections.Generic;

namespace SocialNetwork.ProfileManager.Services
{
    public interface IMemberService
    {
        void CreateMember(Member member);
        void DeleteMember(int id);
        IList<Member> GetAllMembers();
        Member GetMember(int id);
        (IList<Member> records, int total, int totalDisplay) GetMembers(
            int pageIndex, int pageSize, string searchText, string sortText);
        void UpdateMember(Member member);
    }
}