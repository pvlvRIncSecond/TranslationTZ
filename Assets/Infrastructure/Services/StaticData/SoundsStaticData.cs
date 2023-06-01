using System.Collections.Generic;
using Infrastructure.Services.Audio;
using UnityEngine;

namespace Infrastructure.Services.StaticData
{
    [CreateAssetMenu(menuName = "Static Data/Audio Static Data", fileName = "SoundData")]
    public class SoundsStaticData : ScriptableObject
    {
        public List<SoundConfig> SoundConfigs;
    }
}