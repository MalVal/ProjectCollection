using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCollectionClassLibrary
{
    public static class Sequence
    {
        #region Variables and properties

        private static int _current = 0;

        #endregion

        #region Methods

        public static int Next()
        {
            return _current++;
        }

        #endregion
    }
}
