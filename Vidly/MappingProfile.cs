using AutoMapper;
using Vidly.Domain;
using Vidly.Dtos;

namespace Vidly
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>().ForMember(
                dest => dest.Id,
                opt => opt.Ignore());

            CreateMap<Movie, MovieDto>();
            CreateMap<MovieDto, Movie>()
                .ForMember(
                    dest=> dest.Id, 
                    opt => opt.Ignore());

        }
    }
}
