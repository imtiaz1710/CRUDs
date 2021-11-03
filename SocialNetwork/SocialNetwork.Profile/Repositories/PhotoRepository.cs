using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data;
using SocialNetwork.ProfileManager.Contexts;
using SocialNetwork.ProfileManager.Entities;

namespace SocialNetwork.ProfileManager.Repositories
{
    class PhotoRepository : Repository<Photo, int>, IPhotoRepository
    {
        public PhotoRepository(IProfileContext context) :
            base((DbContext)context)
        {

        }
    }
}
