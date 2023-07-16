using AutoMapper;
using Domain.Entites;

namespace Presentation.ViewModels.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TaskItem, TaskVM>();
            CreateMap<TaskVM, TaskItem>();

            CreateMap<Solution, SolutionVM>();
            CreateMap<SolutionVM, Solution>();

            CreateMap<User, UserVM>();
            CreateMap<UserVM, User>();
        }
    }
}
