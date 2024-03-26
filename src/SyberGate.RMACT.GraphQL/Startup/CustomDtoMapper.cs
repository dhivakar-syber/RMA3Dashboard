using AutoMapper;
using SyberGate.RMACT.Authorization.Users;
using SyberGate.RMACT.Dto;

namespace SyberGate.RMACT.Startup
{
    public static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<User, UserDto>()
                .ForMember(dto => dto.Roles, options => options.Ignore())
                .ForMember(dto => dto.OrganizationUnits, options => options.Ignore());
        }
    }
}