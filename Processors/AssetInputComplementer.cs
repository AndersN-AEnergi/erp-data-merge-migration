using SourceData.ObjectStructure;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processors
{
    internal class AssetInputComplementer
    {
        public void Execute(IEnumerable<IAssetObject> topLevelAssets, int startLevel)
        {
            foreach (var assetObject in topLevelAssets)
            {
                processAssetObject(assetObject, startLevel);
            }
        }

        private void processAssetObject(IAssetObject ao, int level)
        {
            // Determine type
            Tuple<IComplementAssetObject.AssetObjectType, int> typeAndSequence = getAssetObjectType(ao, level);

            ComplementAssetObjectBase c = (ao.Complement as ComplementAssetObjectBase);
            c.Type = typeAndSequence.Item1;
            c.TypeSequence = typeAndSequence.Item2; 

            foreach (IAssetObject aoc in ao.Children)
            {
                processAssetObject(aoc, level + 1);
            }
        }

        private Tuple<IComplementAssetObject.AssetObjectType, int> getAssetObjectType(IAssetObject ao, int level)
        {
            IComplementAssetObject.AssetObjectType resultType = IComplementAssetObject.AssetObjectType.Udefinert;
            int resultTypeSequence = 0;

            string[] eblElements = ao.Id.Split('.');

            if (level == 2)
            {
                if (eblElements.Length == 1 && eblElements[0].Length > 2)
                {
                    if (eblElements[0].StartsWith("12"))
                    {
                        resultType = IComplementAssetObject.AssetObjectType.Kraftverk;
                    }
                }
            }
            else if (level == 3)
            {
                if(eblElements. Length == 2)
                {
                    if (eblElements[1] == "300")
                    {
                        resultType = IComplementAssetObject.AssetObjectType.Energitilførsel;
                    }
                }
            }
            else if (level == 4)
            {
                if (eblElements.Length == 2)
                {
                    if (eblElements[1] == "312")
                    {
                        resultType = IComplementAssetObject.AssetObjectType.Magasiner;
                    }
                    else if (eblElements[1] == "314")
                    {
                        resultType = IComplementAssetObject.AssetObjectType.Dammer;
                    }
                    else if (eblElements[1] == "315")
                    {
                        resultType = IComplementAssetObject.AssetObjectType.Inntak;
                    }
                    else if (eblElements[1] == "316")
                    {
                        resultType = IComplementAssetObject.AssetObjectType.Flomløp;
                    }
                    else if (eblElements[1] == "321")
                    {
                        resultType = IComplementAssetObject.AssetObjectType.Tuneller;  // WP
                    }
                    else if (eblElements[1] == "322")
                    {
                        resultType = IComplementAssetObject.AssetObjectType.Svingkammer;
                    }
                    else if (eblElements[1] == "323")
                    {
                        resultType = IComplementAssetObject.AssetObjectType.TrykksjakterRørgater;
                    }
                }
            }
            else if (level == 5)
            {
                if (eblElements.Length == 3)
                {
                    int sequence = int.Parse (eblElements[2]);

                    if (eblElements[1] == "312")
                    {
                        resultType = IComplementAssetObject.AssetObjectType.Magasin;
                    }
                    else if (eblElements[1] == "314")
                    {
                        resultType = IComplementAssetObject.AssetObjectType.Dam;
                    }
                    else if (eblElements[1] == "315")
                    {
                        resultType = IComplementAssetObject.AssetObjectType.Inntak;
                    }
                    else if (eblElements[1] == "316")
                    {
                        resultType = IComplementAssetObject.AssetObjectType.Flomløp;
                    }
                    else if (eblElements[1] == "321")
                    {
                        resultType = IComplementAssetObject.AssetObjectType.Tunell;
                    }
                    else if (eblElements[1] == "322")
                    {
                        resultType = IComplementAssetObject.AssetObjectType.Svingkammer;
                    }
                    else if (eblElements[1] == "323")
                    {
                        resultType = IComplementAssetObject.AssetObjectType.TrykksjakterRørgater;
                    }
                    else if (eblElements[1] == "411")
                    {
                        resultType = IComplementAssetObject.AssetObjectType.Turbin;
                    }
                    else if (eblElements[1] == "415")
                    {
                        resultType = IComplementAssetObject.AssetObjectType.Ventilsystem;
                    }
                    else if (eblElements[1] == "421")
                    {
                        resultType = IComplementAssetObject.AssetObjectType.Generator;
                    }

                    if (resultType != IComplementAssetObject.AssetObjectType.Udefinert)
                    {
                        resultTypeSequence = sequence;
                    }
                }
            }

            if (resultType != IComplementAssetObject.AssetObjectType.Udefinert)
            {
                Console.WriteLine($"Id: {ao.Id} Type: {resultType} Beskrivelse: {ao.Description}");
            }

            return new Tuple<IComplementAssetObject.AssetObjectType, int>(resultType, resultTypeSequence);
        }
    }
}
