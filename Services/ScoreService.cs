using MongoDB.Driver;
using StatsApi.Models;
using Microsoft.Extensions.Options;

namespace StatsApi.Services;

public class ScoreService
{
    private readonly IMongoCollection<Score> _scoreCollection;

    public ScoreService(IMongoDatabase database)
    {
        _scoreCollection = database.GetCollection<Score>("Scores");
    }

    public async Task<List<Score>> GetAllAsync() =>
        await _scoreCollection.Find(_ => true).ToListAsync();

    public async Task<Score?> GetByIdAsync(string id) =>
        await _scoreCollection.Find(s => s.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Score newScore) =>
        await _scoreCollection.InsertOneAsync(newScore);

    public async Task UpdateAsync(string id, Score updatedScore) =>
        await _scoreCollection.ReplaceOneAsync(s => s.Id == id, updatedScore);

    public async Task DeleteAsync(string id) =>
        await _scoreCollection.DeleteOneAsync(s => s.Id == id);
}
