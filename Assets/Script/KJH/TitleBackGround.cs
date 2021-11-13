using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBackGround : MonoBehaviour
{
    public float delay = 5f;
    public int _ran;
    float _timer = 0;
    Coroutine _co = null;

    private void Start()
    {

    }
    void Update()
    {
        _timer += Time.deltaTime;

        if(_timer > delay)
        {
            Debug.Log("delay On");
            _ran = Random.Range(0, 3);
            _timer = 0;
        }

        switch (_ran)
        {
            case 0:
                _co = StartCoroutine("Case0");
                break;
            case 1:
                _co = StartCoroutine("Case1");
                break;
            case 2:
                _co = StartCoroutine("Case2");
                break;
        }
    }
   IEnumerator Case0()
    {
        Debug.Log("Coroutine0");
        this.transform.position = new Vector3(0f, -2.3f, 0);
        yield return new WaitForSeconds(0.2f) ;
        this.transform.position = new Vector3(-0.4f, -2.3f, 0);

        _ran = -1;
        _co = null;
    }
    IEnumerator Case1()
    {
        Debug.Log("Coroutine1");
        this.transform.position = new Vector3(-0.4f, -1.9f, 0);
        yield return new WaitForSeconds(0.2f);
        this.transform.position = new Vector3(-0.4f, -2.3f, 0);

        _ran = -1;
        _co = null;
    }
    IEnumerator Case2()
    {
        Debug.Log("Coroutine2");
        this.GetComponent<SpriteRenderer>().flipX = false;
        yield return new WaitForSeconds(0.2f);
        this.GetComponent<SpriteRenderer>().flipX = true;

        _ran = -1;
        _co = null;
    }
}
