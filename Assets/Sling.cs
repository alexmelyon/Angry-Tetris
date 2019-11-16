using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sling : MonoBehaviour
{
    public GameObject origin;
    public GameObject arrow;
    public float force = 10;
    public GameObject figure;
    public GameObject[] figures;
    


    void Start()
    {
        if(figure == null) {
            CreateFigure();
        }
    }

    void Update()
    {
        if(Input.GetMouseButton(0)) {
            handleClick(Input.mousePosition);
        }
        if(Input.GetMouseButtonUp(0)) {
            handleRelease(Input.mousePosition);
        }
        foreach(Touch t in Input.touches) {
            if(t.phase == TouchPhase.Began || t.phase == TouchPhase.Moved) {
                handleClick(t.position);
            } else if(t.phase == TouchPhase.Ended) {
                handleRelease(t.position);
            }
        }
    }
    void handleClick(Vector3 pos) {
        Vector3 dir = getDir(pos);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    Vector3 getDir(Vector3 pos) {
        Vector3 originPos = origin.transform.position;
        Vector3 world = Camera.main.ScreenToWorldPoint(pos);
        Vector3 dir = world - originPos;
        dir = -dir * 2;
        return dir;
    }

    void handleRelease(Vector3 pos) {
        Rigidbody2D rigid = figure.GetComponent<Rigidbody2D>();
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        
        Vector3 dir = getDir(pos);
        rigid.AddForce(dir * force, ForceMode2D.Impulse);
        
        CreateFigure();
    }

    void CreateFigure() {
        StartCoroutine(InstantiateCoroutine());
    }

    IEnumerator InstantiateCoroutine() {
        yield return new WaitForSeconds(0.5F);
        figure = Instantiate(figures[Random.Range(0, 5)], origin.transform.position, Quaternion.identity);
        figure.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        yield break;
    }
}
