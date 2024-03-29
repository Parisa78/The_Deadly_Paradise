﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveData(Vector3 player, Vector3 mainCamPos)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/paradise.deadly";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(player, mainCamPos);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/paradise.deadly";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData save = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return save;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static SaveData ResetData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/paradise.deadly";
        FileStream stream = new FileStream(path, FileMode.Create);
        Vector3 player = new Vector3(-0.143068f, 1.602692f, 0);
        var mainCamPos = new Vector3(0, 0, -10);
        SaveData data = new SaveData(player,mainCamPos);
        data.hp = 100;
        data.scene = "Village1";
        data.unlockedSwordCount = 1;
        data.shardsCount = 0;

        formatter.Serialize(stream, data);
        stream.Close();
        return data;
    }


    // quit for quit button
    public static void QuitGame()
    {
        Application.Quit();
    }
}
