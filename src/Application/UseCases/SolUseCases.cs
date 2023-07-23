using Application.Services;
using Application.UseCases.Common;
using Application.UseCases.Solutions;
using Application.UseCases.Users;
using Domain.Entities;

namespace Application.UseCases
{
    public class SolUseCases
    {
        public readonly DeleteUseCase<Solution, int> Delete;
        public readonly GetAllUseCase<Solution, int> GetAll;
        public readonly GetByIdUseCase<Solution, int> GetById;
        public readonly UpdateUseCase<Solution, int> Update;
        public readonly GetUserSolsUseCase GetUserSols;
        public readonly AddSolutionUseCase Add;
        public readonly DeclineSolUseCase Decline;
        public readonly AcceptSolUseCase Accept;

        public SolUseCases(IEntityBaseRep<int, Solution> entityBaseRep, 
            IUserRep userRep, ISolutionRep solRep, ITaskItemRep taskItemRep)
        {
            Delete = new(entityBaseRep);
            GetAll = new(entityBaseRep);
            GetById = new(entityBaseRep);
            GetUserSols = new(userRep, solRep);
            Add = new(userRep, taskItemRep, solRep);
            Decline = new(userRep, solRep);
            Accept = new(userRep, solRep);
            Update = new(entityBaseRep);
        }
    }
}
