using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceData.ObjectStructure
{
    public interface IObject
    {
        public enum SourceTypeValue
        {
            IFS9,
            IFS10,
            AssetStructure
        }
        IComplement Complement { get; }
        SourceTypeValue SourceType { get; }
    }
}
