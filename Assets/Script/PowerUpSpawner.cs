using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public float spawnInterval = 30f;
    public Vector2 spawnRangeX = new Vector2(-6f, 6f);
    public Vector2 spawnRangeY = new Vector2(-3f, 3f);
    private float gameDuration = 120f;
    private int maxPowerUps = 3;

    void Start()
    {
        StartCoroutine(SpawnPowerUps());
    }

    IEnumerator SpawnPowerUps()
    {
        float elapsedTime = 0f;

        while (elapsedTime < gameDuration)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (GameObject.FindGameObjectsWithTag("PowerUp").Length < maxPowerUps)
            {
                SpawnPowerUp();
            }

            elapsedTime += spawnInterval;
        }
    }

    void SpawnPowerUp()
    {
        Vector2 spawnPosition = new Vector2(
            Random.Range(spawnRangeX.x, spawnRangeX.y),
            Random.Range(spawnRangeY.x, spawnRangeY.y)
        );

        GameObject newPowerUp = Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity);
        newPowerUp.tag = "PowerUp";
    }
}




