using System;
using System.Collections.Generic;
using UnityEngine;


    [Serializable]
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public class InventoryGridData
    {
      public string OwnerId; 
      public List<InventorySlotData> Slot;
      public Vector2Int Size;
    }

    

