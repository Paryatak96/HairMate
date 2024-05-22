using AutoMapper;
using HairMate.Application.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairMate.Application.ViewModels.Salon
{
    public class SalonForListVm : IMapFrom<HairMate.Domain.Model.Salon>
    {
        public int SalonId { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public string Type { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HairMate.Domain.Model.Salon, SalonForListVm>();
        }
    }
}
