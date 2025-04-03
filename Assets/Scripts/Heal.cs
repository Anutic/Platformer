using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
    if (collision.CompareTag("Player"))
    {
        if (HealthSystem.health < 3)
        {
            HealthSystem.health += 1;
            Hero hero = collision.GetComponent<Hero>(); // Получаем ссылку на компонент Hero
            if (hero != null)
            {
                hero.PleaseWork(); // Вызываем метод PleaseWork() на объекте hero
            }
        }
        Destroy(gameObject);
    }
	}
    
}
