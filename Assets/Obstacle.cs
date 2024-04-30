using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Куда пошел ?!");
        Vector2 direction = (transform.position - other.transform.forward).normalized;
        other.GetComponent<Rigidbody2D>().AddForce(direction * 10f);
    }
}

