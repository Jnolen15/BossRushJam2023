using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    public void BringToTop(Transform thing)
    {
        thing.SetAsLastSibling();
    }
}
