using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public static ObjectController Instance;

   

    public bool CanMove;
    private Vector3 CurrentPosition;
    private Quaternion CurrentRotation;

    


    public float JumpHeight = .6f;
    public float Duration = .7f;
    

    #region Singleton
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    #endregion

    private enum MOVEMENTS{
        top,
        bottom,
        left,
        right
    }

    private void Start() {
        CanMove = true;
        CurrentPosition = transform.position;
        CurrentRotation = transform.rotation;
    }

   
    public void MoveTo(Vector3 target, bool isBackMove){
        if(CanMove){
            if(target.z < transform.position.z){
                StartCoroutine(FlipObject(Duration, target, MOVEMENTS.bottom, isBackMove));
            }
            else if(target.z> transform.position.z){
                StartCoroutine(FlipObject(Duration, target, MOVEMENTS.top, isBackMove));
            }
            else{
                if(target.x< transform.position.x){
                    StartCoroutine(FlipObject(Duration, target, MOVEMENTS.left, isBackMove));
                }
                else if(target.x> transform.position.x){
                    StartCoroutine(FlipObject(Duration, target, MOVEMENTS.right, isBackMove));
                }
            }
        }
    }

    IEnumerator FlipObject(float duration, Vector3 moveTarget, MOVEMENTS rotation, bool isBackMove){
        CanMove = false;
        float progress = 0f;
        Quaternion endRotation;

        switch(rotation){
            case MOVEMENTS.top:
                endRotation = Quaternion.Euler(-180,0,0);
                break;
            case MOVEMENTS.bottom:
                endRotation = Quaternion.Euler(180,0,0);
                break;
            case MOVEMENTS.right:
                endRotation = Quaternion.Euler(0,0,180);
                break;
            case MOVEMENTS.left:
                endRotation = Quaternion.Euler(0,0,-180);
                break;
            default:
                endRotation = Quaternion.Euler(0,0,0);
                break;
        }


        var targetPosition = moveTarget + (isBackMove?new Vector3(0,0,0):new Vector3(0,transform.localScale.y,0));//new Vector3(0,transform.position.y,0);

        while(progress <duration){
            progress += Time.deltaTime;
            var percent = Mathf.Clamp01(progress/duration);
            float height = (JumpHeight)*Mathf.Sin(Mathf.PI*percent);

            transform.position = Vector3.Lerp(CurrentPosition,targetPosition,percent)+new Vector3(0,height,0);
            transform.rotation = Quaternion.Lerp(CurrentRotation,endRotation,percent);
            yield return null;
        }

        
        
        CurrentPosition = new Vector3(transform.position.x, transform.position.y + moveTarget.y,transform.position.z);
        CanMove = true;
        ObjectRotationManager.Instance.moves--;
    }

   

    
    

}
