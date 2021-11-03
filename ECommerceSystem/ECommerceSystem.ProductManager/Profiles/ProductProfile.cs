using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EO=ECommerceSystem.ProductManager.Entities;
using BO = ECommerceSystem.ProductManager.BusinessObjects;

namespace ECommerceSystem.ProductManager.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<EO.Product, BO.Product>().ReverseMap();
        }
    }
}
