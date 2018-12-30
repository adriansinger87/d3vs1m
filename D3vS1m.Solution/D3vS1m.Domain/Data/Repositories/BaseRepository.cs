using D3vS1m.Domain.System.Logging;
using System.Collections;
using System.Collections.Generic;

namespace D3vS1m.Domain.Data.Repositories
{
    /// <summary>
    /// abstract base class with functionality for concrete repository classes 
    /// </summary>
    /// <typeparam name="T">type of the items in the repository</typeparam>
    public abstract class RepositoryBase<T> : IEnumerable
    {
        // -- fields

        private string _name;

        /// <summary>
        /// list of generic items of type T
        /// </summary>
        protected List<T> _items;

        // -- constructor

        /// <summary>
        /// default constructor that creates new list of T
        /// </summary>
        public RepositoryBase()
        {
            _items = new List<T>();
        }

        // -- methods

        /// <summary>
        /// Adds a new item to the repository
        /// </summary>
        /// <param name="item">the new instance of T that shall be added to the list</param>
        /// <returns>returns the new item</returns>
        public T Add(T item)
        {
            this._items.Add(item);
            return item;
        }

        public void AddRange(T[] items)
        {
            _items.AddRange(items);
        }

        /// <summary>
        /// Removes an item at a specific index
        /// </summary>
        /// <param name="index">zero-based index</param>
        public void RemoveAt(int index)
        {
            _items.RemoveAt(index);
        }

        /// <summary>
        /// Removes an specific item that is stored in the repository
        /// </summary>
        /// <param name="item">the instance of T that shall be removed</param>
        /// <returns></returns>
        public bool Remove(T item)
        {
            return _items.Remove(item);
        }

        public void Clear()
        {
            _items.Clear();
            Log.Trace($"{Name} cleared");
        }

        /// <summary>
        /// overwritten method
        /// </summary>
        /// <returns>Returns the name property and the count of items</returns>
        public override string ToString()
        {
            return $"{Name}: {_items.Count} {(_items.Count == 1 ? "item" : "items")}";
        }

        /// <summary>
        /// Returns the IEnumerator of the repository list
        /// </summary>
        /// <returns>the result of the GetEnumerator method of the list</returns>
        public IEnumerator GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        // -- properties

        /// <summary>
        /// Gets or sets the name of the instance
        /// </summary>
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_name))
                {
                    return "repository of type " + typeof(T).Name;
                }
                else
                {
                    return this._name;
                }
            }
            set
            {
                this._name = value;
            }
        }

        /// <summary>
        /// Gets the count of the internal repository list
        /// </summary>
        public int Count { get { return _items.Count; } }

        // -- indexer

        /// <summary>
        /// Zero-based intexer to be able to iterate the repository directly
        /// </summary>
        /// <param name="i">zero-based index</param>
        /// <returns>the instance of T at the index i</returns>
        public T this[int i]
        {
            get { return _items[i]; }
        }
    }
}
