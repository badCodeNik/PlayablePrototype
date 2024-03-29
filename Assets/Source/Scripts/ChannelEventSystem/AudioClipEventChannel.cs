using UnityEngine;
using UnityEngine.Events;

namespace Source.Scripts.EventSystem
{
    [CreateAssetMenu(fileName = "New AudioClipEventChannel", menuName = "Events/AudioClip Event Channel")]
    public class AudioClipEventChannel : ScriptableObject
    {
        public UnityAction<AudioClip> OnEventRaised;
        public void RaiseEvent(AudioClip audioClip)
        {
            OnEventRaised?.Invoke(audioClip);
        }
    }
}