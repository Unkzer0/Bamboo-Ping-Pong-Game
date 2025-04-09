using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIButtonToggler : MonoBehaviour
{
    public GameObject controlUIGroup; // Assign this in Inspector
    private bool isVisible = true;

    public void ToggleUI()
    {
        isVisible = !isVisible;
        controlUIGroup.SetActive(isVisible);

}
}
