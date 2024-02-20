using CosmosMVCApp.Models;
using Microsoft.Azure.Cosmos;

namespace CosmosMVCApp.Services
{
    public class TodoService
    {
        private readonly Container _container;
        private readonly string _containerName;

        public TodoService(string connectionString, string database, string containerName)
        {
            _container = new CosmosClient(connectionString)
                           .GetDatabase(database)
                            .GetContainer(containerName);

            _containerName = containerName;
        }

        public async Task<List<Todo>> GetTodosAsync()
        {
            string query = $@"SELECT * FROM {_containerName}";

            var iterator = _container.GetItemQueryIterator<Todo>(query);

            List<Todo> todoList = new List<Todo>();
            while (iterator.HasMoreResults)
            {
                var item = await iterator.ReadNextAsync();
                todoList.AddRange(item);
            }

            return todoList;
        }

        public async Task<bool> AddTodo(Todo model)
        {

            model.Id = Guid.NewGuid().ToString();
            var itemResponse = await _container.CreateItemAsync<Todo>(model, new PartitionKey(model.Priority));
            if (itemResponse.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return true;
            }
            else
            {

                return false;
            }

        }

    }
}
