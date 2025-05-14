using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : MonoBehaviour
{
    public TrashType binType;
}
public enum TrashType
{
    None,
    Paper,
    Plastic,
    Can,
    Glass
}
