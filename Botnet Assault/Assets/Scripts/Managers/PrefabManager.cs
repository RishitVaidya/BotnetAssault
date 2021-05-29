using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{

    public static PrefabManager Instance;

    public GameObject prefab_Data;

    private void Awake()
    {
        Instance = this;
    }
}
