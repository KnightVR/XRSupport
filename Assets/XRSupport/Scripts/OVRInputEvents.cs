using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OVRInputEvents : MonoBehaviour
{
    public OVRHand leftHand;
    public OVRHand rightHand;

    public UnityEvent leftPrimaryPressed;
    public UnityEvent leftSecondaryPressed;
    public UnityEvent rightPrimaryPressed;
    public UnityEvent rightSecondaryPressed;
    //TODO add release events

    private bool lastLeftPrimaryPressing = false;
    private bool lastRightPrimaryPressing = false;
    private bool lastLeftSecondaryPressing = false;
    private bool lastRightSecondaryPressing = false;

    enum ButtonState { unchanged, released, pressed, error };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();
        if (leftHand.IsTracked)
        {
            InvokeHandEvents(leftHand, ref lastLeftPrimaryPressing, ref lastLeftSecondaryPressing, leftPrimaryPressed, leftSecondaryPressed);
        }
        if (leftHand.IsTracked)
        {
            InvokeHandEvents(rightHand, ref lastRightPrimaryPressing, ref lastRightSecondaryPressing, rightPrimaryPressed, rightSecondaryPressed);
        }
    }

    void InvokeHandEvents(OVRHand hand, ref bool lastPrimaryPressing, ref bool lastSecondaryPressing, in UnityEvent primaryEvent, in UnityEvent secondaryEvent)
    {
        /// <summary>
        /// Invoke Unity events for controller
        /// </summary>
        /// <param controller>
        ///     Input device to read states from
        /// </param>
        /// <param lastPrimaryPressing>
        ///     Previous state of primary button (should be global to keep track of button state between frames)
        /// </param>
        /// <param lastSecondaryPressing>
        ///     Previous state of secondary button (should be global to keep track of button state between frames)
        /// </param>
        /// <param primaryEvent>
        ///     Event to invoke when primary button is pressed
        /// </param>
        /// <param secondaryEvent>
        ///     Event to invoke when secondary button is pressed
        /// </param>

        ButtonState primaryButtonState = GetHandFingerPressed(hand, OVRHand.HandFinger.Index, ref lastPrimaryPressing);
        ButtonState secondaryButtonState = GetHandFingerPressed(hand, OVRHand.HandFinger.Middle, ref lastSecondaryPressing);
        if (primaryButtonState == ButtonState.pressed)
            primaryEvent.Invoke();
        if (secondaryButtonState == ButtonState.pressed)
            secondaryEvent.Invoke();
    }

    ButtonState GetHandFingerPressed(OVRHand hand, OVRHand.HandFinger finger, ref bool previousFingerPressing)
    {
        /// <summary>
        /// Gets state of button on controller (only if state has changed from false->true this frame)
        /// </summary>
        /// <param inputDevice>
        ///     Input device to read button from
        /// </param>
        /// <param button>
        ///     Button on controller to read
        ///     For example the primary button:
        ///         CommonUsages.primaryButton
        /// </param>
        /// <param previousButtonState>
        ///     Previous state of button (should be global to keep track of button state between frames)
        /// </param>
        /// <returns>state of button: 0 = state not changed, 1 = pressed, 2 = released </returns>

        bool fingerPressing = hand.GetFingerIsPinching(finger);
        ButtonState newButtonState = ButtonState.error;
        if (fingerPressing)
        {
            if (fingerPressing != previousFingerPressing)
            {
                if (fingerPressing)
                {
                    newButtonState = ButtonState.pressed;
                }
                else
                {
                    newButtonState = ButtonState.released;
                }
            }
            else
            {
                newButtonState = ButtonState.unchanged;
            }
            previousFingerPressing = fingerPressing;
        }
        return newButtonState;
    }
}
