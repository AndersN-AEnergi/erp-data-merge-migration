using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceData.ObjectStructure
{
    public interface IAssetObject : IObject
    {
        public string Id { get; }
        public string Description { get; }
        public IAssetObject Parent { get; }
        public IEnumerable<IAssetObject> Children { get; }
    }
}
