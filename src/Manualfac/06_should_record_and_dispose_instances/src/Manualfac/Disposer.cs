using System;
using System.Collections.Generic;
using System.Linq;

namespace Manualfac
{
    class Disposer : Disposable
    {
        #region Please implements the following methods

        /*
         * The disposer is used for disposing all disposable items added when it is disposed.
         */
        private Stack<IDisposable> disposableItems = new Stack<IDisposable>();
        public void AddItemsToDispose(object item)
        {
            var disposableItem = item as IDisposable;
            if (disposableItem != null)
            {
                disposableItems.Push(disposableItem);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                while (disposableItems.Count > 0)
                {
                    disposableItems.Pop().Dispose();
                }
                disposableItems = null;
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}