using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapSceneController : MonoBehaviour
{
    [Header("Prefab")]
    public SwapSceneItem itemSend;
    public SwapSceneItem itemGet;

    [Header("List")]
    public GameObject listSend;
    public GameObject listGet;

    [Header("Where To Spawn")] 
    public GameObject send;
    public GameObject get;


}
