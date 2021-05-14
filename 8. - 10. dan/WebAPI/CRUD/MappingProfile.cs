using AutoMapper;
using Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static CRUD.Controllers.CityController;
using static CRUD.Controllers.PlayersController;

namespace CRUD
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ICity, CityREST>();
            CreateMap<CityREST, ICity>();
            CreateMap<PlayerREST, IPlayer>();
            CreateMap<IPlayer, PlayerREST>();
        }
    }
}