using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data;
using SocialNetwork.ProfileManager.Contexts;
using SocialNetwork.ProfileManager.Entities;

namespace SocialNetwork.ProfileManager.Repositories
{
    public class MemberRepository : Repository<Member, int>, IMemberRepository
    {
        public MemberRepository(IProfileContext context) : 
            base((DbContext)context)
        {

        }
    }
}
