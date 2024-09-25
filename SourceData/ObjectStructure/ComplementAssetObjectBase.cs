using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceData.ObjectStructure
{
    public class ComplementAssetObjectBase : IComplementAssetObject
    {
        public IComplementAssetObject.AssetObjectType Type { get; set; }

        public int TypeSequence { get; set; }

        public string TypeSequenceFormatted {
            get 
            { 
                string result = string.Empty;

                if (TypeSequence != 0)
                {
                    result = TypeSequence.ToString();
                }

                return result;
            } 
        }
    }
}
