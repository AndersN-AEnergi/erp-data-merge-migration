using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceData.ObjectStructure
{
    public class AssetObjectBase : IAssetObject
    {
        private string id;

        private string description;

        private string parentId;

        private IAssetObject parent;

        private List<IAssetObject> children = new List<IAssetObject>();

        IComplementAssetObject complement;

        IObject.SourceTypeValue sourceType;

        public AssetObjectBase(string id, string description, string parentId, IObject.SourceTypeValue sourceType)
        {
            this.id = id;
            this.description = description;
            this.parentId = parentId;
            this.complement = new ComplementAssetObjectBase();
            this.sourceType = sourceType;
        }

        public string Id { get => id; }
        public string Description { get => description; }
        public string ParentId { get => parentId; }
        public IAssetObject Parent { get => parent; set => parent = value; }
        public IEnumerable<IAssetObject> Children { get => children; }

        public IComplement Complement { get { return complement; } }

        public IObject.SourceTypeValue SourceType { get => sourceType; }

        public void AddChild(IAssetObject child)
        {
            children.Add(child);
        }
    }
}
