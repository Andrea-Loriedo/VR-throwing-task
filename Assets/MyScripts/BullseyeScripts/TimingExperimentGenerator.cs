using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;
using System;

public class TimingExperimentGenerator : MonoBehaviour {

public void Generate(Session session)
    {
        int numTrials = System.Convert.ToInt32(session.settings["trials_per_block"]);

        Dictionary<string, object> Block1Settings = (Dictionary<string, object>)session.settings["block_1_settings"];
        Dictionary<string, object> Block2Settings = (Dictionary<string, object>)session.settings["block_2_settings"];
        Dictionary<string, object> Block3Settings = (Dictionary<string, object>)session.settings["block_3_settings"];
        Dictionary<string, object> Block4Settings = (Dictionary<string, object>)session.settings["block_4_settings"];
        Dictionary<string, object> Block5Settings = (Dictionary<string, object>)session.settings["block_5_settings"];

        Block block1 = session.CreateBlock(numTrials);
        Block block2 = session.CreateBlock(numTrials);
        Block block3 = session.CreateBlock(numTrials);
        Block block4 = session.CreateBlock(numTrials);
        Block block5 = session.CreateBlock(numTrials);

        AssignBlockSettings(Block1Settings, block1);

        AssignBlockSettings(Block2Settings, block2);

        AssignBlockSettings(Block3Settings, block3);

        AssignBlockSettings(Block4Settings, block4);

        AssignBlockSettings(Block5Settings, block5);
    }

	void AssignBlockSettings(Dictionary<string, object> settings, Block block)
	{
		string speed = System.Convert.ToString(settings["speed"]);
		string mode = System.Convert.ToString(settings["mode"]); // can be random, vertical, horizontal
	
		block.settings["speed"] = speed;
		block.settings["mode"] = mode;
	}
}
