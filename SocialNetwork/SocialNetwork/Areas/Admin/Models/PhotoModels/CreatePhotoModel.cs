using Autofac;
using Microsoft.AspNetCore.Http;
using SocialNetwork.ProfileManager.BusinessObjects;
using SocialNetwork.ProfileManager.Services;

namespace SocialNetwork.Areas.Admin.Models.PhotoModels
{
    public class CreatePhotoModel
    {
        public int MemberId { get; set; }
        public IFormFile PhotoFile { get; set; }
        public string UniquePhotoFileName { private get; set; }

        private readonly IPhotoService _photoService;

        public CreatePhotoModel()
        {
            _photoService = Startup.AutofacContainer.Resolve<IPhotoService>();
        }

        public CreatePhotoModel(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        internal void LoadUniquePhotoFileName()
        {
            var photoFileOperationModel = new PhotoFileOperationModel();
            UniquePhotoFileName = photoFileOperationModel.GetUniqueFileName(PhotoFile);
        }

        internal void CreatePhoto()
        {
            var photo = new Photo
            {
                MemberId = MemberId,
                PhotoFileName = UniquePhotoFileName
            };

            _photoService.CreatePhoto(photo);
        }

        internal void UploadPhoto()
        {
            var photoFileOperationModel = new PhotoFileOperationModel();
            photoFileOperationModel.Upload(PhotoFile, UniquePhotoFileName);
        }
    }
}
