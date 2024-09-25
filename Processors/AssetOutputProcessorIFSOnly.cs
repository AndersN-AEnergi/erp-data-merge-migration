using S = SourceData.ObjectStructure;
using T = TargetData.ObjectStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SourceData.ObjectStructure;

namespace Processors
{
    internal class AssetOutputProcessorIFSOnly
    {
        bool isTest;
        string testPrefix;
        bool isAnonymize;

        private AssetOutputProcessorIFSDelegate processorIFSDelegate;

        public AssetOutputProcessorIFSOnly(bool isTest, string testPrefix, bool isAnonymize)
        {
            this.isTest = isTest;
            this.testPrefix = testPrefix;
            this.isAnonymize = isAnonymize;

            processorIFSDelegate = new AssetOutputProcessorIFSDelegate(isTest, testPrefix, isAnonymize);
        }

        /***
         * Returns a list of the top level objects in the target format
         */
        public IEnumerable<T.IAssetObject> Execute(IEnumerable<S.IAssetObject> topLevelAssets)
        {
            int idCounterAsset = 300000;

            List<T.IAssetObject> result = new List<T.IAssetObject>();

            foreach (var assetObject in topLevelAssets)
            {
                result.Add(processorIFSDelegate.ProcessAssetObject(assetObject, null, ref idCounterAsset));
            }

            return result;
        }
    }
}
