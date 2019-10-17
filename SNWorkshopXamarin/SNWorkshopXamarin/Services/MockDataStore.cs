using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SNWorkshopXamarin.Models;

namespace SNWorkshopXamarin.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>();
            var mockItems = new List<Item>
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "Primeiro item", Description="Descrição mockada." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Segundo item", Description="Descrição mockada." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Terceiro item", Description="Descrição mockada." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Quarto item", Description="Descrição mockada." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Quinto item", Description="Descrição mockada." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Sexto item", Description="Descrição mockada." }
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}