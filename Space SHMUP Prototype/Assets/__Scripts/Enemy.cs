using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Set in Inspector: Enemy")]
    public float speed = 10f; // Скорость в м/с
    public float fireRate = 0.3f; // Секунд между выстрелами (не используется)
    public float health = 10;
    public int score = 100; // Очки за уничтожение этого корабля

    protected BoundsCheck bndCheck;

    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }

    void Update()
    {
        Move();

        if(bndCheck != null && bndCheck.offDown)
        {
            // Корабль за нижнец границей, поэтому его нужно уничтожить
            Destroy(gameObject);
        }
    }

    // Это свойство: метод, действующий как поле
    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }

    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject otherGO = collision.gameObject;
        if(otherGO.tag == "ProjectileHero")
        {
            Destroy(otherGO); // Уничтожить снаряд
            Destroy(gameObject); // Уничтожить игровой объект Enemy
        }
        else
        {
            print("Enemy hit by non-ProjectileHero: " + otherGO.name);
        }
    }
}
