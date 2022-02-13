using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Cinemachine;

public class PlayerScript : MonoBehaviourPunCallbacks, IPunObservable
{
    public Rigidbody2D RB;
    public Animator AN;
    public SpriteRenderer SR;
    public PhotonView PV;
    public Text NickNameText;



    public bool isGround;
    public bool isRun;
    public bool isUnder = false;
    public bool isDie = false;
    public bool IsRound2_Trigger = false;
    public bool IsRound2_Trigger2 = false;

    Vector3 curPos;
    bool stream_isDie;
    float axis;
    public AudioSource mysfx;
    public AudioClip jumpsfx;
    int round;

    //Stream 받아올 임시변수
    bool get_isRun;
    bool get_isGround;
    bool get_isUnder;

    void Awake()
    {

        // 닉네임 표시
        NickNameText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;
        round = R_NetWorkManager.round;

        //자기 플레이어를 따라다니는 카메라 설정
        //CM은 시네머신 카메라 변수
        //에러남 보류 라운드 7일때 제외
        if (PV.IsMine && round != 7)
        {
            var CM = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
            CM.Follow = transform;
            CM.LookAt = transform;
        }
    }

    void Update()
    {
        /*캐릭터 사망여부 동기화*/

        /*캐릭터 포지션 동기화*/
        if (PV.IsMine)
        {
            /*죽었을 때 애니메이션 종료, 속도 0*/
            if (isDie && isGround)
            {
                AN.SetBool("isRun", false);
                AN.SetBool("isJump", false);
                Vector2 Direction = Vector2.zero;
                RB.velocity = Direction;
            }

            if (!isDie)
            {
                axis = Input.GetAxisRaw("Horizontal");
                RB.velocity = new Vector2(4 * axis, RB.velocity.y);
            }

            if (axis != 0 && !isDie)
            {
                isRun = true;
                AN.SetBool("isRun", true);
                PV.RPC("FilpXRPC", RpcTarget.AllBuffered, axis); // 재접속시 filpX를 동기화해주기 위해서 AllBuffered
            }
            else
            {
                AN.SetBool("isRun", false);
                isRun = false;
            }


            // 점프, 바닥체크
            isGround = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(0, -0.5f), 0.07f, 1 << LayerMask.NameToLayer("Ground"));

            AN.SetBool("isJump", !isGround);


            if (Input.GetKeyDown(KeyCode.Space) && isGround && !isDie)
            {

                PV.RPC("JumpRPC", RpcTarget.All);
                JumpSound();

            }

            if(round == 7)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                    isUnder = true;
                if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
                    isUnder = false;

            }


        }

        //IsMine이 아닌 것들은 부드럽게 위치 동기화
        else if ((transform.position - curPos).sqrMagnitude >= 100) transform.position = curPos;
        else
        {
            transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);
            isGround = get_isGround;
            isRun = get_isRun;
            isUnder = get_isUnder;
        }
    }


    [PunRPC]
    void FilpXRPC(float axis)
    {
        SR.flipX = axis == -1;
    }// 왼쪽 키를 누를 경우 True 반환 오른쪽 키를 누르는 경우 False 반환

    [PunRPC]
    void JumpRPC()
    {
        RB.velocity = Vector2.zero;
        RB.AddForce(Vector2.up * 700);    
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);

            if (round == 7)
            {
                stream.SendNext(isRun);
                stream.SendNext(isGround);
                stream.SendNext(isUnder);
            }
            
        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();

            if (round == 7)
            {
                get_isRun = (bool)stream.ReceiveNext();
                get_isGround = (bool)stream.ReceiveNext();
                get_isUnder = (bool)stream.ReceiveNext();
            }
        }
    }

    public void JumpSound()
    {
        mysfx.PlayOneShot(jumpsfx);
    }
}