using System;
using System.Collections.Generic;
using UnityEngine;

public class PersistentManager : MonoBehaviour
{
    private static Dictionary<String, GameObject> persistentObjects = new();

    public static void Register(string nameGo, GameObject go)
    {
        persistentObjects.Add(nameGo, go);
        DontDestroyOnLoad(go);
    }

    public static void Remove(string nameGo)
    {
        if (persistentObjects.ContainsKey(nameGo))
        {
            Destroy(persistentObjects[nameGo]);
            persistentObjects.Remove(nameGo);
        }
    }
}