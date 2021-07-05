using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using System;
using System.Collections.Generic;
using System.Text;
using DAL.models;

namespace DataObject
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Telephonist, TelephonistDTO>();
            CreateMap<TelephonistDTO, Telephonist>();

            CreateMap<TelephonistInCompanies, TelephonistCompaniesDTO>();
            CreateMap<TelephonistCompaniesDTO, TelephonistInCompanies>();

            CreateMap<Calls, CallsDTO>();
            CreateMap<CallsDTO, Calls>();

            CreateMap<Contribution, ContributionDTO>();
            CreateMap<ContributionDTO, Contribution>();

            CreateMap<Companies, CompaniesDTO>();
            CreateMap<CompaniesDTO, Companies>();

            CreateMap<PhoneNumbers, Phone_numbersDTO>();
            CreateMap<Phone_numbersDTO, PhoneNumbers>();


        }
    }
}
