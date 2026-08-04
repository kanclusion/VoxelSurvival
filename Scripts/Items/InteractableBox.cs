using Godot;
using System;

public partial class InteractableBox : StaticBody3D, IInteractable
{
    private MeshInstance3D _mesh;
    private Tween _tween;
    private MeshInstance3D _outlineMesh;
    private StandardMaterial3D _material;

    public override void _Ready()
    {
        _mesh = GetNode<MeshInstance3D>("MeshInstance3D");
        _material = new StandardMaterial3D();
        StandardMaterial3D original = _mesh.Mesh.SurfaceGetMaterial(0) as StandardMaterial3D;
        if (original != null)
        {
            _material.AlbedoColor = original.AlbedoColor;
            _material.Metallic = original.Metallic;
            _material.Roughness = original.Roughness;
        }
    
        _mesh.MaterialOverride = _material;

        _outlineMesh = _mesh.GetNode<MeshInstance3D>("MeshInstance3D_outline");
        if (_outlineMesh != null)
            _outlineMesh.Scale = new Vector3(0.94f, 0.94f, 0.94f);
        else
            GD.PrintErr("Outline mesh not found!");
    }

    public void Interact(Player player)
    {
        if (_mesh == null)
        {
            return;
        }

        StandardMaterial3D material = _mesh.Mesh.SurfaceGetMaterial(0) as StandardMaterial3D;

        if (_material != null)
        {
            _material.AlbedoColor = new Color(GD.Randf(), GD.Randf(), GD.Randf());
        }

        GD.Print($"Куб {Name} взаимодействует!");

    }
    public string GetPromptText()
    {
        return "Press 'E' to change color";
    }

    public void SetHighlight(bool enabled)
    {
        if (_outlineMesh == null) return;

        _tween?.Kill();
        _tween = CreateTween();

        Vector3 targetScale = enabled ? Vector3.One : Vector3.Zero;
        Vector3 currentScale = _outlineMesh.Scale;

        _tween.TweenMethod(Callable.From<Vector3>(SetOutlineScale), currentScale, targetScale, 0.03f);
    }

    private void SetOutlineScale(Vector3 scale)
    {
        _outlineMesh.Scale = scale;
    }
}
