using UnityEngine;
using System;
using System.Linq;

public class Band : MonoBehaviour
{
    BeatCounter counter = new BeatCounter();

    public int BeatsPerBar = 4;
    public int BeatsPerMinute = 60;
    public float BeatSync = 0.25f;

    public int BeatCount;
    public double BeatProgress;
    public int BarCount;
    public double BarProgress;

    private double lastBeat;
    private double nextBeat;

    private Instrument[] instruments = new Instrument[Enum.GetValues(typeof(InstrumentType)).OfType<InstrumentType>().Cast<int>().Max()];

	public void Start()
    {
	    for(int i = 0; i < instruments.Length; i++)
        {
            instruments[i] = new Instrument((InstrumentType)i);
        }
        counter.Start();
	}
	
	public void Update()
    {
        counter.Update();
        for(int i = 0; i < instruments.Length; i++)
        {
            instruments[i].Update(counter, TimeSpan.FromMinutes(BeatSync / BeatsPerMinute).TotalSeconds);
        }
	}

    public void AddMessage(string message)
    {
        AddMessage(Message.FromString(message));
    }
    public void AddMessage(Message message)
    {
        instruments[(int)message.Instrument].AddMessage(message);
    }
}
