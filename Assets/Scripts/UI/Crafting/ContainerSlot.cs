﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class ContainerSlot : InventorySlot
{
    public Container container;


    public void SetMyContainerData()
    {
        container.selectedIngredient = ii.ingredient;
        int ingredientAmount = container.GetIngredientAmount(ii.ingredient);
        if (ingredientAmount < 5)
        {
            container.RemoveInventoryItem(ii.ingredient, 1);
            container.playerInventory.AddInventoryItem(ii.ingredient, 1);
        }
        else
        {
            container.root.SetActive(false);
            container.playerInventory.root.SetActive(false);
            container.transferModal.Open(ingredientAmount, true);
        }
    }
}
