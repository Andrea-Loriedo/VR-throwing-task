using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UXF;

namespace Valve.VR.InteractionSystem.Sample
{
    public class DartsOptions : MonoBehaviour
    {
        public HoverButton hoverButton;
        public BullseyeController bullseye;
        public TextMeshPro moveText;
        public ExperimentManager experiment;
        public Transform player;
        public Transform closeDistance;
        public Transform FarDistance;
        public Transform workspace;

        // UXF
        public Session session;

        private bool moveTarget;
        private bool buttonPress;

        enum options {Still, Move};
        options gameMode;

        private void Start()
        {
            hoverButton.onButtonDown.AddListener(OnButtonDown);
            moveTarget = false;
            buttonPress = false;
            gameMode = options.Still;
            moveText.text = " " + ModeSelect(); // Button text initially set to "Still"
            MovePlayer(closeDistance);
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

        public void ApplyBlockSettings(ExperimentManager.BlockSettings settings)
        {
            if(settings.distance == "Close" && settings.targetMode == "Still")
            {
                MovePlayer(closeDistance);
                gameMode = options.Still;
            }
            else if(settings.distance == "Close" && settings.targetMode == "Move")
            {
                MovePlayer(closeDistance);
                gameMode = options.Move;
            }
            else if(settings.distance == "Far" && settings.targetMode == "Still")
            {
                MovePlayer(FarDistance);
                gameMode = options.Still;
            }
            else if(settings.distance == "Far" && settings.targetMode == "Move")
            {
                MovePlayer(FarDistance);
                gameMode = options.Move;
            }
        }

        void MovePlayer(Transform spawnPlayer)
        {
            player.localPosition = spawnPlayer.localPosition; 
            workspace.localPosition = spawnPlayer.localPosition + new Vector3(0, 0.001f, 0);
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
