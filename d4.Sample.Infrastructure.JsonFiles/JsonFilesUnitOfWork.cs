using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using d4.Core.Kernel.Interfaces;

namespace d4.Sample.Infrastructure.JsonFiles
{
    public class JsonFilesUnitOfWork : IUnitOfWork
    {
        private Dictionary<string, string> _files = new Dictionary<string, string>();

        public void AddFile(string fileName, string fileContent)
        {
            if (_files.ContainsKey(fileName))
            {
                _files[fileName] = fileContent;
            }
            else
            {
                _files.Add(fileName, fileContent);    
            }
        }

        public bool Contains(string fileName)
        {
            return _files.ContainsKey(fileName);
        }

        public string GetFileContent(string fileName)
        {
            if (!_files.ContainsKey(fileName))
                throw new ArgumentException("Filename not in UnitOfWork");
            
            return _files[fileName];
        }
        
        public async Task Commit()
        {
            foreach (var file in _files)
            {
                if (file.Value == null)
                {
                    File.Delete(file.Key);
                }
                else
                {
                    await File.WriteAllTextAsync(file.Key, file.Value);
                }
            }
        }
    }
}