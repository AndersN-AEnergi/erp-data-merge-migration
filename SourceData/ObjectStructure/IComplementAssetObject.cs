using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceData.ObjectStructure
{
    public interface IComplementAssetObject : IComplement
    {
        public enum AssetObjectType
        {
            Udefinert,
            GruppeKraftverk,        // 12
            GruppeTurbiner,
            Kraftverk,              // 12nnn
            Energitilførsel,        // 300
            Magasiner,              // 312
            Magasin,                // 312.nnn
            Dammer,                 // 314
            Dam,                    // 314.nnn
            Inntak,                 // 315
            Flomløp,                // 316
            Tuneller,               // 321
            Tunell,                 // 321.nnn
            Svingkammer,            // 322
            TrykksjakterRørgater,   // 323

            Turbin,                 // 411
            Ventilsystem,           // 415
            Generator,              // 421

            Tromme,
            Aksel,
            Løpehjul1,
            Løpehjul2,
            Ledeapparat,
            Sugerør
        }
        public AssetObjectType Type { get; }
        public int TypeSequence { get; }
        public string TypeSequenceFormatted { get; }
    }
}
