namespace FastFood.Web.MappingConfiguration
{
    using AutoMapper;

    using Models;
    using ViewModels.Categories;
    using ViewModels.Employees;
    using ViewModels.Items;
    using ViewModels.Positions;
    using FastFood.Web.ViewModels.Orders;

    public class FastFoodProfile : Profile
    {
        public FastFoodProfile()
        {
            //Positions
            this.CreateMap<CreatePositionInputModel, Position>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.PositionName));

            this.CreateMap<Position, PositionsAllViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            //Employees
            this.CreateMap<RegisterEmployeeInputModel, Employee>();

            this.CreateMap<Employee, EmployeesAllViewModel>()
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position.Name));

            this.CreateMap<Position, RegisterEmployeeViewModel>()
                .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src => src.Name));

            //Categories
            this.CreateMap<CreateCategoryInputModel, Category>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CategoryName));

            this.CreateMap<Category, CategoryAllViewModel>();

            //Items
            this.CreateMap<CreateItemInputModel, Item>();

            this.CreateMap<Category, CreateItemViewModel>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Id));

            this.CreateMap<Item, ItemsAllViewModels>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));

            //Order
            this.CreateMap<CreateOrderInputModel, Order>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.OrderType));

            this.CreateMap<Order, OrderAllViewModel>()
                .ForMember(dest => dest.Employee, opt => opt.MapFrom(src => src.Employee.Name))
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DateTime, opt => opt.MapFrom(src => src.DateTime.ToString("dd/MM/yyy hh:mm:ss")));
        }
    }
}