using UnityEngine;

public abstract class GameGoal : ScriptableObject
{
    public string Name = "Goal";
    public string Description = "Get points!";
    public SoundEffect EntrySoundEffect;
    public Color Color = Color.white;

    public virtual void OnEnter()
    {
        Debug.Log($"Subscribe {Name} events!");
    }
    public virtual void OnExit()
    {
        Debug.Log($"Unsubscribe {Name} events!");
    }
}
