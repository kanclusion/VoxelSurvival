using Godot;
using System;

public partial class InteractableBox : StaticBody3D, IInteractable
{
    private MeshInstance3D _mesh;
    private ShaderMaterial _outlineMaterial;
    private Tween _tween;

    public override void _Ready()
    {
        _mesh = GetNode<MeshInstance3D>("MeshInstance3D");
        _outlineMaterial = _mesh.MaterialOverlay as ShaderMaterial;
        if (_outlineMaterial == null)
        {
            GD.PrintErr("Outline material not found!");
        } else
        {
            _outlineMaterial.SetShaderParameter("outline_width", 0.0f);
        }

    }

    public void Interact(Player player)
    {
        if (_mesh == null)
        {
            return;
        }

        StandardMaterial3D material = _mesh.Mesh.SurfaceGetMaterial(0) as StandardMaterial3D;

        if (material == null)
        {
            material = new StandardMaterial3D();
            _mesh.Mesh.SurfaceSetMaterial(0, material);
        }

        material.AlbedoColor = new Color(GD.Randf(), GD.Randf(), GD.Randf());
        GD.Print($"Куб {Name} взаимодействует!");

    }
    public string GetPromptText()
    {
        return "Press 'E' to change color";
    }

    public void SetHighlight(bool enabled)
    {
        GD.Print($"SetHighlight({enabled}) called");
        if (_outlineMaterial == null) return;

        // Убиваем старый Tween
        _tween?.Kill();
        _tween = CreateTween();

        float target = enabled ? 6.0f : 0.0f;
        float current = _outlineMaterial.GetShaderParameter("outline_width").AsSingle();

        // Плавно меняем силу обводки за 0.3 секунды
        _tween.TweenMethod(Callable.From<float>(SetOutlineStrength), current, target, 0.1f);
    }

    private void SetOutlineStrength(float value)
    {
        _outlineMaterial.SetShaderParameter("outline_width", value);
    }
}
