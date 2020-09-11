using AutoMapper;
using Vidly.Domain;
using Vidly.Dtos;

namespace Vidly
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to DTO
            CreateMap<Customer, CustomerDto>();
            CreateMap<Movie, MovieDto>();
            CreateMap<MembershipType, MembershipTypeDto>();
            CreateMap<Genre, GenreDto>();


            // DTO to Domain
            CreateMap<CustomerDto, Customer>().ForMember(
                dest => dest.Id,
                opt => opt.Ignore());
            CreateMap<MovieDto, Movie>()
                .ForMember(
                    dest=> dest.Id, 
                    opt => opt.Ignore());

        }
    }
}
