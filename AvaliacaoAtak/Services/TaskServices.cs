using Microsoft.Extensions.Options;
using MongoDB.Driver;
using AvaiacaoAtak.Models;

namespace AvaiacaoAtak.Services
{
    public class TaskServices
    {
        private readonly IMongoCollection<TaskModel> _taskCollection;

        public TaskServices(IOptions<TaskDbSettings> taskServices)
        {
            var mongoClient = new MongoClient(taskServices.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(taskServices.Value.DatabaseName);

            _taskCollection = mongoDatabase.GetCollection<TaskModel>
                (taskServices.Value.TaskCollectionName);

        }

        public async Task<List<TaskModel>> GetAsync() =>
            await _taskCollection.Find(x => true).ToListAsync();

        public async Task<TaskModel> GetAsync(string id) =>
           await _taskCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(TaskModel taskModel) =>
            await _taskCollection.InsertOneAsync(taskModel);

        public async Task UpdateAsync(string id, TaskModel taskModel) =>
           await _taskCollection.ReplaceOneAsync(x => x.Id == id, taskModel);

        public async Task RemoveAsync(string id) =>
            await _taskCollection.DeleteOneAsync(x => x.Id == id);

        public async Task<List<TaskModel>> GetTasksByStatusAsync(Status status)
        {
            var filter = Builders<TaskModel>.Filter.Eq(task => task.Status, status);
            return await _taskCollection.Find(filter).ToListAsync();
        }

        public async Task<List<TaskModel>> GetTasksByCreatedAtAscAsync()
        {
            return await _taskCollection
                .Find(task => true)
                .SortBy(task => task.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<TaskModel>> GetTasksByCreatedAtDescAsync()
        {
            return await _taskCollection
                .Find(task => true)
                .SortByDescending(task => task.CreatedAt)
                .ToListAsync();
        }
    }
}