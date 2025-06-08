using System;
using UnityEngine;

internal class Divider:MonoBehaviour
{
    public event Action<Cube> Divided;

    private Exploder _exploder = new Exploder();

    public void Divide(Cube cube)
    {
        float maxChance = 100f;
        float minChance = 0f;

        float divideChance = UnityEngine.Random.Range(minChance, maxChance);

        if (divideChance <= cube.CurrentDivideChance)
        {
            Vector3 position = cube.transform.position;
            Vector3 scale = cube.transform.localScale;

            Divided?.Invoke(cube);
        }
        else
        {
            _exploder.Explode(cube);
        }

        Destroy(cube.gameObject);
    }
}

