using S = SourceData.ObjectStructure;
using T = TargetData.ObjectStructure;
using SourceData.ObjectStructure;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processors
{
    internal class AssetOutputProcessorIFSDelegate
    {
        bool isTest;
        string testPrefix;
        bool isAnonymize;

        public AssetOutputProcessorIFSDelegate(bool isTest, string testPrefix, bool isAnonymize)
        {
            this.isTest = isTest;
            this.testPrefix = testPrefix;
            this.isAnonymize = isAnonymize;
        }

        internal T.AssetObjectBase ProcessAssetObject(S.IAssetObject ao, T.IAssetObject parent, ref int idCounterAsset)
        {
            string id = (idCounterAsset++).ToString();
            string rdsId = "??";

            S.IComplementAssetObject complementAssetObject = (S.IComplementAssetObject)((S.AssetObjectBase)ao).Complement;

            string rdsIdElement = getRDSValue(complementAssetObject.Type, complementAssetObject.TypeSequence);
            if (rdsIdElement != null && parent != null)
            {
                rdsId = parent.RDSId + "." + rdsIdElement;
            }

            T.IAssetObject.D365ElementTypeValue elementType = T.IAssetObject.D365ElementTypeValue.Asset;

            T.IAssetObject.AssetTypeValue assetType = getAssetType(ao);

            T.AssetObjectBase result = new T.AssetObjectBase(GetTestId(id), ao.Description, rdsId, parent, elementType, T.IAssetObject.LocationTypeValue.Udefinert, assetType, ao);

            if (isAnonymize)
            {
                result.Anonymize();
            }

            foreach (S.IAssetObject aoc in ao.Children)
            {
                ProcessAssetObject(aoc, result, ref idCounterAsset);
            }

            return result;
        }

        internal string GetTestId(string id)
        {
            string result = id;

            if (isTest)
            {
                result = $"{testPrefix}.{id}";
            }
            return result;
        }
        private string? getRDSValue(S.IComplementAssetObject.AssetObjectType assetObjectType, int sequence)
        {
            string? result = null;

            if (assetObjectType == IComplementAssetObject.AssetObjectType.Tuneller)
            {
                result = "WP";
            }
            else if (assetObjectType == IComplementAssetObject.AssetObjectType.Tunell)
            {
                result = $"WP{sequence}";
            }

            return result;
        }
        private T.IAssetObject.AssetTypeValue getAssetType(S.IAssetObject ao)
        {
            T.IAssetObject.AssetTypeValue result = T.IAssetObject.AssetTypeValue.Udefinert;

            if (ao.Complement != null)
            {
                IComplementAssetObject comp = (IComplementAssetObject)ao.Complement;

                if (comp.Type == IComplementAssetObject.AssetObjectType.Ventilsystem)
                {
                    result = T.IAssetObject.AssetTypeValue.Ventilsystem;
                }
                else if (comp.Type == IComplementAssetObject.AssetObjectType.Turbin)
                {
                    result = T.IAssetObject.AssetTypeValue.Turbin;
                }
                else if (comp.Type == IComplementAssetObject.AssetObjectType.Generator)
                {
                    result = T.IAssetObject.AssetTypeValue.GeneratorÅ;
                }
            }

            return result;
        }
    }
}
