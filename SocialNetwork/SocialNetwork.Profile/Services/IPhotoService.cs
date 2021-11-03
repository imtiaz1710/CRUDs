using SocialNetwork.ProfileManager.BusinessObjects;
using System.Collections.Generic;

namespace SocialNetwork.ProfileManager.Services
{
    public interface IPhotoService
    {
        void CreatePhoto(Photo photo);
        void DeletePhoto(int id);
        IList<Photo> GetAllPhotos();
        Photo GetPhoto(int id);
        (IList<Photo> records, int total, int totalDisplay) GetPhotos(
            int pageIndex, int pageSize, string searchText, string sortText);
        void UpdatePhoto(Photo photo);
    }
}