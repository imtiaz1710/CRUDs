using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data;
using SocialNetwork.ProfileManager.Contexts;
using SocialNetwork.ProfileManager.Repositories;

namespace SocialNetwork.ProfileManager.UnitOfWorks
{
    public class ProfileUnitOfWork : UnitOfWork, IProfileUnitOfWork
    {
        public IMemberRepository Members { get; private set; }
        public IPhotoRepository Photos { get; private set; }

        public ProfileUnitOfWork(IMemberRepository members, 
            IPhotoRepository photos, IProfileContext context)
            : base((DbContext)context)
        {
            Members = members;
            Photos = photos;
        }
    }
}
