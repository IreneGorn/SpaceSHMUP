using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static public Main S; // Объект-одиночка Main

    [Header("Set in Inspector")]
    public GameObject[] prefabEnemies; // Массив шаблонов Enemy
    public float enemySpawnPerSecond = 0.5f; // Вражеских кораблей в секунду
    public float enemyDefaultPadding = 1.5f; // Отступ для позиционирования

    private BoundsCheck bndCheck;

    void Awake()
    {
        S = this;
        // Записать в bndCheck ссылку на компонент BoundsCheck этого игрового объекта
        bndCheck = GetComponent<BoundsCheck>();
        // Вызывать SpawnEnemy() один раз (в 2 секунды при значения по умолчанию)
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
    }

    public void SpawnEnemy()
    {
        // Выбирать случайный шаблон Enemy для создания
        int ndx = Random.Range(0, prefabEnemies.Length);
        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);

        // Разместить вражеский корабль над экраном в случайной позиции x
        float enemyPadding = enemyDefaultPadding;
        if (go.GetComponent<BoundsCheck>() != null)
        {
            enemyPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
        }

        // Установить начальные координаты созданного вражеского корабля
        Vector3 pos = Vector3.zero;
        float xMin = -bndCheck.camWidth + enemyPadding;
        float xMax = bndCheck.camWidth - enemyPadding;
        pos.x = Random.Range(xMin, xMax);
        pos.y = bndCheck.camHeight + enemyPadding;
        go.transform.position = pos;

        // Снова вызвать SpawnEnemy()
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
    }

    public void DelayedRestart(float delay)
    {
        // Вызвать метод Restart() через delay секунд
        Invoke("Restart", delay);
    }

    public void Restart()
    {
        // Перезагрузить _Scene_0, чтобы перезапустить игру
        SceneManager.LoadScene("_Scene_0");
    }
}
