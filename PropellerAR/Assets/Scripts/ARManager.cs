using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using TMPro;
using Vuforia;

public class ARManager : MonoBehaviour
{
    public static ARManager Instance;

    public AnimState animState;
    public Animator proppelerAnimator;
    public Button nextAnimButton;
    public Button resetAnimButton;
    public TextMeshProUGUI runIndicatorText;
    public MidAirPositionerBehaviour midAirPos;

    // Start is called before the first frame update
    void Start()
    {
      if (Instance == null)
        {
            Instance = this;
        }   

      else if (Instance != this)
        {
            Destroy(gameObject);
        }

        nextAnimButton.onClick.RemoveAllListeners();
        nextAnimButton.onClick.AddListener(() => {
            PlayNextAnimation();
        });

        resetAnimButton.onClick.RemoveAllListeners();
        resetAnimButton.onClick.AddListener(() =>
        {
            ResetAnimation();
        });
    }

    private void OnDisable()
    {
        nextAnimButton.onClick.RemoveAllListeners();
    }


    public void OnTrackableFound()
    {
        midAirPos.MidAirIndicator.SetActive(false);
    }

    public void OnTrackableLost()
    {
        midAirPos.MidAirIndicator.SetActive(true);
    }

    public void PlayNextAnimation()
    {
        switch (animState)
        {
            case AnimState.Idle:
                runIndicatorText.text = "Run 2";
                animState = AnimState.InstallWasher1;
                proppelerAnimator.SetTrigger(animState.ToString());
                break;

            case AnimState.InstallWasher1:
                animState = AnimState.Rotate1;
                proppelerAnimator.SetTrigger(animState.ToString());
                break;

            case AnimState.Rotate1:
                runIndicatorText.text = "Run 3";
                animState = AnimState.InstallWasher2;
                proppelerAnimator.SetTrigger(animState.ToString());
                break;

            case AnimState.InstallWasher2:
                animState = AnimState.Rotate2;
                proppelerAnimator.SetTrigger(animState.ToString());
                break;

            default:
                break;
        }
    }

    public void ResetAnimation()
    {
        runIndicatorText.text = "Run 1";
        foreach (var trigger in proppelerAnimator.parameters)
        {
            if(trigger.type == AnimatorControllerParameterType.Trigger)
            {
                proppelerAnimator.ResetTrigger(trigger.name);
            }
        }

        animState = AnimState.Idle;
        proppelerAnimator.SetTrigger(animState.ToString());
    }


    public enum AnimState
    {
        Idle = 0,
        InstallWasher1 = 1,
        Rotate1 = 2,
        InstallWasher2 = 3,
        Rotate2 = 4
    }
}
