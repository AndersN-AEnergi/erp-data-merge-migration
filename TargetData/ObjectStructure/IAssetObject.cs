using S = SourceData.ObjectStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TargetData.ObjectStructure
{
    public interface IAssetObject : IObject
    {
        public enum D365ElementTypeValue
        {
            FunctionalLocation,
            Asset
        }
        public enum LocationTypeValue
        {
            Vassdragsområde,
            Kraftverksfelt,
            Magasin,
            Dam,
            Kraftstasjon,
            Udefinert, 
            TestTop
        }
        public enum AssetTypeValue
        {
            Aggregatsystem,
            Sikkerhetssystemer,
            Inntaksmagasin,
            Vannmagasin,
            Ventilsystem,
            Turbin, 
            GeneratorÅ,
            Udefinert
        }
        public string Id { get; }
        public string Description { get; }
        public IAssetObject Parent { get; }
        public IEnumerable<IAssetObject> Children { get; }

        public S.IAssetObject SourceAsset { get; }

        public string RDSId { get; }

        public D365ElementTypeValue ElementType { get; }
        public LocationTypeValue LocationType { get; }
        public AssetTypeValue AssetType { get; }
    }
}
