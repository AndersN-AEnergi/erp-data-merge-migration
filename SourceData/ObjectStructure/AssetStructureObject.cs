using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceData.ObjectStructure
{
    public class AssetStructureObject : IAssetObject
    {
        public enum AssetStructureObjectType
        {
            Vassdragsområde,
            Kraftverksfelt,
            AnleggKraftstasjon,
            AnleggMagasin,
            AnleggDam,

            AnleggAggregatsystem,
            AnleggSikkerhetssystemer,
            AnleggInntaksmagasin,
            AnleggVannmagasin,

            Undefined
        }
        private string rdsId;
        private string eblId;
        private string description;
        private string typeFromInput;
        private AssetStructureObjectType type = AssetStructureObjectType.Undefined;

        private AssetStructureObject parent;
        private List<AssetStructureObject> children = new List<AssetStructureObject>();
        IComplementAssetObject complement;

        public AssetStructureObject(string rdsId, string eblId, string name, string typeFromInput, AssetStructureObject parent)
        {
            this.rdsId = rdsId;
            this.eblId = eblId;
            this.description = name;
            this.typeFromInput = typeFromInput;
            this.parent = parent;
            this.complement = new ComplementAssetObjectBase();
        }

        public string RdsId { get => rdsId; set => rdsId = value; }
        public string EblId { get => eblId; set => eblId = value; }
        public string Description { get => description; set => description = value; }
        public string TypeFromInput { get => typeFromInput; set => typeFromInput = value; }
        public AssetStructureObjectType Type { get => type; set => type = value; }

        public AssetStructureObject Parent { get => parent; set => parent = value; }

        public IComplement Complement { get => complement; }


        public IEnumerable<AssetStructureObject> Children { get => children; }

        public string RDS
        {
            get
            {
                StringBuilder result = new StringBuilder();

                if (parent != null)
                {
                    result.Append(parent.RDS);
                    result.Append('.');
                }
                result.Append(rdsId);

                return result.ToString();
            }
        }

        public string Id => EblId;

        IAssetObject IAssetObject.Parent => Parent;

        IEnumerable<IAssetObject> IAssetObject.Children => Children;

        public IObject.SourceTypeValue SourceType => IObject.SourceTypeValue.AssetStructure;

        public void AddChild(AssetStructureObject child)
        {
            children.Add(child);
        }
    }
}
