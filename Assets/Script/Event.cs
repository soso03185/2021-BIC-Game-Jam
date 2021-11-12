using UnityEngine;
using UnityEngine.Events;

public class Event : MonoBehaviour
{
    public bool isActivated { get; set; } = false;

    public int index = 0;

    [SerializeField] private UnityEvent OnActivate;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(OnActivate != null)
        {
            OnActivate.Invoke();
            this.isActivated = true;

            Stage.currentActivatedTriggers.Add(this.index);
        }
        
    }

}
