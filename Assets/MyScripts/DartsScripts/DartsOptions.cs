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
        private bool buttonPress;
        public TextMeshPro moveText;

        options gameMode;

        private void Start()
        {
            hoverButton.onButtonDown.AddListener(OnButtonDown);
            moveTarget = false;
            buttonPress = false;
            gameMode = options.Still;
            moveText.text = " " + ModeSelect(); // Button text initially set to "Still"
        }

        void Update()
        {
            if(buttonPress){
                moveText.text = " " + ModeSelect();
                buttonPress = false;
            }
            PlayAnimation();
        }

        private void OnButtonDown(Hand hand)
        {
            moveTarget = !moveTarget; // Toggle target mode
            buttonPress = true;
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
                break;
                case options.Still:
                bullseye.StopTarget();
                break;
                default:
                break;
            }
        }
    }
}
