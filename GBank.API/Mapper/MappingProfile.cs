using AutoMapper;
using GBank.Application.ModelMapping;
using GBank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBank.API.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap < Bill,BillToFront>();
        }

        
    }
}
