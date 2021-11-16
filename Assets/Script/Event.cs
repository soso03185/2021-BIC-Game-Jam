using UnityEngine;
using UnityEngine.Events;

public class Event : MonoBehaviour
{
    public bool isActivated { get; set; } = false;
    public float delay { get; set; } = 0f;

    public int index = 99;

    [SerializeField] private UnityEvent OnActivate;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(OnActivate != null)
        {
            OnActivate.Invoke();
            this.isActivated = true;

            if (gameObject.CompareTag("Event") == true)
            Stage.currentActivatedTriggers.Add(this.index);

            gameObject.SetActive(false);
        }
        
    }

}
