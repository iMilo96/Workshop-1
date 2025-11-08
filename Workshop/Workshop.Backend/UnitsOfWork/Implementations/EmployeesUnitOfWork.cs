using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using Workshop.Backend.Repositories.Interfaces;
using Workshop.Backend.UnitsOfWork.Interfaces;
using Workshop.Shared.DTOs;
using Workshop.Shared.Entities;
using Workshop.Shared.Responses;

namespace Workshop.Backend.UnitsOfWork.Implementations;

public class EmployeesUnitOfWork : GenericUnitOfWork<Employee>, IEmployeesUnitOfWork
{
    private readonly IEmployeesRepository _employeesRepository;

    public EmployeesUnitOfWork(IGenericRepository<Employee> repository, IEmployeesRepository
        employeesRepository) : base(repository)
    {
        _employeesRepository = employeesRepository;
    }

    public override async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination) => await _employeesRepository.GetTotalRecordsAsync(pagination);

    public override async Task<ActionResponse<IEnumerable<Employee>>> GetAsync(PaginationDTO pagination) =>
        await _employeesRepository.GetAsync(pagination);

    public override async Task<ActionResponse<IEnumerable<Employee>>> GetAsync() => await
        _employeesRepository.GetAsync();

    public override async Task<ActionResponse<Employee>> GetAsync(int id) => await
        _employeesRepository.GetAsync(id);

    public async Task<IEnumerable<Employee>> GetComboAsync() => await _employeesRepository.GetComboAsync();
}