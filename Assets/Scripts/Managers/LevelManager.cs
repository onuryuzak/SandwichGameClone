using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    Vector3 StartPos = new Vector3(-1,0.2f,1);
    Vector3 IngrediantSize = new Vector3(1,0.2f,1);
    [SerializeField]
    public GameObject[] Objects = new GameObject[9];
    public List<GameObject>[,] GridBoard = new List<GameObject>[3,3];


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start() {
        StartPos.y = IngrediantSize.y/2;

        Vector3 pos = StartPos;

        for(int x=0; x< GridBoard.GetLength(0); x++){
            for(int y=0; y< GridBoard.GetLength(1); y++){
                GridBoard[x,y] = new List<GameObject>();

                if(Objects[x+y*3] != null){
                    GameObject obj = Instantiate(Objects[x+y*3],pos,Quaternion.identity);
                    obj.transform.SetParent(transform);
                    GridBoard[x,y].Add(obj);
                }

                pos += new Vector3(0, 0, -IngrediantSize.z);
            }
            pos = new Vector3(pos.x,pos.y,StartPos.z);
            pos += new Vector3(IngrediantSize.x, 0, 0);
        }
    }

    public (int,int) GetObjectLocation(GameObject obj){

        for(int x=0; x< GridBoard.GetLength(0); x++){
            for(int y=0; y< GridBoard.GetLength(1); y++){
                if(GridBoard[x,y].Contains(obj)){
                    return (x,y);
                }
            }
        }
        return (0,0);
    }

    public List<GameObject> GetObjectLine(GameObject obj){
        var (x,y) = GetObjectLocation(obj);
        return GridBoard[x,y];
    }

    public void ObjectTransfer(List<GameObject> objs, int oldx, int oldy, int newx, int newy){
        foreach(GameObject obj in objs){
            GridBoard[oldx,oldy].Remove(obj);
            GridBoard[newx,newy].Add(obj);
        }
    }

    public (int,List<GameObject>) EmptyBoxCount(){
        int counter = 0;
        List<GameObject> finalLine = null;
        for(int x=0; x< GridBoard.GetLength(0); x++){
            for(int y=0; y< GridBoard.GetLength(1); y++){
                if(GridBoard[x,y].Count>0){
                    counter++;
                    finalLine = GridBoard[x,y];
                }
            }
        }
        if(counter==1){
            return (counter,finalLine);
        }
        else{
            return (counter,null);
        }
    }
}
