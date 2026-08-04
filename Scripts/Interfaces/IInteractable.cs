using Godot;
using System;

public interface IInteractable
{
    void Interact(Player player);

    string GetPromptText();
    
    void SetHighlight(bool enabled);
}
