public interface IAddSolutionUseCase
{
    Task<string?> AddSolution(int taskId, string? userId, string? URL);
}
