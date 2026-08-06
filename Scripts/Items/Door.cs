using Godot;

public partial class Door : Node3D, IInteractable
{
    [Export] private RigidBody3D _doorBody;      // створка (RigidBody3D)
    [Export] private float _forceMagnitude = 10f; // сила толчка (можно подобрать)
    [Export] private float _cooldown = 0.5f;      // задержка между взаимодействиями

    private bool _isOpen = false;
    private bool _isInteracting = false;          // блокировка спама

    public override void _Ready()
    {
        if (_doorBody == null)
            GD.PrintErr("DoorBody not assigned!");
    }

    public void Interact(Player player)
    {
        if (_isInteracting) return; // защита от спама

        _isInteracting = true;

        // Определяем направление силы (в локальной системе створки)
        // Обычно дверь открывается вдоль оси Z или X, смотрим по ориентации модели
        // В примере используется -basis.z, но у RigidBody3D есть свойство GlobalTransform
        Vector3 localForward = -_doorBody.GlobalTransform.Basis.Z; // куда смотрит створка
        Vector3 forceDirection = _isOpen ? -localForward : localForward; // меняем направление

        // Применяем импульс к центру масс створки
        _doorBody.ApplyImpulse(forceDirection * _forceMagnitude);

        // Переключаем состояние после небольшой задержки (чтобы не спамить)
        GetTree().CreateTimer(_cooldown).Timeout += () =>
        {
            _isOpen = !_isOpen;
            _isInteracting = false;
        };
    }

    public string GetPromptText()
    {
        return _isOpen ? "Нажмите E, чтобы закрыть дверь" : "Нажмите E, чтобы открыть дверь";
    }

    public void SetHighlight(bool enabled)
    {
        // Если нужна подсветка — можно реализовать через MaterialOverlay
        // Пока оставляем пустым (подсветка не нужна)
    }
}