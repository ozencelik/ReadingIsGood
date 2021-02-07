using AutoMapper;
using Data.Dtos.BookDtos;
using Data.Dtos.CustomerDtos;
using Data.Dtos.OrderDtos;
using Data.Entities.Books;
using Data.Entities.Customers;
using Data.Entities.Orders;

namespace Core.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Book
            CreateMap<Book, GetBookDto>();
            #endregion

            #region Customer
            CreateMap<Customer, GetCustomerDto>();

            CreateMap<CreateCustomerDto, Customer>();
            #endregion

            #region Order
            CreateMap<Order, GetOrderDto>();
            CreateMap<OrderProduct, GetOrderProductDto>();

            CreateMap<CreateOrderDto, Order>();
            CreateMap<CreateOrderProductDto, OrderProduct>();
            #endregion
        }
    }
}
