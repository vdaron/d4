using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using d4.Core.Kernel.Interfaces;
using d4.Sample.Domain.Employees;

namespace d4.Sample.Infrastructure.JsonFiles
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _basePath;
        private readonly JsonFilesUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeRepository(IMapper mapper, Config config)
        {
            _mapper = mapper;
            _basePath = config.BasePath;
            Directory.CreateDirectory(_basePath);
            _unitOfWork = new JsonFilesUnitOfWork();
        }

        public IUnitOfWork UnitOfWork => _unitOfWork;

        public async Task<Employee?> GetByIdAsync(Trigram id)
        {
            string fileName =  GetEmployeeFileName(id);

            if (!File.Exists(fileName))
                return null;
            
            string content = _unitOfWork.Contains(fileName)
                ? _unitOfWork.GetFileContent(fileName)
                : await File.ReadAllTextAsync(fileName);
            
            return Deserialize(content);
        }

        public Task<Employee> CreateAsync(Employee entity)
        {
            string fileName = GetEmployeeFileName(entity.Id);
            _unitOfWork.AddFile(fileName,Serialize(entity));
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
            return Path.Combine(_basePath, id.Value + ".json");
        }

        private string Serialize(Employee e)
        {
            return JsonSerializer.Serialize(
                new EmployeeState(
                    e.Id.Value,
                    e.FirstName,
                    e.LastName,
                    e.BirthDate,
                    e.Address));
        }
        
        private Employee Deserialize(string jsonEmployee)
        {
            var e = JsonSerializer.Deserialize<EmployeeState>(jsonEmployee);
            return _mapper.Map<Employee>(e);
        }
    }
}