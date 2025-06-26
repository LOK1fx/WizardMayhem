using System.Linq;
using UnityEngine;

public interface ISpell
{
    ESpellId Id { get; }
    void StartExecution(PlayerCharacter sender);
    void StopExecution();
    void OnUpdate();
    void OnFixedUpdate();
}

public abstract class SpellBase<T> : ISpell where T : SpellData
{
    private T _data;
    
    public T Data {
        get
        {
            if (_data == null)
            {
                _data = Resources.LoadAll<T>("Data")
                        .Where(s => s.SpellId == Id)
                        .FirstOrDefault();
            }
            
            return _data;
        }
        private set
        {
            _data = value;
        }
    }

    public abstract ESpellId Id { get; }

    public abstract void StartExecution(PlayerCharacter sender);
    public abstract void StopExecution();
    public abstract void OnUpdate();
    public virtual void OnFixedUpdate() { }
}
