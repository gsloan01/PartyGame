using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject titlePanel;
    public GameObject menuPanel1;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void OnExitTitle()
    {
        titlePanel.SetActive(false);
        menuPanel1.SetActive(true);
    }
}
