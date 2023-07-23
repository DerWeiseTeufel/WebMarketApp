using Application.Services;
using Application.UseCases.Common;
using Application.UseCases.Users;
using Domain.Entities;

namespace Application.UseCases
{
    public class UserUseCases
    {
        public readonly DeleteUseCase<User, string> Delete;
        public readonly GetAllUseCase<User, string> GetAll;
        public readonly GetByIdUseCase<User, string> GetById;
        public readonly UpdateUseCase<User, string> Update;
        public readonly GetByEmailUseCase GetByEmail;

        public UserUseCases(IEntityBaseRep<string, User> entityBaseRep, IUserRep userRep)
        {
            Delete = new(entityBaseRep);
            GetAll = new(entityBaseRep);
            GetById = new(entityBaseRep);
            GetByEmail = new(userRep);
            Update = new(entityBaseRep);
        }
    }
}
