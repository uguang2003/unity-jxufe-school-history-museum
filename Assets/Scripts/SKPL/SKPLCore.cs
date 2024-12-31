using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKPLCore : MonoBehaviour
{
    public GameObject questionManager;

    public GameObject questionUI;

    void Start()
    {
        initialize();
    }

    void Update()
    {

    }

    private void initialize()
    {
        Instantiate(questionManager, null);
        Instantiate(questionUI, null);
    }

}
