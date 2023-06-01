using System.Collections.Generic;
using Infrastructure.Services.Audio;
using UnityEngine;

namespace Infrastructure.Services.StaticData
{
    [CreateAssetMenu(menuName = "Static Data/Music Static Data", fileName = "MusicData")]
    public class MusicStaticData : ScriptableObject
    {
        public List<MusicConfig> MusicConfigs;
    }
}