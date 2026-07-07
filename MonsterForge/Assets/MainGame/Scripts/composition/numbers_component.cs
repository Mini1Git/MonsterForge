using System.Collections;
using System.Threading;
using UnityEngine;

public class numbers_component : MonoBehaviour
{
    Coroutine running;
    
    public float duration = 2f;
    public void Update()
    {
        
        if (running == null)
        {
            running = StartCoroutine(float_numbers());
        }
    }
    IEnumerator float_numbers (){
        float timer = 0;
        while (timer < duration)
        {
            float randomNum = Random.Range(-1f,1f);
            print(randomNum);
            gameObject.transform.position = new Vector2(transform.position.x + 2*randomNum * Time.deltaTime, transform.position.y + 2 * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
            
            
        }

        running = null;

    }
}
