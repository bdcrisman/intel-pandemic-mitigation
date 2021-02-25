using UnityEngine;

public class ButtonClickReset : MonoBehaviour
{
    const float ButtonPressWait = 5f;
    const int ButtonPressResetCount = 5;

    private float _buttonPressTimer = 0;
    private bool _isIncrementResetCount = false;
    private int _buttonPressCount = 0;

    private void Update()
    {
        if (!_isIncrementResetCount)
            return;

        var t = Time.time;

        if (_buttonPressTimer < t)
        {
            _buttonPressCount = 0;
            _buttonPressTimer = 0f;
        }

        if (_isIncrementResetCount)
        {
            _isIncrementResetCount = false;

            if (_buttonPressTimer < t)
                _buttonPressTimer = t + ButtonPressWait;

            ++_buttonPressCount;
        }

        if (_buttonPressCount >= ButtonPressResetCount)
            GetComponent<DemoControl>().ReloadDemo();
    }

    /// <summary>
    /// BUTTON PRESS
    /// Triggers the rest count.
    /// </summary>
    public void IncrementResetCount()
    {
        _isIncrementResetCount = true;
    }
}
