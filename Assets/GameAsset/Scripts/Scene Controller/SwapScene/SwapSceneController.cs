using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapSceneController : MonoBehaviour
{
    [Header("Prefab")]
    public SwapSceneItem itemSend;
    public SwapSceneItem itemGet;

    [Header("List")]
    public GameObject[] list;
    public GameObject[] whereSpawn;

}
