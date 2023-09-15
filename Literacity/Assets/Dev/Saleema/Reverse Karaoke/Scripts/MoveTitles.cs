using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTitles : MonoBehaviour

{
    public GameObject button;
    [SerializeField] private GameObject[] location;
    private int target;
    public GameObject title;
    // Start is called before the first frame update
    void Start()
    {
        button.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, location[target].transform.position, 500 * Time.deltaTime);
        //transform.position = Vector3.Lerp(transform.position, location[target].transform.position, 500 * Time.deltaTime);
        if (transform.position == location[target].transform.position)
        {
            target += 1;
            button.SetActive(true);
        }

    }
}