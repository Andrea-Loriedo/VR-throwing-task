using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Valve.VR.InteractionSystem.Sample
{
    public class DartsOptions : MonoBehaviour
    {
        public HoverButton hoverButton;

        public BullseyeController bullseye;

        enum options {Still, Move};

        private bool moveTarget;
        public TextMeshPro moveText;

        options gameMode;

        private void Start()
        {
            hoverButton.onButtonDown.AddListener(OnButtonDown);
            moveTarget = false;
            gameMode = options.Still;
        }

        void Update()
        {
            moveText.text = " " + ModeSelect();
            PlayAnimation();
        }

        private void OnButtonDown(Hand hand)
        {
            moveTarget = !moveTarget;
            ModeSelect();
        }

        options ModeSelect()
        {
            if(moveTarget)
            {
                gameMode = options.Move;
            }
            else if(!moveTarget)
            {
                gameMode = options.Still;
            }
            return gameMode;
        }

        void PlayAnimation()
        {
            switch(gameMode)
            {
                case options.Move:
                StartCoroutine(bullseye.MoveTarget());
                Debug.LogFormat("Moving!");
                break;
                case options.Still:
                bullseye.StopTarget();
                Debug.LogFormat("Stop!");
                break;
                default:
                break;
            }
        }
    }
}
