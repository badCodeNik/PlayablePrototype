using Source.EasyECS;
using Source.Scripts.EventSystem;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/EventHub", fileName = "EventHub")]
public class EventHub : Configuration
{
    [SerializeField] private DoubleFloatEventChannel playerHealthChannel;
    
    public DoubleFloatEventChannel PlayerHealthChannel => playerHealthChannel;

}
