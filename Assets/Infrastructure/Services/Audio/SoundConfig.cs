using System;
using UnityEngine;

namespace Infrastructure.Services.Audio
{
    [Serializable]
    public class SoundConfig
    {
        public SoundId SoundId;
        public AudioClip Clip;
    }
}