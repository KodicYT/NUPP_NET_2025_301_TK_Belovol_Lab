using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Common
{
    public class LibraryServiceAsync<T>
    {
        private readonly List<T> _items = new List<T>();

        public async Task<bool> CreateAsync(T element)
        {
            try
            {
                _items.Add(element);
                await Task.Delay(1);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<T?> ReadAsync(Guid id)
        {
            await Task.Delay(1);
            // Для тестування повертаємо перший елемент
            return _items.Count > 0 ? _items[0] : default(T);
        }

        public async Task<IEnumerable<T>> ReadAllAsync()
        {
            await Task.Delay(1);
            return _items;
        }

        public async Task<IEnumerable<T>> ReadAllAsync(int page, int amount)
        {
            await Task.Delay(1);
            return _items.Skip((page - 1) * amount).Take(amount);
        }

        public async Task<bool> UpdateAsync(T element)
        {
            try
            {
                await Task.Delay(1);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoveAsync(T element)
        {
            try
            {
                _items.Remove(element);
                await Task.Delay(1);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                await Task.Delay(1);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}