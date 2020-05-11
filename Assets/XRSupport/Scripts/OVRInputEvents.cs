using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OVRInputEvents : MonoBehaviour
{
    public OVRHand leftHand;
    public OVRHand rightHand;

    public GameObject leftController;
    public GameObject rightController;

    public UnityEvent leftPrimaryPressed;
    public UnityEvent leftSecondaryPressed;
    public UnityEvent rightPrimaryPressed;
    public UnityEvent rightSecondaryPressed;
    //TODO add release events

    private bool lastLeftPrimaryPressing = false;
    private bool lastRightPrimaryPressing = false;
    private bool lastLeftSecondaryPressing = false;
    private bool lastRightSecondaryPressing = false;

    private Transform leftControllerLastTransform;
    private Transform rightControllerLastTransform;

    enum ButtonState { unchanged, released, pressed, error };

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Hide controller if tracked
        if (leftHand.IsTracked)
        {
            leftController.SetActive(false);
            ButtonState leftPrimaryButtonState = GetHandFingerPressed(leftHand, OVRHand.HandFinger.Index, ref lastLeftPrimaryPressing);
            if (leftPrimaryButtonState != ButtonState.unchanged)
            {
                if (leftPrimaryButtonState == ButtonState.pressed)
                    leftPrimaryPressed.Invoke();
            }
            ButtonState leftSecondaryButtonState = GetHandFingerPressed(leftHand, OVRHand.HandFinger.Middle, ref lastLeftSecondaryPressing);
            if (leftSecondaryButtonState != ButtonState.unchanged)
            {
                if (leftSecondaryButtonState == ButtonState.released)
                    leftSecondaryPressed.Invoke();
            }
        } else
        {
            leftController.SetActive(true);
        }

        // Hide controller if tracked
        if (rightHand.IsTracked)
        {
            rightController.SetActive(false);
            ButtonState rightPrimaryButtonState = GetHandFingerPressed(rightHand, OVRHand.HandFinger.Index, ref lastRightPrimaryPressing);
            if (rightPrimaryButtonState != ButtonState.unchanged)
            {
                if (rightPrimaryButtonState == ButtonState.released)
                    rightPrimaryPressed.Invoke();
            }
            ButtonState rightSecondaryButtonState = GetHandFingerPressed(rightHand, OVRHand.HandFinger.Middle, ref lastRightSecondaryPressing);
            if (rightSecondaryButtonState != ButtonState.unchanged)
            {
                if (rightSecondaryButtonState == ButtonState.released)
                    rightSecondaryPressed.Invoke();
            }
        } else
        {
            rightController.SetActive(true);
        }

            /*
            //if (leftHand.IsTracked)
            if (!leftHand.IsSystemGestureInProgress)
                InvokeHandEvents(ref leftHand, ref lastLeftPrimaryPressing, ref lastLeftSecondaryPressing, leftPrimaryPressed, leftSecondaryPressed);
            //if (rightHand.IsTracked)
            if (!rightHand.IsSystemGestureInProgress)
                InvokeHandEvents(ref rightHand, ref lastRightPrimaryPressing, ref lastRightSecondaryPressing, rightPrimaryPressed, rightSecondaryPressed);
            */
        }

    void InvokeHandEvents(ref OVRHand hand, ref bool lastPrimaryPressing, ref bool lastSecondaryPressing, in UnityEvent primaryEvent, in UnityEvent secondaryEvent)
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

    ButtonState GetHandFingerPressed(OVRHand hand, OVRHand.HandFinger finger, ref bool previousFingerState)
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

        if (hand.IsSystemGestureInProgress)
            return ButtonState.unchanged;
        bool fingerPressing = hand.GetFingerIsPinching(finger);
        ButtonState newButtonState = ButtonState.error;
        if (fingerPressing != previousFingerState)
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
        previousFingerState = fingerPressing;
        return newButtonState;
    }
}
