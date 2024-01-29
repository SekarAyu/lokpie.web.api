using AutoMapper;
using Lokpie.Common.Dtos;
using Lokpie.Repository.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Lokpie.Service
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Reservation, ReservationDto>()
                .ForMember(x => x.TableId, opt => opt.MapFrom(src => src.Table.Id))
                .ForMember(x => x.TableNumber, opt => opt.MapFrom(src => src.Table.Number))
                .ForMember(x => x.TableAreaName, opt => opt.MapFrom(src => src.Table.AreaName))
                .ReverseMap();
            CreateMap<Order, OrderDto>()
                .ForMember(x => x.TableNo, opt => opt.MapFrom(src => src.Table.Number))
                .ReverseMap();
            CreateMap<OrderDetail, OrderDetailDto>()
                .ForMember(x => x.MenuId, opt => opt.MapFrom(src => src.Menu.Id))
                .ForMember(x => x.MenuName, opt => opt.MapFrom(src => src.Menu.Name))
                .ReverseMap();
            CreateMap<TenantTable, TableDto>()
                .ForMember(x => x.TableReservationDate, opt => opt.MapFrom(src => JsonConvert.DeserializeObject<IList<string>>(src.TableReservationDate)))
                .ReverseMap();
            CreateMap<TenantTable, TableDetailDto>()
                .ForMember(x => x.TableReservationDate, opt => opt.MapFrom(src => JsonConvert.DeserializeObject<IList<string>>(src.TableReservationDate)))
                .ReverseMap();
            CreateMap<TenantTable, BasePhotoDto>()
                .ForMember(x => x.Photo, opt => opt.MapFrom(src => src.TablePhoto))
                .ForMember(x => x.FileName, opt => opt.MapFrom(src => src.TablePhotoFileName))
                .ReverseMap();
        }
    }
}
