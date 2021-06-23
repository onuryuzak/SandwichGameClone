using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotationManager : MonoBehaviour
{
    public static ObjectRotationManager Instance;

    [System.NonSerialized]
    public List<Move> History;


    [System.NonSerialized]
    public GameObject targetObject;

    [System.NonSerialized]
    public int moves;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        History = new List<Move>();
        moves = 0;
    }

    public void flip(GameObject selectedObject)
    {
        Vector3 targetObjectPos = targetObject.transform.position;
        List<GameObject> selectedObjects = LevelManager.Instance.GetObjectLine(selectedObject);
        var (x1, y1) = LevelManager.Instance.GetObjectLocation(selectedObject);
        var (x2, y2) = LevelManager.Instance.GetObjectLocation(targetObject);
        Move move = new Move(x1, y1, x2, y2);
        List<GameObject> reMove = new List<GameObject>();

        selectedObjects.Reverse();


        foreach (GameObject undoObject in selectedObjects)
        {
            moves++;
            reMove.Add(undoObject);
            move.AddToList(undoObject, undoObject.transform.position);
            undoObject.GetComponent<ObjectController>().MoveTo(targetObjectPos, false);
            targetObjectPos.y += targetObject.transform.localScale.y;
        }

        LevelManager.Instance.ObjectTransfer(reMove, x1, y1, x2, y2);
        targetObject = null;

        History.Add(move);
        UIManager.Instance.ActivateBackButton();
        GameManager.Instance.CheckWinConditions();
    }



    #region Undo Move
    public void BackMove()
    {
        Move move = History[History.Count - 1];
        History.RemoveAt(History.Count - 1);

        LevelManager.Instance.ObjectTransfer(move.Objects, move.newX, move.newY, move.oldX, move.oldY);
        for (int i = 0; i < move.Objects.Count; i++)
        {
            moves++;
            move.Objects[i].GetComponent<ObjectController>().MoveTo(move.Positions[i], true);
        }
    }




    public struct Move
    {
        public int oldX { get; }
        public int oldY { get; }
        public int newX { get; }
        public int newY { get; }
        public List<GameObject> Objects;
        public List<Vector3> Positions;

        public Move(int oldx, int oldy, int newx, int newy)
        {
            Objects = new List<GameObject>();
            Positions = new List<Vector3>();

            oldX = oldx;
            oldY = oldy;
            newX = newx;
            newY = newy;
        }

        public void AddToList(GameObject obj, Vector3 position)
        {
            Objects.Add(obj);
            Positions.Add(position);
        }
    }
    #endregion
}
