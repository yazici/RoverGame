﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Sol
{
    public class InventoryInfoPanel : MonoBehaviour
    {
        public Text title;
        public Image image;
        public Text description;
        public Text amountText;

        public Button dropButton;
        public Button useButton;


        public void Initialize(Ingredient i, int amount)
        {
            Debug.Log("Initializing!!");
            title.text = i.displayName;
            image.sprite = i.image;
            description.text = i.description;
            amountText.text = amount.ToString();
        }


        private void DropItem()
        {
            //TODO implement logic for dropping items
        }


        private void UseItem()
        {

        }


        private void TransferItem()
        {

        }
    }
}

