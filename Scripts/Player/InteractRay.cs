using Godot;

public partial class InteractRay : RayCast3D
{
    private Label _prompt;
    private IInteractable _currentInteractable;
    private IInteractable _previousInteractable;

    public IInteractable CurrentInteractable => _currentInteractable;
    public override void _Ready()
    {

        _prompt = GetNode<Label>("Prompt");
        if (_prompt == null)
        {
            GD.PrintErr("Prompt not found!");
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        _currentInteractable = null;
        string promptText = "";

        if (IsColliding())
        {
            Node3D collider = GetCollider() as Node3D;
            if (collider != null && collider is IInteractable iteractable) 
            {
                _currentInteractable = iteractable;
                promptText = iteractable.GetPromptText();
            }
        }

        if (_previousInteractable != null && _previousInteractable != _currentInteractable)
        {
            _previousInteractable.SetHighlight(false);
        }
        if (_currentInteractable != null)
        {
            _currentInteractable.SetHighlight(true);
        }

        _previousInteractable = _currentInteractable;

        if (_prompt != null)
        {
            _prompt.Text = promptText;
        }
    }
}
