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
        // private TextContainer m_TextContainer;
        public BullseyeController score;
        public Session session;
        
        public DartControl dart;
    
        void Awake()
        {
            // Get a reference to an existing TextMeshPro component or Add one if needed.
            scoreText = GetComponent<TextMeshPro>() ?? gameObject.AddComponent<TextMeshPro>();
    
            // Set the text
            scoreText.text = "Score: 0";
        }

        void Update()
        {
            scoreText.text = "Score: " + score.GetScore();
            dartsLeftText.text = "Darts remaining: " + dart.GetDartsLeft();
        }
    }
}
