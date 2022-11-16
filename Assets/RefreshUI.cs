using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshUI : MonoBehaviour
{
    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = Object.FindObjectOfType<GameManager>();
        gm.RefreshUI();
    }
}
