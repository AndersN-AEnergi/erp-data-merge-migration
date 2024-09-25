using S = SourceData.ObjectStructure;
using T = TargetData.ObjectStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SourceData.ObjectStructure;
using NPOI.POIFS.Properties;

namespace Processors
{
    internal class AssetOutputProcessorIFSPlusStructure
    {
        private int idCounterLocation = 100000;
        private int idCounterAsset = 200000;

        bool isTest;
        string testPrefix;
        bool isAnonymize;

        private AssetOutputProcessorIFSDelegate processorIFSDelegate; 

        public AssetOutputProcessorIFSPlusStructure(bool isTest, string testPrefix, bool isAnonymize)
        {
            this.isTest = isTest;
            this.testPrefix = testPrefix;
            this.isAnonymize = isAnonymize;

            processorIFSDelegate = new AssetOutputProcessorIFSDelegate(isTest, testPrefix, isAnonymize);
        }

        /***
         * Returns a list of the top level objects in the target format
         */
        public IEnumerable<T.IAssetObject> Execute(Dictionary<string, S.AssetObjectBase> assets, IEnumerable<AssetStructureObject> topLevelAssetStructures)
        {
            List<T.IAssetObject> result = new List<T.IAssetObject>();

            T.IAssetObject topParent = null;

            if (isTest)
            {
                S.IObject topParentSource = new S.AssetStructureObject(string.Empty, string.Empty, string.Empty, string.Empty, null);
                topParent = new T.AssetObjectBase(
                    processorIFSDelegate.GetTestId("000000"),
                    $"Test set {testPrefix}", 
                    string.Empty, 
                    null, 
                    T.IAssetObject.D365ElementTypeValue.FunctionalLocation, 
                    T.IAssetObject.LocationTypeValue.TestTop,
                    T.IAssetObject.AssetTypeValue.Udefinert,
                    topParentSource);
                result.Add(topParent);
            }

            foreach (var assetStreuctureObject in topLevelAssetStructures)
            {
                T.IAssetObject assetObject = processAssetObject(assetStreuctureObject, topParent, 0, assets);
                if (!isTest)
                {
                    result.Add(assetObject);
                }
            }

            return result;
        }

        private T.AssetObjectBase processAssetObject(AssetStructureObject aso, T.IAssetObject parent, int level, Dictionary<string, S.AssetObjectBase> assets)
        {

            string id; 

            T.IAssetObject.D365ElementTypeValue elementType;
            T.IAssetObject.LocationTypeValue locationType = T.IAssetObject.LocationTypeValue.Udefinert;
            T.IAssetObject.AssetTypeValue assetType = T.IAssetObject.AssetTypeValue.Udefinert;

            if (level < 3)
            {
                elementType = T.IAssetObject.D365ElementTypeValue.FunctionalLocation;
                id = (idCounterLocation++).ToString();

                if (aso.Type == AssetStructureObject.AssetStructureObjectType.Vassdragsområde)
                {
                    locationType = T.IAssetObject.LocationTypeValue.Vassdragsområde;
                }
                else if (aso.Type == AssetStructureObject.AssetStructureObjectType.Kraftverksfelt)
                {
                    locationType = T.IAssetObject.LocationTypeValue.Kraftverksfelt;
                }
                else if (aso.Type == AssetStructureObject.AssetStructureObjectType.AnleggMagasin)
                {
                    locationType = T.IAssetObject.LocationTypeValue.Magasin;
                }
                else if (aso.Type == AssetStructureObject.AssetStructureObjectType.AnleggDam)
                {
                    locationType = T.IAssetObject.LocationTypeValue.Dam;
                }
                else if (aso.Type == AssetStructureObject.AssetStructureObjectType.AnleggKraftstasjon)
                {
                    locationType = T.IAssetObject.LocationTypeValue.Kraftstasjon;
                }
            }
            else 
            {
                elementType = T.IAssetObject.D365ElementTypeValue.Asset;
                id = (idCounterAsset++).ToString();

                if (aso.Type == AssetStructureObject.AssetStructureObjectType.AnleggAggregatsystem)
                {
                    assetType = T.IAssetObject.AssetTypeValue.Aggregatsystem;
                }
                else if (aso.Type == AssetStructureObject.AssetStructureObjectType.AnleggSikkerhetssystemer)
                {
                    assetType = T.IAssetObject.AssetTypeValue.Sikkerhetssystemer;
                }
                else if (aso.Type == AssetStructureObject.AssetStructureObjectType.AnleggInntaksmagasin)
                {
                    assetType = T.IAssetObject.AssetTypeValue.Inntaksmagasin;
                }
                else if (aso.Type == AssetStructureObject.AssetStructureObjectType.AnleggVannmagasin)
                {
                    assetType = T.IAssetObject.AssetTypeValue.Vannmagasin;
                }
            }

            T.AssetObjectBase result = new T.AssetObjectBase(
                processorIFSDelegate.GetTestId(id),
                aso.Description, 
                string.Empty, 
                parent, 
                elementType, 
                locationType,
                assetType,
                aso);

            if (isAnonymize)
            {
                result.Anonymize();
            }

            if (level < 3)
            {
                foreach (AssetStructureObject asoc in aso.Children)
                {
                    processAssetObject(asoc, result, level + 1, assets);
                }
            }
            else if (level == 3)
            {
                foreach (AssetStructureObject asoc in aso.Children)
                {
                    string eblCode = asoc.Id;
                    if (assets.ContainsKey(eblCode))
                    {
                        IAssetObject asset = assets[eblCode];
                        processorIFSDelegate.ProcessAssetObject(asset, result, ref idCounterAsset);
                    }
                    else
                    {
                        Console.WriteLine($"EBL code: {eblCode} not found !");
                    }
                }
            }
            return result;
        }
    }
}
