using S = SourceData.ObjectStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TargetData.ObjectStructure
{
    public class AssetObjectBase : ObjectBase, IAssetObject
    {
        private string id;

        private string description;

        private string rdsId;

        private IAssetObject parent;

        private IAssetObject.D365ElementTypeValue elementType;

        private IAssetObject.LocationTypeValue locationType;
        private IAssetObject.AssetTypeValue assetType;

        private List<IAssetObject> children = new List<IAssetObject>();

        public AssetObjectBase(string id, string description, string rdsId, IAssetObject parent, IAssetObject.D365ElementTypeValue elementType, IAssetObject.LocationTypeValue locationType, IAssetObject.AssetTypeValue assetType, S.IObject source) : base(source)
        {
            this.id = id;
            this.description = description;
            this.rdsId = rdsId;
            this.parent = parent;
            this.elementType = elementType;
            this.locationType = locationType;
            this.assetType = assetType;

            if (parent != null)
            {
                ((AssetObjectBase)parent).AddChild(this);
            }
        }

        private void AddChild(AssetObjectBase child)
        {
            children.Add(child);
        }

        public string Id { get => id; }
        public string Description { get => description; }
        public string RDSId { get => rdsId; }
        public IAssetObject.D365ElementTypeValue ElementType { get => elementType; }
        public IAssetObject.LocationTypeValue LocationType { get => locationType; }
        public IAssetObject.AssetTypeValue AssetType { get => assetType; }

        public IAssetObject Parent { get => parent; set => parent = value; }
        public IEnumerable<IAssetObject> Children { get => children; }

        public S.IAssetObject SourceAsset {
            get
            {
                S.IAssetObject result = null;

                if (Source is S.IAssetObject)
                {
                    result = Source as S.IAssetObject;
                }
                else
                {
                    throw new InvalidOperationException("Source asset is not expected type.");
                }
                return result;
            }
        }

        public override void Anonymize()
        {
            if (elementType == IAssetObject.D365ElementTypeValue.FunctionalLocation)
            {
                description = $"{locationType}_{id}";
            }
            else if (elementType == IAssetObject.D365ElementTypeValue.Asset)
            {
                description = $"{assetType}_{id}";
            }
            else
            {
                description = "?????????????";
            }
        }
    }
}
