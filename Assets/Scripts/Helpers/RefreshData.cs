using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///  Updates Datactrl script so that it provides the most recent data
/// </summary>
public class RefreshData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DataCtrl.instance.RefreshData();
    }

    
}
