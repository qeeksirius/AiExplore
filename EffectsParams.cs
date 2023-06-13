using MainStory.Runtime.Scripts.View.ScenePage.Item;
using UnityEngine;
using UnityEngine.Serialization;

namespace MF.Art.Game
{
    /// <summary>
    /// 用户控制动效播放
    /// </summary>
    [System.Serializable]
    public abstract class EffectsParams
    {
        /// <summary>
        /// 播放时机
        /// </summary>
        public SceneEvent PlayOnEvent = SceneEvent.Start;

        /// <summary>
        /// 循环
        /// </summary>
        public bool Loop;
        
        /// <summary>
        /// 播放次数 Loop为true时忽略此项
        /// </summary>
        public int Times = 1;

    }
    
    [System.Serializable]
    public class AudioParams: EffectsParams
    {
        [FormerlySerializedAs("targetAudio")] public AudioClip target;
        
        [SerializeField]
        [Range(0, 100)]
        private float volumePercent = 100;
        public float VolumeNormalize()
        {
            return volumePercent / 100f;
        }
    }

    [System.Serializable]
    public class ParticleParams : EffectsParams
    {
        public ParticleSystem target;
    }
}