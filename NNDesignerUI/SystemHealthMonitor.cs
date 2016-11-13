using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNDesignerUI
{
    interface SystemHealthMonitor
    {
        void AttachToModel();
        void DeAttach();
        bool IsDisposed();
    }
}
