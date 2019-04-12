using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Valve.VR.InteractionSystem;
using UXF;

namespace Valve.VR.InteractionSystem.Sample
{
    public class ScoreboardController : MonoBehaviour
    {
        private TextMeshPro scoreText;
        public TextMeshPro dartsLeftText;
        public BullseyeController score;
        public DartControl dart;

        //UXF 
        public Session session;
        public ExperimentManager experiment;
    
        void Awake()
        {
            // Get a reference to an existing TextMeshPro component or Add one if needed.
            scoreText = GetComponent<TextMeshPro>() ?? gameObject.AddComponent<TextMeshPro>();
    
            // Set the text
            scoreText.text = "Score: 0";
        }

        void Update()
        {
            if(experiment.NewBlockBegin() && session.currentTrial.status != UXF.TrialStatus.Done)
            {
                dart.SetDartsLeft(session.settings.GetInt("trials_per_block"));
            }
            scoreText.text = "Score: " + score.GetScore();
            dartsLeftText.text = "Darts remaining: " + dart.GetDartsLeft();
        }
    }
}
