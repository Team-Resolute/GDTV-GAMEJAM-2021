using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LevelChanger.Instance.ChangeScene(Level.Illusia);
    }

}
