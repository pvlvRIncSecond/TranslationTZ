using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Infrastructure.Services.Audio
{
    [Serializable]
    public class MusicConfig
    {
        [FormerlySerializedAs("Id")] public MusicId MusicId;
        public AudioClip Clip;

    }
}