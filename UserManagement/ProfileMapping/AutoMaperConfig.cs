using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace UserManagement.ProfileMapping
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile)
            => profile.CreateMap(typeof(T), GetType()).ReverseMap();
    }
    public static class AutoMaperConfig
    {
        public static IServiceCollection AddAutomapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}
