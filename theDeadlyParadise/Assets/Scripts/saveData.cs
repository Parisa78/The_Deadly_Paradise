using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SaveData
{
    public string scene;
    public int hp;
    public float[] position;
    public int unlockedSwordCount;
    public int shardsCount;
    public float[] cameraPosition;

    public SaveData(Vector3 playerPos, Vector3 mainCameraPosition)
    {
        scene = SceneManager.GetActiveScene().name;

        hp = gameStatus.instance.playerHP;
        unlockedSwordCount = gameStatus.instance.unlockedSwordCount;
        shardsCount = gameStatus.instance.shardsCount;
        position = new float[3];
        cameraPosition = new float[3];
        position[0] = playerPos.x;
        position[1] = playerPos.y;
        position[2] = playerPos.z;
        cameraPosition[0] = mainCameraPosition.x;
        cameraPosition[1] = mainCameraPosition.y;
        cameraPosition[2] = mainCameraPosition.z;
    }
}
