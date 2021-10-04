using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using d4.Core.Kernel.Interfaces;
using d4.Sample.Domain.Employees;

namespace d4.Sample.Infrastructure.JsonFiles
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _basePath;
        private readonly JsonFilesUnitOfWork _unitOfWork;

        public EmployeeRepository(string basePath)
        {
            _basePath = basePath;
            _unitOfWork = new JsonFilesUnitOfWork();
        }

        public IUnitOfWork UnitOfWork => _unitOfWork;

        public async Task<Employee> GetByIdAsync(Trigram id)
        {
            string fileName =  GetEmployeeFileName(id);

            string content = _unitOfWork.Contains(fileName)
                ? _unitOfWork.GetFileContent(fileName)
                : await File.ReadAllTextAsync(fileName);
            
            return JsonSerializer.Deserialize<Employee>(content);
        }

        public Task<Employee> CreateAsync(Employee entity)
        {
            string fileName = GetEmployeeFileName(entity.Id);
            _unitOfWork.AddFile(fileName,JsonSerializer.Serialize<Employee>(entity));
            return Task.FromResult(entity);
        }

        public async Task UpdateAsync(Employee entity)
        {
            await CreateAsync(entity);
        }

        public Task DeleteAsync(Employee entity)
        {
            _unitOfWork.AddFile(GetEmployeeFileName(entity.Id),null);
            return Task.CompletedTask;
        }

        private string GetEmployeeFileName(Trigram id)
        {
            return Path.Combine(_basePath, id.Value, ".json");
        }
    }
}