using Godot;
using System;

public partial class InteractRay : RayCast3D
{
    private Label _prompt;

    public override void _Ready()
    {

        _prompt = GetNode<Label>("Prompt");
        if (_prompt == null)
        GD.PrintErr("Prompt not found!");
    }

    public override void _PhysicsProcess(double delta)
    {

        // Очищаем текст по умолчанию
        string text = "";

        // Если луч во что-то попал — ставим текст
        if (IsColliding())
            text = "something else...";

        // Применяем текст
        if (_prompt != null)
            _prompt.Text = text;
    }


}
