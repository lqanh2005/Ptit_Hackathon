using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashUI : MonoBehaviour
{
    public Image icon;

    public void Setup(Sprite sprite)
    {
        icon.sprite = sprite;
    }
}
