using AutoMapper;
using Domain.Entities;

namespace Presentation.ViewModels.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TaskVM, TaskItem>();
            CreateMap<TaskItem, TaskVM>().ForMember(src => src.IsExpired, 
                opt => opt.MapFrom((src, dest) => DateTime.Now > src.Deadline));     

            CreateMap<Solution, SolutionVM>();
            CreateMap<SolutionVM, Solution>();

            CreateMap<User, UserVM>();
            CreateMap<UserVM, User>();
        }
    }
}
