using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject[] objects;
    public GameObject[] spawnPoints;
    public GameObject finalUI;
    public TMP_Text scoreText;
    public TMP_Text finalText;
    public float timer;
    private int score;

    void Start()
    {
        StartCoroutine(Timer());
        StartCoroutine(SpawnObject());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(clickPosition);

            if (hitCollider != null && hitCollider.CompareTag("Ball"))
            {
                score++;
                scoreText.text = score.ToString();
                Destroy(hitCollider.gameObject);
            }
        }
    }

    private IEnumerator SpawnObject()
    {
        if (!finalUI.activeSelf)
        {
            List<GameObject> newObjects = new List<GameObject>();
            yield return new WaitForSeconds(0.7f);
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(0.1f);
                GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                newObjects.Add(Instantiate(objects[Random.Range(0, objects.Length)], new Vector3(0, 0, 0), Quaternion.identity));
                Rigidbody2D newObjectRigidbody = newObjects[i].GetComponent<Rigidbody2D>();
                newObjects[i].transform.position = spawnPoint.transform.position;
                newObjectRigidbody.AddForce(newObjects[i].transform.position * -55);
                if (newObjects[i].transform.position.y >= 9) { newObjectRigidbody.velocity = new Vector2(newObjectRigidbody.velocity.x, 10); }
            }
            StartCoroutine(SpawnObject());
            yield return new WaitForSeconds(4f);
            for (int i = 0; i < 3; i++)
            {
                if (newObjects[i] != null)
                {
                    Destroy(newObjects[i]);
                }
            }
        }
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(timer);
        finalUI.SetActive(true);
        finalText.text = "You managed to burst ... balloons – " + score.ToString() + "\n\nWould you like to do it again?";
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
