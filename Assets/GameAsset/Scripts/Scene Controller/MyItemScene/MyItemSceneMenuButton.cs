using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyItemSceneMenuButton : MonoBehaviour
{
    public void OnClickButton(){
        FindObjectOfType<MyItemSceneUIController>().OnClickMenuButton(gameObject.GetComponent<MyItemSceneMenuButton>());
    }
}
