using Autofac;
using Microsoft.AspNetCore.Http;
using SocialNetwork.ProfileManager.BusinessObjects;
using SocialNetwork.ProfileManager.Services;

namespace SocialNetwork.Areas.Admin.Models.PhotoModels
{
    public class EditPhotoModel
    {
        public int? Id { get; set; }
        public int? MemberId { get; set; }
        public IFormFile PhotoFile { get; set; }

        private readonly IPhotoService _photoService;

        public EditPhotoModel()
        {
            _photoService = Startup.AutofacContainer.Resolve<IPhotoService>();
        }

        public void LoadModelData(int id)
        {
            var photo = _photoService.GetPhoto(id);
            var photoFileOperation = new PhotoFileOperationModel();

            Id = photo?.Id;
            MemberId = photo?.MemberId;

            if(photo.PhotoFileName != null)
                PhotoFile = photoFileOperation.ConverToPhotoFile(photo.PhotoFileName);
        }

        internal void Update()
        {
            var photoFileOperation = new PhotoFileOperationModel();
            var PhotoName = photoFileOperation.GetUniqueFileName(PhotoFile);
            var photo = new Photo
            {
                Id = Id.HasValue ? Id.Value : 0,
                MemberId = MemberId.HasValue ? MemberId.Value : 0,
                PhotoFileName = PhotoName
            };

            photoFileOperation.Upload(PhotoFile, PhotoName);
            _photoService.UpdatePhoto(photo);
        }
    }
}
