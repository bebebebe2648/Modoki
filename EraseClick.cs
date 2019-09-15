using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EraseClick : MonoBehaviour
{
    GameSys GS;

    GameObject DB;

    Vector2 ColorP;

    bool CheckP1 = false;
    bool CheckP2 = false;
    bool CheckP3 = false;


    /*
     小ブロック・大ブロック生成完了
     小ブロック消しての落下処理完了
     小ブロック消去後、追加の小ブロック落下完了

     落下アニメーション完了

     追加の小ブロックがEraseListのみ参照してるため、大ブロックに非対応
     EraseList・ColorList使う必要あるのか問題（オブジェクトのIDで対応する？）
     =>EraseList削除・ColorListで対応
    
     大ブロックのサイズ・位置調整完了
     大ブロックの削除完了
     大ブロック含めた削除後の落下処理
     =>1つだけの大ブロックの場合、割とうまくいく
     =>右の小ブロック消してから、左の小ブロックを消す処理は割とうまくいくが、逆の手順だと落下位置がバグる
         
     落下位置の修正完了
     削除したブロックより上のブロックがおかしい
         
         
         
         
         
         
         
         
         
    */

    public void Start()
    {
        GS = GameObject.Find("GameSys").GetComponent<GameSys>();
    }

    //削除判定用
    GameObject DelB()
    {
        GameObject delB = null;

        ColorP = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D BoxB = Physics2D.OverlapPoint(ColorP);
        if (BoxB)
        {
            delB = BoxB.transform.gameObject;
            delB.transform.position = ColorP;
            Debug.Log("TEST2");
        }

        return delB;
    }

    public void Clicked()
    {
        Debug.Log("TEST");

        //削除判定用
        DB = DelB();

        //大ブロック削除
        for (int A = 0; A < GS.BigList.Count; A++)
        {
            if (DB.transform.position == GS.BigList[A].BigB.transform.position)
            {
                for (int j = 6; j >= 0; j--)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        //クリックしたオブジェクトと一致する場合
                        if (GS.BigList[A].BPos.x == GS.BPosX[i, j] && GS.BigList[A].BPos.y == GS.BPosY[i, j])
                        {
                            Debug.Log("TEST3");

                            //消したブロックの座標を格納
                            GS.EraseBPos = new Vector2(GS.BigList[A].BPos.x, GS.BigList[A].BPos.y);

                            GS.ColorList[i, j] = 0;
                            GS.ColorList[i + 1, j] = 0;
                            GS.ColorList[i, j + 1] = 0;
                            GS.ColorList[i + 1, j + 1] = 0;

                            //消したブロックのX軸・Y軸を格納
                            GS.EraseX = i;
                            GS.EraseY = j;

                            //オブジェクトの破壊
                            DestroyImmediate(GS.BigList[A].BigB);
                            //リストから削除
                            GS.BigList.RemoveAt(A);

                            CheckP2 = true;
                            CheckP3 = true;
                            break;
                        }
                    }

                    if (CheckP2 == true)
                    {
                        break;
                    }
                }

                if (CheckP2 == true)
                {
                    CheckP2 = false;
                    break;
                }
            }
        }

        if (CheckP3 != true)
        {
            //小ブロック削除
            for (int C = 0; C < GS.SmallList.Count; C++)
            {
                //小ブロックであるか
                if (DB.transform.position == GS.SmallList[C].SmallB.transform.position)
                {
                    for (int j = 7; j >= 0; j--)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            //クリックしたオブジェクトと一致する場合
                            if (GS.SmallList[C].SPos.x == GS.SPosX[i, j] && GS.SmallList[C].SPos.y == GS.SPosY[i, j])
                            {
                                Debug.Log("TEST4");

                                //消したブロック（落下位置）の座標を格納
                                GS.EraseSPos = new Vector2(GS.SPosX[i, j], GS.SPosY[i, j]);
                                GS.EraseBPos = new Vector2(GS.BPosX[i, j], GS.BPosY[i, j]);

                                Debug.Log("!!同じ x: " + GS.EraseSPos.x + " / y: " + GS.EraseSPos.y + " ?? x: " + GS.EraseBPos.x + " / y: " + GS.EraseBPos.y);
                                Debug.Log("i: " + i + " / j: " + j);

                                GS.ColorList[i, j] = 0;
                                Debug.Log("消した場所の色: " + GS.ColorList[i, j]);

                                //消したブロックのX軸・Y軸を格納
                                GS.EraseX = i;
                                //GS.EraseY = j;

                                //オブジェクトの破壊
                                DestroyImmediate(GS.SmallList[C].SmallB);
                                //リストから削除
                                GS.SmallList.RemoveAt(C);

                                CheckP1 = true;
                                break;
                            }
                        }

                        if (CheckP1 == true)
                        {
                            break;
                        }
                    }

                    if (CheckP1 == true)
                    {
                        CheckP1 = false;
                        break;
                    }
                }
            }
        }

        CheckP3 = false;
    }
}