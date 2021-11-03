using Autofac;
using SocialNetwork.Models;
using SocialNetwork.ProfileManager.Services;
using System.Linq;

namespace SocialNetwork.Areas.Admin.Models.PhotoModels
{
    public class PhotoListModel
    {
        private IPhotoService _photoService;

        public PhotoListModel()
        {
            _photoService = Startup.AutofacContainer.Resolve<IPhotoService>();
        }

        public PhotoListModel(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        internal object GetPhotos(DataTablesAjaxRequestModel tableModel)
        {
            var data = _photoService.GetPhotos(
                tableModel.PageIndex,
                tableModel.PageSize,
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "MemberId", "PhotoFileName" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                            record.MemberId.ToString(),
                            record.PhotoFileName,
                            record.Id.ToString()
                        }
                    ).ToArray()
            };
        }

        internal void Delete(int id)
        {
            _photoService.DeletePhoto(id);
        }
    }
}
