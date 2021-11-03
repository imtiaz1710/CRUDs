using SocialNetwork.Data;
using SocialNetwork.ProfileManager.Repositories;

namespace SocialNetwork.ProfileManager.UnitOfWorks
{
    public interface IProfileUnitOfWork : IUnitOfWork 
    {
        public IMemberRepository Members { get;}
        public IPhotoRepository Photos { get;}
    }
}
