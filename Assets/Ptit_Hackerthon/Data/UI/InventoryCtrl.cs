using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCtrl : MonoBehaviour
{
    public List<SlotBase> slotBases = new List<SlotBase>();
    public List<DraggableItem> draggableItems = new List<DraggableItem>();
    public List<TrashData> trashSlots = new List<TrashData>();
    public List<RectTransform> bins = new List<RectTransform>();
    public void Init()
    {
        for(int i = 0; i < trashSlots.Count; i++)
        {
            if (i < slotBases.Count)
            {
                slotBases[i].avt.sprite = trashSlots[i].image;
                slotBases[i].trashData = trashSlots[i];
                slotBases[i].isEmpty = false;
                draggableItems[i].type = slotBases[i].trashData.type;
                draggableItems[i].Init();
            }
        }
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
    }

    public void Show()
    {
        Init();
        this.gameObject.SetActive(true);
    }
}
