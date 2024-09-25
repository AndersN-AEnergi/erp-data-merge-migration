using SourceData.ObjectStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processors
{
    internal class AssetInputProcessor
    {
        public IEnumerable<AssetObjectBase> Execute(Dictionary<string, AssetObjectBase> assets)
        {
            // Build parent child relationships

            foreach (KeyValuePair<string, AssetObjectBase> asset in assets)
            {
                if (!string.IsNullOrEmpty(asset.Value.ParentId))
                {
                    AssetObjectBase parentAsset = assets[asset.Value.ParentId];
                    asset.Value.Parent = parentAsset;
                    parentAsset.AddChild(asset.Value);
                }
            }

            // Find and return top level objects
            List<AssetObjectBase> result = (from a in assets where a.Value.Parent == null select a.Value).ToList<AssetObjectBase>();

            // Process tree, interprete values and complement
               
            return result;
        }
    }
}
