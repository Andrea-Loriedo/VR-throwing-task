using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Valve.VR.InteractionSystem.Sample
{
    public class DartsOptions : MonoBehaviour
    {
        public HoverButton hoverButton;

        public GameObject bullseye;

        enum options {Still, Move};

        private bool moveTarget;
        public TextMeshPro moveText;

        options gameMode;

        private void Start()
        {
            hoverButton.onButtonDown.AddListener(OnButtonDown);
            moveTarget = false;
            moveText = GetComponent<TextMeshPro>() ?? gameObject.AddComponent<TextMeshPro>();
            gameMode = options.Still;
        }

        void Update()
        {
            moveText.text = " " + ModeSelect();
        }

        private void OnButtonDown(Hand hand)
        {
            moveTarget = !moveTarget;
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
    }
}
