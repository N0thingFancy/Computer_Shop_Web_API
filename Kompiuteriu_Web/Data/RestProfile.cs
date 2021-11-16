using AutoMapper;
using Kompiuteriu_Web.Data.Dtos.Auth;
using Kompiuteriu_Web.Data.Dtos.Computers;
using Kompiuteriu_Web.Data.Dtos.Shops;
using Kompiuteriu_Web.Data.Dtos.Workers;
using Kompiuteriu_Web.Data.Entities;

namespace Kompiuteriu_Web.Data
{
    public class RestProfile : Profile
    {
        public RestProfile()
        {
            //Computers
            CreateMap<Computer, ComputerDto>();
            CreateMap<CreateComputerDto, Computer>();
            CreateMap<UpdateComputerDto, Computer>();

            //Shops
            CreateMap<Shop, ShopDto>();
            CreateMap<CreateShopDto, Shop>();
            CreateMap<UpdateShopDto, Shop>();

            //Workers
            CreateMap<Worker, WorkerDto>();
            CreateMap<CreateWorkerDto, Worker>();
            CreateMap<UpdateWorkerDto, Worker>();

            //Users
            CreateMap<RestUser, UserDto>();
        }
    }
}
