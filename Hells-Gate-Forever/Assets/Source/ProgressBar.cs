using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public void SetProgress(float progress)
    {
        this.transform.localScale = new Vector3(progress, 1, 1);
    }
}
