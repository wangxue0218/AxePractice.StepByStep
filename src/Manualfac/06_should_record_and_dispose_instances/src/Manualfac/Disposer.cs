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
        private readonly List<object> resolvedObjects = new List<object>();
        public void AddItemsToDispose(object item)
        {
            resolvedObjects.Add(item);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                resolvedObjects.OfType<IDisposable>().ToList().ForEach(r => r.Dispose());
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}