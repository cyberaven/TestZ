using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{
    //нужен некий префаб контейнер.
    [SerializeField] private GameObject PlayerPrefab;
    [SerializeField] private GameObject FinishPrefab;
    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private GameObject RoadPrefab;
    
    private Player Player;
    private Finish Finish;
    private List<Enemy> Enemys = new List<Enemy>();
    private int EnemyCount = 20;
    private Road Road;
   

    private Vector3 CameraStartPosition = new Vector3(-1.92f, 3.23f, -0.06f);

   
    private void OnEnable()
    {
        StartButton.StartButtonClickEvent += StartGame;
        Player.FinishTouchEvent += WinGame;
        Player.EnemyTouchEvent += LoseGame;
    }
    private void OnDisable()
    {
        StartButton.StartButtonClickEvent -= StartGame;
        Player.FinishTouchEvent -= WinGame;
        Player.EnemyTouchEvent -= LoseGame;
    }

    private void StartGame()
    {
        CreateGameEntite();
        Camera.main.transform.SetParent(Player.transform);
        Camera.main.transform.localPosition = CameraStartPosition;               
    }
    private void WinGame()
    {
        Debug.Log("WinGame");
        DestroyGameEntite();
    }
    private void LoseGame()
    {
        Debug.Log("LoseGame");
        DestroyGameEntite();
    }

    private void CreateGameEntite() //каждому нужна фабрика и пул.
    {
        Player = Instantiate(PlayerPrefab).GetComponent<Player>();
        Finish = Instantiate(FinishPrefab).GetComponent<Finish>();
        CreateEnemys(EnemyCount);        
        Road = Instantiate(RoadPrefab).GetComponent<Road>();
    }
    private void CreateEnemys(int count)
    {
        Enemys = new List<Enemy>();

        float maxX = 60;//граница случайных координат. Взята на глаз.
        float minX = 20;
        float maxY = 0.5f;
        float minY = 0;
        float maxZ = 2;
        float minZ = -2;

        for (int i = 0; i < count; i++)
        {
            Enemy enemy = Instantiate(EnemyPrefab).GetComponent<Enemy>();//прощай производительность            
            
            Vector3 randomPos = new Vector3(Random.Range(minX,maxX),Random.Range(minY,maxY),Random.Range(minZ,maxZ));
            enemy.transform.position = randomPos;

            Enemys.Add(enemy);
        }
    }
    private void DestroyGameEntite()//объекты надо не уничтожать, а помещать в пул.
    {
        Camera.main.transform.SetParent(null);
        Destroy(Player.gameObject);

        Destroy(Finish.gameObject);        
        Destroy(Road.gameObject);

        foreach (Enemy enemy in Enemys)
        {
            if (enemy)//костыль/заплатка
            {
                Destroy(enemy.gameObject);
            }
        }
    }

}
