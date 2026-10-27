using RahaHaldur.Models;
using SQLite;

namespace RahaHaldur.Services;

// Kõik SQLite tööd on siin: lisa, muuda, kustuta, loe.
public class DatabaseService
{
    private SQLiteAsyncConnection? _db;

    // Loob andmebaasi faili ja tabeli esimesel kasutamisel
    private async Task InitAsync()
    {
        if (_db != null)
            return;

        var path = Path.Combine(FileSystem.AppDataDirectory, "rahahaldur.db3");
        _db = new SQLiteAsyncConnection(path);
        await _db.CreateTableAsync<MoneyEntry>();
    }

    public async Task<List<MoneyEntry>> GetAllAsync()
    {
        await InitAsync();
        return await _db!.Table<MoneyEntry>().OrderByDescending(e => e.Date).ToListAsync();
    }

    // Id == 0 tähendab uut kirjet (INSERT), muidu muudame olemasolevat (UPDATE)
    public async Task SaveAsync(MoneyEntry entry)
    {
        await InitAsync();
        if (entry.Id == 0)
            await _db!.InsertAsync(entry);
        else
            await _db!.UpdateAsync(entry);
    }

    public async Task DeleteAsync(MoneyEntry entry)
    {
        await InitAsync();
        await _db!.DeleteAsync(entry);
    }
}
