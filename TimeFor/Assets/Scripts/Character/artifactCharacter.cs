using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class artifactCharacter : mainCharacter
{
    [Header("UI")]
    public Slot ArtifactRing;
    public Slot AmuletSlot;
    public Slot HeaddressSlot;
    public Slot QuickSlot1;
    public Slot QuickSlot2;

    [Header("Текущие усиления")]
    public ArtifactsObject ArtifactRingObject;
    public ArtifactsObject AmuletSlotObject;
    public ArtifactsObject HeaddressSlotObject;
    public foodItem QuickSlot1Object;
    public foodItem QuickSlot2Object;

    mainCharacter mainCharacter;

    private void Start()
    {
        mainCharacter = GetComponent<mainCharacter>();

        Slot.onPuttingArtifact += PutArtifact;
        Slot.onTakingArtifact += TakeArtifact;

        Slot.onUseQuickSlot += PutFood;
    }

    public void GetUI(PlayerEntryPoint player, UIEntryPoint uI)
    {
        this.playerEntry = player;
        this.uIEntry = uI;

        ArtifactRing = uI.ArtifactRing;
        AmuletSlot = uI.AmuletSlot;
        HeaddressSlot = uI.HeaddressSlot;
        QuickSlot1 = uI.QuickSlot1;
        QuickSlot2 = uI.QuickSlot2;
    }

    public void PutArtifact(Slot slot)
    {
        if (slot.artifactType == ArtifactType.Ring) 
        {
            ArtifactRingObject = slot.artifact;
            mainCharacter.indicators.SetMaxHealth(ArtifactRingObject.staminaIncrease); 
            mainCharacter.indicators.SetMaxStamina(ArtifactRingObject.staminaIncrease);
        }
        else if (slot.artifactType == ArtifactType.Amulet)
        {
            AmuletSlotObject = slot.artifact;
            
        }
        else if (slot.artifactType == ArtifactType.Headdress)
        {
            HeaddressSlotObject = slot.artifact;
            
        }
    }

    public void TakeArtifact(Slot slot)
    {
        if (slot.artifactType == ArtifactType.Ring)
        {
            mainCharacter.indicators.SetMaxHealth(-ArtifactRingObject.staminaIncrease);
            mainCharacter.indicators.SetMaxStamina(-ArtifactRingObject.staminaIncrease);
        }
        else if (slot.artifactType == ArtifactType.Amulet)
        {
            AmuletSlotObject = slot.artifact;
        }
        else if (slot.artifactType == ArtifactType.Headdress)
        {
            HeaddressSlotObject = slot.artifact;
        }
    }

    public void PutFood(Slot slot)
    {
        if(slot.Id == 4 && slot.foodItem != null)
        {
            QuickSlot1Object = slot.foodItem;
        }
        if(slot.Id == 5 && slot.foodItem != null)
        {
            QuickSlot2Object = slot.foodItem;
        }
    }
}
