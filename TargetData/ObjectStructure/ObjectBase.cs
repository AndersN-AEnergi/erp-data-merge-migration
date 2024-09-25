using S = SourceData.ObjectStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;

namespace TargetData.ObjectStructure
{
    public abstract class ObjectBase : IObject
    {
        public ObjectBase(S.IObject source) {
            this.source = source;
        }

        private S.IObject source;
        public S.IObject Source { get => source; }

        public abstract void Anonymize();
    }
}
