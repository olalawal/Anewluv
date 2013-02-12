using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Concurrent;


namespace Shell.MVC2.Infastructure.Chat
{
    public class SafeCollection<T> : ICollection<T>
    {

        private readonly ConcurrentDictionary<T, bool> _inner = new ConcurrentDictionary<T, bool>();


        private readonly ConcurrentDictionary<Guid, ConcurrentDictionary<int, List<object>>> values =
       new ConcurrentDictionary<Guid, ConcurrentDictionary<int, List<object>>>();

        public void Store(Guid taskId, int level, object value)
        {
            values.GetOrAdd(taskId, guid => new ConcurrentDictionary<int, List<object>>())
                .GetOrAdd(level, i => new List<object>())
                .Add(value);
        }



        public void Add(T item)
        {
            _inner.TryAdd(item, true);
        }

        public void Clear()
        {
            _inner.Clear();
        }

        public bool Contains(T item)
        {
            return _inner.ContainsKey(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _inner.Keys.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get
            {
                return _inner.Count;
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            bool value;
            return _inner.TryRemove(item, out value);
        }
                
       
        //public bool AddorUpdate(T item)
        //{
        //    // bool value;
        //    return _inner.AddOrUpdate(item, true,(key, oldValue) =>  ); );

        //    List<object>

        //}



        public IEnumerator<T> GetEnumerator()
        {
            return _inner.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}