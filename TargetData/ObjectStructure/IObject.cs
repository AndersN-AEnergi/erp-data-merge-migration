using S = SourceData.ObjectStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TargetData.ObjectStructure
{
    public interface IObject
    {
        S.IObject Source { get; }
        void Anonymize();
    }
}
