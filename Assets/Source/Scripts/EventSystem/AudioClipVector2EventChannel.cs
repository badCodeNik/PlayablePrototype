using UnityEngine;
using UnityEngine.Events;

namespace Source.Scripts.EventSystem
{
    [CreateAssetMenu(fileName = "New AudioClipVector2EventChannel", menuName = "Events/AudioClip Vector2 Event Channel")]
    public class AudioClipVector2EventChannel : ScriptableObject
    {
        public UnityAction<AudioClip, Vector2> OnEventRaised;
        public void RaiseEvent(AudioClip audioClip, Vector2 vector2)
        {
            OnEventRaised?.Invoke(audioClip, vector2);
        }
    }
}