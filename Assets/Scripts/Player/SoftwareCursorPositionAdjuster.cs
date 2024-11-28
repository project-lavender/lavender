using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class SoftwareCursorPositionAdjuster : MonoBehaviour
{
    [SerializeField] private VirtualMouseInput _virtualMouse;
    [SerializeField] private InputSystemUIInputModule _inputSystemUIInputModule;
    [SerializeField] private Canvas _canvas;

    private float _lastScaleFactor = 1;

    // ���݂�Canvas�X�P�[��
    private float CurrentScale =>
        _virtualMouse.cursorMode == VirtualMouseInput.CursorMode.HardwareCursorIfAvailable
            ? 1
            : _canvas.scaleFactor;

    // Canvas�̃X�P�[�����Ď����āAVirtualMouse�̍��W��␳����
    private void Update()
    {
        // Canvas�̃X�P�[���擾
        var scale = CurrentScale;

        // �X�P�[�����ω��������̂݁A�ȍ~�̏��������s
        if (Math.Abs(scale - _lastScaleFactor) == 0) return;

        // VirtualMouseInput�̃J�[�\���̃X�P�[����ύX����Processor��K�p
        _inputSystemUIInputModule.point.action.ApplyBindingOverride(new InputBinding
        {
            overrideProcessors = $"VirtualMouseScaler(scale={scale})"
        });

        _lastScaleFactor = scale;
    }
}