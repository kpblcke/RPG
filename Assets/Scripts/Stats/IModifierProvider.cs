using System.Collections.Generic;

namespace RPG.Stats
{
    public interface IModifierProvider
    {
        IEnumerable<float> GetAdditiveModifiers(StatType stat);
        IEnumerable<float> GetPercentageModifiers(StatType stat);
    }
}