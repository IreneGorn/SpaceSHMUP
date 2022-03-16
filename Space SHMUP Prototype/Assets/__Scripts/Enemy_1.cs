using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : Enemy
{
    [Header("Set in Inspector: Enemy_1")]
    public float waveFrequency = 2; // Число секунд полного цикла синусоиды
    public float waveWidth = 4; // Ширина синусоиды в метрах
    public float waveRotY = 45;

    private float x0; // Начальное значение координаты X
    private float birthTime;

    void Start()
    {
        // Установить начальную координату X объекта Enemy_1
        x0 = pos.x;

        birthTime = Time.time;
    }

    // Переопределить функцию Move суперкласса Enemy
    public override void Move()
    {
        // Так как pos - это свойство, нельзя напрямую изменять pos.x, поэтому получим pos в виде вектора Vector3, доступного для изменения
        Vector3 tempPos = pos;
        // Значение theta изменяется с течением времени
        float age = Time.time - birthTime;
        float theta = Mathf.PI * 2 * age / waveFrequency;
        float sin = Mathf.Sin(theta);
        tempPos.x = x0 + waveWidth * sin;
        pos = tempPos;

        // Повернуть немного относительно оси Y
        Vector3 rot = new Vector3(0, sin * waveRotY, 0);
        this.transform.rotation = Quaternion.Euler(rot);

        // base.Move() обрабатывает движение вниз
        base.Move();
    }
}
