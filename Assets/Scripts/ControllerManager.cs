using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControllerManager : MonoBehaviour
{
 //   public static ControllerManager instance;
    public static string scheme;
    public TMP_Dropdown tMP_Dropdown;
    // Start is called before the first frame update
    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    public void SelectController()
    {
        scheme = tMP_Dropdown.value.ToString();
    }

}
