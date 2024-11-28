using UnityEngine;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
using UnityEditor;

[InitializeOnLoad]
#endif
public class VirtualMouseScaler : InputProcessor<Vector2>
{
    public float scale = 1;

    private const string ProcessorName = nameof(VirtualMouseScaler);

#if UNITY_EDITOR
    static VirtualMouseScaler() => Initialize();
#endif

    // Processor�̓o�^����
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Initialize()
    {
        // �d���o�^����ƁAInput Action��Processor�ꗗ�ɐ������\������Ȃ��������邽�߁A
        // �d���`�F�b�N���s��
        if (InputSystem.TryGetProcessor(ProcessorName) == null)
            InputSystem.RegisterProcessor<VirtualMouseScaler>(ProcessorName);
    }

    // �Ǝ���Processor�̏�����`
    public override Vector2 Process(Vector2 value, InputControl control)
    {
        // VirtualMouse�̏ꍇ�̂݁A���W�n��肪�������邽��Processor��K�p����
        if (control.device.name == "VirtualMouse")
            value *= scale;

        return value;
    }
}