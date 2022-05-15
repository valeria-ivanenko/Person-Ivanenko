using Desktop.Person_Ivanenko.Models;                              
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Desktop.Person_Ivanenko.Repositories
{
    class FileRepository
    {
        private static readonly string BaseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CSKMAStorage");

        public FileRepository()
        {
            if (!Directory.Exists(BaseFolder))
                Directory.CreateDirectory(BaseFolder);
        }

        public void CleanRepository()
        {
            DirectoryInfo di = new DirectoryInfo(BaseFolder);
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }

        }
        public async Task AddOrUpdateAsync(Person obj)
        {
            var stringObj = JsonSerializer.Serialize(obj);

            using (StreamWriter sw = new StreamWriter(Path.Combine(BaseFolder, obj.Email), false))
            {
                await sw.WriteAsync(stringObj);
            }
        }

        public async Task<Person> GetAsync(string login)
        {
            string stringObj = null;
            string filePath = Path.Combine(BaseFolder, login);

            if (!File.Exists(filePath))
                return null;

            using (StreamReader sw = new StreamReader(filePath))
            {
                stringObj = await sw.ReadToEndAsync();
            }

            return JsonSerializer.Deserialize<Person>(stringObj);
        }

        public async Task<List<Person>> GetAllAsync()
        {
            var res = new List<Person>();
            foreach (var file in Directory.EnumerateFiles(BaseFolder))
            {
                string stringObj = null;

                using (StreamReader sw = new StreamReader(file))
                {
                    stringObj = await sw.ReadToEndAsync();
                }

                res.Add(JsonSerializer.Deserialize<Person>(stringObj));
            }

            return res;
        }

        public List<Person> GetAll()
        {
            var res = new List<Person>();
            foreach (var file in Directory.EnumerateFiles(BaseFolder))
            {
                string stringObj = null;

                using (StreamReader sw = new StreamReader(file))
                {
                    stringObj = sw.ReadToEnd();
                }

                res.Add(JsonSerializer.Deserialize<Person>(stringObj));
            }

            return res;
        }
    }
}
