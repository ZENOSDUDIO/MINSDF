using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ECount.Infrustructure
{
    internal class ProviderCollection : ICollection<IProvider>
    {
        private readonly Collection<IProvider> _provider = new Collection<IProvider>();

        public IProvider this[string name]
        {
            get
            {
                foreach (IProvider provider in _provider)
                {
                    if (provider.Name.Equals(name))
                        return provider;
                }
                return null;
            }
        }

        #region ICollection<IProvider> Members

        public void Add(IProvider item)
        {
            foreach (IProvider provider in _provider)
            {
                if (provider.Name.Equals(item.Name))
                    return;
            }
            _provider.Add(item);
        }

        public void Clear()
        {
            _provider.Clear();
        }

        public bool Contains(IProvider item)
        {
            return _provider.Contains(item);
        }

        public void CopyTo(IProvider[] array, int arrayIndex)
        {
            _provider.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _provider.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(IProvider item)
        {
            return _provider.Remove(item);
        }

        public IEnumerator<IProvider> GetEnumerator()
        {
            return _provider.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public bool Contains(string name)
        {
            foreach (IProvider provider in _provider)
            {
                if (provider.Name.Equals(name))
                    return true;
            }

            return false;
        }

        public bool Remove(string name)
        {
            IProvider item = null;
            foreach (IProvider provider in _provider)
            {
                if (provider.Name.Equals(name))
                {
                    item = provider;
                    break;
                }
            }
            if (item == null)
                return false;
            return Remove(item);
        }
    }
}