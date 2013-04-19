using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public interface IVisitor
    {
        void Visit(IVisitable pVisitable);
    }
}
