using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //damage enemy
        if (collision.collider.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            StartCoroutine(enemy.DamageCharacter(damage, 0.0f));
            Destroy(gameObject);
        }
        else if (collision.collider.tag == "Objects")
        {
            Destroy(gameObject);
        }
        else
            StartCoroutine(DestroyBulletAfer(gameObject, 2f)); // destroy bullet after 2 seconds
        
    }
    
    IEnumerator DestroyBulletAfer(GameObject go, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(go);
    }
}
