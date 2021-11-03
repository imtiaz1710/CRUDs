using AutoMapper;
using SocialNetwork.ProfileManager.BusinessObjects;
using SocialNetwork.ProfileManager.Exceptions;
using SocialNetwork.ProfileManager.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetwork.ProfileManager.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IProfileUnitOfWork _profileUnitOfWork;
        private readonly IMapper _mapper;

        public PhotoService(IProfileUnitOfWork profileUnitOfWork, IMapper mapper)
        {
            _profileUnitOfWork = profileUnitOfWork;
            _mapper = mapper;
        }

        public IList<Photo> GetAllPhotos()
        {
            var photoEntites = _profileUnitOfWork.Photos.GetAll();
            var photos = new List<Photo>();

            foreach (var entity in photoEntites)
            {
                photos.Add(_mapper.Map<Photo>(entity));
            }

            return photos;
        }

        public void CreatePhoto(Photo photo)
        {
            if (photo == null)
                throw new InvalidParameterException("Photo Was not Provided");

            _profileUnitOfWork.Photos.Add(
                _mapper.Map<Entities.Photo>(photo));

            _profileUnitOfWork.Save();
        }

        public (IList<Photo> records, int total, int totalDisplay) GetPhotos(int pageIndex, int pageSize,
            string searchText, string sortText)
        {
            var photoData = _profileUnitOfWork.Photos.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? null : x => x.MemberId.ToString().Contains(searchText),
                sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from photo in photoData.data
                              select _mapper.Map<Photo>(photo)).ToList();

            return (resultData, photoData.total, photoData.totalDisplay);
        }

        public Photo GetPhoto(int id)
        {
            var photo = _profileUnitOfWork.Photos.GetById(id);

            if (photo == null) return null;

            return _mapper.Map<Photo>(photo);
        }

        public void UpdatePhoto(Photo photo)
        {
            if (photo == null)
                throw new InvalidOperationException("Photo is missing");

            var photoEntity = _profileUnitOfWork.Photos.GetById(photo.Id);

            if (photoEntity != null)
            {
                _mapper.Map(photo, photoEntity);

                _profileUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Couldn't find photo");
        }

        public void DeletePhoto(int id)
        {
            _profileUnitOfWork.Photos.Remove(id);
            _profileUnitOfWork.Save();
        }
    }
}
