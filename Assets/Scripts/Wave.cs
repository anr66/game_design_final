using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveSegment
{
    public List<int> spawns = new List<int>();
    public int wait;

}


public class Wave : MonoBehaviour
{
    [TextArea(3, 10)]
    public string pattern;

    public WaveSegment[] PatternToWaveSegments()
    {
        // cut up the line patterns
        string[] lines = pattern.Split('\n');

        // create list to be returned
        List<WaveSegment> segments = new List<WaveSegment>();

        // loop through each line
        foreach (string line in lines)
        {
            // create new wave segment
            WaveSegment segment = new WaveSegment();

            // cut up the line
            string[] spawns = line.Split(' ');

            // loop through spawn numbers
            for (int i = 0; i < spawns.Length - 1; i++)
            {
                segment.spawns.Add(int.Parse(spawns[i]));
            }

            // add last number as wait time
            segment.wait = int.Parse(spawns[spawns.Length - 1]);

            // add sengemnt to the wave
            segments.Add(segment);
        }

        // were done, return segements
        return segments.ToArray();
    }

    
}
