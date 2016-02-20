﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    public class Container : Menu
    {
        public GameObject background;
        public ContainerSlot containerSlotPrefab;
        public Transform InventorySlotContainer;
        public Button closeButton;
        public Button toggleButton;

        public List<Ingredient> ingredientsInInventory = new List<Ingredient>();
        public List<ContainerSlot> containerSlots = new List<ContainerSlot>();

        private ContainerObject currentContainer;

        private const string GIVE_TEXT = "Give";
        private const string TAKE_TEXT = "Take";


        public override void Open()
        {
            base.Open();
        }


        public void Toggle()
        {
            Inventory playerInventory = UIManager.GetMenu<Inventory>();

            Close();
            playerInventory.Open(true);
        }


        public void Open(List<Ingredient> ingredients, ContainerObject container)
        {
            currentContainer = container;
            ingredientsInInventory = ingredients;
            InitializeInventorySlots();
            Open();
        }


        public override void Close()
        {
            base.Close();
        }


        public virtual int GetIngredientAmount(int ingredientId)
        {
            int count = 0;
            foreach (Ingredient i in ingredientsInInventory)
            {
                if (i.id == ingredientId) count++;
            }
            return count;
        }


        public virtual int GetIngredientAmount(Ingredient ingredient)
        {
            int count = 0;
            foreach (Ingredient i in ingredientsInInventory)
            {
                if (i.id == ingredient.id) count++;
            }
            return count;
        }


        public virtual void AddInventoryItem(Ingredient ingredient, int count)
        {
            for (int i = count; i > 0; i--)
            {
                ingredientsInInventory.Add(ingredient);
            }
            Debug.Log("item added");
            InitializeInventorySlots();
        }


        public virtual void RemoveInventoryItem(Ingredient ingredient, int count)
        {
            if (ingredientsInInventory.Count < count)
            {
                Debug.LogError("unable to comply, insufficient inventory ingredients");
                return;
            }

            while (count > 0)
            {
                for (int i = 0; i < ingredientsInInventory.Count; i++)
                {
                    if (ingredientsInInventory[i].id == ingredient.id)
                    {
                        ingredientsInInventory.RemoveAt(i);
                        count--;
                        break;
                    }
                }
            }

            InitializeInventorySlots();
        }


        public virtual void InitializeInventorySlots()
        {
            for (int i = containerSlots.Count - 1; i >= 0; i--)
            {
                Destroy(containerSlots[i].gameObject);
            }

            containerSlots.Clear();
            List<Ingredient> encounteredIngredients = new List<Ingredient>();
            foreach (Ingredient i in ingredientsInInventory)
            {
                if (!encounteredIngredients.Contains(i))
                {
                    BuildInventorySlot(i, GetIngredientAmount(i));
                    encounteredIngredients.Add(i);
                }
            }
            Debug.Log("inventroy slots initialized");
        }


        public virtual void BuildInventorySlot(Ingredient ingredient, int count)
        {
            ContainerSlot newSlot = Instantiate(containerSlotPrefab) as ContainerSlot;
            newSlot.transform.SetParent(InventorySlotContainer);
            newSlot.transform.localScale = Vector3.one;

            newSlot.image.sprite = ingredient.image;
            newSlot.Amount = count;
            newSlot.ii.ingredient = ingredient;
            newSlot.ii.amount = count;

            containerSlots.Add(newSlot);
        }


        private void Update()
        {
            if (IsActive)
            {
                if (Input.GetKeyDown(KeyCode.E)) Close();
            }
        }


        private void Awake()
        {
            closeButton.onClick.AddListener(Close);
            InitializeInventorySlots();
            toggleButton.onClick.AddListener(Toggle);
        }
    }
}