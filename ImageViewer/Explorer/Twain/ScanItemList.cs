using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;

namespace ClearCanvas.ImageViewer.Explorer.Twain
{
    internal class ScanItemList : IList<IScanItem> 
    {
        private readonly BindingList<IScanItem> _bindingList;

        public ScanItemList(BindingList<IScanItem> bindingList)
        {
            _bindingList = bindingList;
        }

        public BindingList<IScanItem> BindingList
        {
            get { return _bindingList; }
        }

        #region IList<IScanItem> Members

        public int IndexOf(IScanItem item)
        {
            return _bindingList.IndexOf(item);
        }

        public void Insert(int index, IScanItem item)
        {
            _bindingList.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            if (index < _bindingList.Count)
            {
                if (_bindingList[index].IsLocked)
                    throw new InvalidOperationException("Unable to remove item because it is locked");

                _bindingList.RemoveAt(index);
            }
        }

        public IScanItem this[int index]
        {
            get { return _bindingList[index]; }
            set { throw new InvalidOperationException("Cannot set items via the indexer."); }
        }

        #endregion

        #region ICollection<IScanItem> Members

        public void Add(IScanItem item)
        {
            _bindingList.Add(item);
        }

        public void Clear()
        {
            if (_bindingList.Any(item => item.IsLocked))
                throw new InvalidOperationException("Unable to clear the list; there is a lcoked item.");

            _bindingList.Clear();
        }

        public bool Contains(IScanItem item)
        {
            return _bindingList.Contains(item);
        }

        public void CopyTo(IScanItem[] array, int arrayIndex)
        {
            _bindingList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _bindingList.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(IScanItem item)
        {
            if (item.IsLocked)
                throw new InvalidOperationException("Unable to remove item because it is locked.");

            return _bindingList.Remove(item);
        }

        #endregion

        #region IEnumerable<IScanItem> Members

        public IEnumerator<IScanItem> GetEnumerator()
        {
            return _bindingList.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _bindingList.GetEnumerator();
        }

        #endregion
    }
}
