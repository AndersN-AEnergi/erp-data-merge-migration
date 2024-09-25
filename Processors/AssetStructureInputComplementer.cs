using SourceData.ObjectStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SourceData.ObjectStructure.AssetStructureObject;

namespace Processors
{
    internal class AssetStructureInputComplementer
    {
        public void Execute(IEnumerable<AssetStructureObject> topLevelAssetStructures, int startLevel)
        {
            foreach (var assetStructureObject in topLevelAssetStructures)
            {
                processAssetStructureObject(assetStructureObject, startLevel);
            }
        }

        private void processAssetStructureObject(AssetStructureObject aso, int level)
        {
            // Determine type
            AssetStructureObjectType type = getAssetStructureObjectType(aso, level);
            aso.Type = type;

            foreach (AssetStructureObject aoc in aso.Children)
            {
                processAssetStructureObject(aoc, level + 1);
            }
        }

        private AssetStructureObjectType getAssetStructureObjectType(AssetStructureObject aso, int level)
        {
            AssetStructureObjectType result = AssetStructureObjectType.Undefined;

            if (aso.TypeFromInput == "Vassdragsområde")
            {
                result = AssetStructureObjectType.Vassdragsområde;
            }
            else if (aso.TypeFromInput == "Kraftverksfelt")
            {
                result = AssetStructureObjectType.Kraftverksfelt;
            }
            else if (aso.TypeFromInput == "Anlegg, magasin")
            {
                result = AssetStructureObjectType.AnleggMagasin;
            }
            else if (aso.TypeFromInput == "Anlegg, dam")
            {
                result = AssetStructureObjectType.AnleggDam;
            }
            else if (aso.TypeFromInput == "Anlegg, kraftstasjon")
            {
                result = AssetStructureObjectType.AnleggKraftstasjon;
            }
            else if (aso.TypeFromInput == "Aggregat")
            {
                result = AssetStructureObjectType.AnleggAggregatsystem;
            }
            else if (aso.TypeFromInput == "Sikkerhetssystemer")
            {
                result = AssetStructureObjectType.AnleggSikkerhetssystemer;
            }
            else if (aso.TypeFromInput == "Inntaksmagasin")
            {
                result = AssetStructureObjectType.AnleggInntaksmagasin;
            }
            else if (aso.TypeFromInput == "Vannmagasin")
            {
                result = AssetStructureObjectType.AnleggVannmagasin;
            }

            return result;
        }
    }
}
