using Mole.Halt.ApplicationLayer;
using Mole.Halt.PresentationLayer.Models;
using UnityEngine;

public abstract class MenuScreen : MonoBehaviour, Initializable
{
    abstract public ScreenId ScreenType { get; }

    abstract public void Init();
    abstract public void Deinit();

    public virtual void Enter()
    {
        gameObject.SetActive(true);
    }

    public virtual void Leave()
    {
        gameObject.SetActive(false);
    }

}
