using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;

public class DatabaseHelper
{
    private readonly SQLiteAsyncConnection _database;

    public DatabaseHelper()
    {
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "MyDatabase.db3");
        _database = new SQLiteAsyncConnection(dbPath);
        // InitializeDatabaseAsync().Wait(); 
         // Initialize the DB table when this class is instantiated
        Task.Run(async () => await InitializeDatabaseAsync()).Wait();
    }

    public async Task InitializeDatabaseAsync()
    {
        await _database.CreateTableAsync<User>();
    }

    public async Task<int> AddUserAsync(User user)
    {
        int result = await _database.InsertAsync(user);
        return result;
    }

    public async Task<List<User>> GetUsersAsync()
    {
        var users = await _database.Table<User>().ToListAsync();
        return users;
    }
      public async Task<int> UpdateUserAsync(User user)
    {
        return await _database.UpdateAsync(user);
    }

    public async Task<int> DeleteUserAsync(User user)
    {
        return await _database.DeleteAsync(user);
    }
}
