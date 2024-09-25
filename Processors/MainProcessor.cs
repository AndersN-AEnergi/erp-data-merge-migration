using S = SourceData.ObjectStructure;
using T = TargetData.ObjectStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlOutput;
using SourceData.ObjectStructure;
using ExcelWriter;

namespace Processors
{
    public class MainProcessor
    {
        public void Execute(string[] args,Dictionary<string, S.AssetObjectBase> assets, List<AssetStructureObject> assetStructure, bool isTest, string testPrefix, bool isAnonymize)
        {
            AssetInputProcessor assetInputProcessor = new AssetInputProcessor();
            IEnumerable<S.IAssetObject> topLevelAssets = assetInputProcessor.Execute(assets);
            IEnumerable<S.IAssetObject> topLevelAssetsToProcess = topLevelAssets;
            int startLevel = 0;

            // listAssetTree(topLevelAssets);

            S.IAssetObject selected = (from a in assets where a.Value.Id == "12126" select a.Value).FirstOrDefault();
            listAssetNode(selected, 0);
            List<S.IAssetObject> topLevelSelected = new List<S.IAssetObject>();
            topLevelSelected.Add(selected);

            // topLevelAssetsToProcess = topLevelSelected;
            // startLevel = 2;

            AssetInputComplementer assetInputComplementer = new AssetInputComplementer();
            assetInputComplementer.Execute(topLevelAssetsToProcess, startLevel);

            IEnumerable<AssetStructureObject> topLevelStructureSelected = null;

            if (args[0] == "2")
            {
                topLevelStructureSelected = (from aso in assetStructure where aso.Parent == null select aso).FirstOrDefault().Children;

                AssetStructureInputComplementer assetStructureInputComplementer = new AssetStructureInputComplementer();
                assetStructureInputComplementer.Execute(topLevelStructureSelected, 0);
            }

            IEnumerable<T.IAssetObject> topLevelTargetAssets = null;
            string filePath = string.Empty;

            if (args[0] == "1")
            {
                filePath = "C:\\work\\AssetsIFSOnly.html";
                AssetOutputProcessorIFSOnly assetOutputProcessor = new AssetOutputProcessorIFSOnly(isTest, testPrefix, isAnonymize);
                topLevelTargetAssets = assetOutputProcessor.Execute(topLevelAssetsToProcess);
            }            
            else if (args[0] == "2")
            {
                filePath = "C:\\work\\AssetsIFSPlusStructure.html";
                AssetOutputProcessorIFSPlusStructure assetOutputProcessor = new AssetOutputProcessorIFSPlusStructure(isTest, testPrefix, isAnonymize);
                topLevelTargetAssets = assetOutputProcessor.Execute(assets, topLevelStructureSelected);
            }
            if (topLevelTargetAssets != null)
            {
                string outputFilePath = "C:\\work\\";

                HtmlWriter htmlWriter = new HtmlWriter();
                htmlWriter.Execute(topLevelTargetAssets, filePath);

                ExcelWriterAssets excelWriterAssets = new ExcelWriterAssets(outputFilePath); 
                excelWriterAssets.Execute(topLevelTargetAssets, outputFilePath);
            }

        }

        private void listAssetTree(IEnumerable<S.IAssetObject> topLevelAssets)
        {
            Console.WriteLine();
            Console.WriteLine();

            foreach (var topLevelAsset in topLevelAssets) 
            {
                listAssetNode(topLevelAsset, 0);
            }
        }
        private void listAssetNode(S.IAssetObject asset, int level)
        {
            for (int i = 0; i < level; i++)
            {
                Console.Write("\t");
            }
            Console.WriteLine($"{asset.Id} - {asset.Description}");
            foreach(S.IAssetObject c in asset.Children) 
            {
                listAssetNode(c, level + 1);
            }
        }
    }
}
