using AutoMapper;
using eCommerce.OrderMicroservice.BusinessLogicLayer.DTO;
using eCommerce.OrderMicroservice.DataAccessLayer.Entity;

namespace eCommerce.OrderMicroservice.Businesslogiclayer.Mappers;

public class Mappers : Profile
{
    public Mappers()
    {
        CreateMap<Order, OrderAddRequest>().ReverseMap();
        CreateMap<Order,OrderResponse>().ReverseMap();
        CreateMap<Order,OrderUpdateRequest>().ReverseMap();
        CreateMap<OrderItem,OrderItemAddRequest>().ReverseMap();
        CreateMap<OrderItem,OrderItemResponse>().ReverseMap();
        CreateMap<OrderItem, OrderItemUpdateRequest>().ReverseMap();
    }
}
