using Source.EasyECS;
using Source.Scripts.EventSystem;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/EventHub", fileName = "EventHub")]
public class EventHub : Configuration
{
    [SerializeField] private DoubleFloatEventChannel playerHealthChannel;
    [SerializeField] private HeroChannel heroChannel;
    [SerializeField] private EnemyChannel enemyChannel;
    [SerializeField] private Collider2DEventChannel colliderChannel;
    [SerializeField] private Vector3FloatEventChannel directionSpeedChannel;

    public Vector3FloatEventChannel DirectionSpeedChannel => directionSpeedChannel;

    public Collider2DEventChannel ColliderChannel => colliderChannel;

    public EnemyChannel EnemyChannel => enemyChannel;

    public HeroChannel HeroChannel => heroChannel;

    public DoubleFloatEventChannel PlayerHealthChannel => playerHealthChannel;
}
