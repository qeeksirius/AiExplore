
using System;
using MainStory.Runtime.Scripts.App;
using MainStory.Runtime.Scripts.Effects;
using MainStory.Runtime.Scripts.View.ScenePage.Item;
using UnityEngine;

namespace MF.Art.Game
{
    public class AudioTrigger : MonoBehaviour, IEventObserver
    {
        public AudioParams[] targetAudio;

        private bool _itemLiveTriggerd = false;

        public void OnEvent(SceneEvent sceneEvent, int itemId)
        {
            if (targetAudio == null)
            {
                return;
            }

            // 仅触发一次
            if (sceneEvent == SceneEvent.ItemLive)
            {
                if (!gameObject.activeInHierarchy)
                    return;
                if (_itemLiveTriggerd)
                    return;
                _itemLiveTriggerd = true;
            }

            foreach (var audio in targetAudio)
            {
                if (audio.PlayOnEvent == sceneEvent)
                {
                    if (audio.Loop)
                    {
                        if (!MainStoryRepository.CoreProvider.IsPlayAudio(audio.target))
                        {
                            MainStoryRepository.CoreProvider.PlaySound(audio.target, true, audio.VolumeNormalize());
                        }
                    }
                    else if (audio.Times > 0)
                    {
                        LoopAudio(audio.Times, audio);
                    }
                }
            }
        }

        private void LoopAudio(int times, AudioParams audio)
        {
            if (times <= 0)
            {
                return;
            }

            MainStoryRepository.CoreProvider.PlaySound(audio.target, audio.Loop, audio.VolumeNormalize());
        }

        private void OnDisable()
        {
            if (_itemLiveTriggerd)
            {
                foreach (var audio in targetAudio)
                {
                    MainStoryRepository.CoreProvider.StopSound(audio.target);
                }
            }
        }

        private void OnDestroy()
        {
            if (targetAudio == null)
            {
                return;
            }

            foreach (var audio in targetAudio)
            {
                MainStoryRepository.CoreProvider.StopSound(audio.target);
            }
        }
    }
}