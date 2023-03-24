using FinalXamarin.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FinalXamarin.Repository
{
    public class ApiRepository
    {
        readonly SQLiteAsyncConnection database;

        public ApiRepository(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            
            database.CreateTableAsync<PersonaModel>().Wait();
           
        }
        
        public Task<List<PersonaModel>> GetPersonasAsync()
        {
            return database.Table<PersonaModel>().ToListAsync();
        }


        public Task<PersonaModel> GetPersonasAsync(int id)
        {
            return database.Table<PersonaModel>()
                .Where(i => i.ID == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SavePersonasAsync(PersonaModel good)
        {
            if (good.ID != 0)
            {
                return database.UpdateAsync(good);
            }
            else
            {
                return database.InsertAsync(good);
            }
        }

        public Task<int> DeletePersonasAsync(PersonaModel good)
        {
            return database.DeleteAsync(good);
        }
    }
}
