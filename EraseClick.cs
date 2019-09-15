using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EraseClick : MonoBehaviour
{
    GameObject DB;

    Vector2 ColorP;

    public static Vector2 EraseSPos;
    public static Vector2 EraseBPos;
    public static Vector2 MoveP;

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
         
         
         
         
         
         
         
         
         
         
         
    */
    
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
        for (int A = 0; A < GameSys.BigList.Count; A++)
        {
            if (DB.transform.position == GameSys.BigList[A].BigB.transform.position)
            {
                for (int j = 6; j >= 0; j--)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        //クリックしたオブジェクトと一致する場合
                        if (GameSys.BigList[A].BPos.x == GameSys.BPosX[i, j] && GameSys.BigList[A].BPos.y == GameSys.BPosY[i, j])
                        {
                            Debug.Log("TEST3");

                            //消したブロックの座標を格納
                            EraseBPos = new Vector2(GameSys.BigList[A].BPos.x, GameSys.BigList[A].BPos.y);

                            GameSys.ColorList[i, j] = 0;
                            GameSys.ColorList[i + 1, j] = 0;
                            GameSys.ColorList[i, j + 1] = 0;
                            GameSys.ColorList[i + 1, j + 1] = 0;

                            //オブジェクトの破壊
                            DestroyImmediate(GameSys.BigList[A].BigB);
                            //リストから削除
                            GameSys.BigList.RemoveAt(A);
                            
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
            for (int C = 0; C < GameSys.SmallList.Count; C++)
            {
                //小ブロックであるか
                if (DB.transform.position == GameSys.SmallList[C].SmallB.transform.position)
                {
                    for (int j = 7; j >= 0; j--)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            //クリックしたオブジェクトと一致する場合
                            if (GameSys.SmallList[C].SPos.x == GameSys.SPosX[i, j] && GameSys.SmallList[C].SPos.y == GameSys.SPosY[i, j])
                            {
                                //消したブロック（落下位置）の座標を格納
                                EraseSPos = new Vector2(GameSys.SPosX[i, j], GameSys.SPosY[i, j]);
                                EraseBPos = new Vector2(GameSys.BPosX[i, j], GameSys.BPosY[i, j]);

                                Debug.Log("!!同じ x: " + EraseSPos.x + " / y: " + EraseSPos.y + " ?? x: " + EraseBPos.x + " / y: " + EraseBPos.y);
                                Debug.Log("i: " + i + " / j: " + j);

                                GameSys.ColorList[i, j] = 0;
                                Debug.Log("消した場所の色: " + GameSys.ColorList[i, j]);
                                //オブジェクトの破壊
                                DestroyImmediate(GameSys.SmallList[C].SmallB);
                                //リストから削除
                                GameSys.SmallList.RemoveAt(C);
                                
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
    /*
    public void Move()
    {
        for(int y = 7; y > 0; y--)
        {
            for(int x = 0; x < 8; x++)
            {
                //落下座標
                if (GameSys.ColorList[x, y] == 0)
                {
                    //消したブロックの1個上を比較対象に
                    for (int Y = y - 1; Y >= 0; Y--)
                    {
                        Debug.Log("Color[x, Y]: " + GameSys.ColorList[x, Y]);

                        //消したブロックの上のブロックがあるか
                        if (GameSys.ColorList[x, Y] != 0)
                        {
                            Debug.Log("x: " + x + " / Y: " + Y);

                            //小ブロックの場合
                            if (GameSys.ColorList[x, Y] == 1 || GameSys.ColorList[x, Y] == 2 || GameSys.ColorList[x, Y] == 3 ||
                                GameSys.ColorList[x, Y] == 7)
                            {
                                //データ上の移動
                                GameSys.ColorList[x, Y + 1] = GameSys.ColorList[x, Y];
                                GameSys.ColorList[x, Y] = 0;

                                for (int D = 0; D < GameSys.SmallList.Count; D++)
                                {
                                    //落下するブロックを決定
                                    if (GameSys.SmallList[D].SPos.x == EraseSPos.x && GameSys.SmallList[D].SPos.y == EraseSPos.y + 50)
                                    {
                                        Debug.Log("Report2 同じ X:" + GameSys.SmallList[D].SPos.x + " / Y: " + GameSys.SmallList[D].SPos.y);
                                        Debug.Log("Report3 X:" + GameSys.SPosX[x, Y + 1] + " / Y: " + GameSys.SPosY[x, Y + 1]);
                                        GameSys.SmallList[D].SPos = new Vector2(GameSys.SPosX[x, Y + 1], GameSys.SPosY[x, Y + 1]);
                                        //GameSys.SmallList[D].SPos = new Vector2(GameSys.SPosX[x, Y + 1], GameSys.SPosY[x, Y + 1]);

                                        //Vector2.Lerp(始点,終点,0.0f～1.0f);
                                        //Debug.Log("Time: " + GameSys.tmpTime + " / EraseSPos: " + EraseSPos);
                                        MoveP = Vector2.Lerp(GameSys.SmallList[D].SPos, EraseSPos, GameSys.tmpTime);

                                        //Debug.Log("MoveP" + MoveP);
                                        GameSys.SmallList[D].SmallB.transform.localPosition = new Vector2(MoveP.x, MoveP.y);
                                        //Debug.Log("Report3: " + GameSys.SmallList[D].SmallB.transform.localPosition);

                                        //GameSys.SmallList[D].SmallB.transform.localPosition = new Vector2(GameSys.SmallList[D].SPos.x, GameSys.SmallList[D].SPos.y);
                                        GameSys.SmallList[D].SmallB.transform.SetParent(GS.Panel.transform, false);

                                        //Debug.Log("Report4 X:" + GameSys.SmallList[D].SmallB.transform.position.x + " / Y: " + GameSys.SmallList[D].SmallB.transform.position.y);
                                        Debug.Log("Report5 X:" + GameSys.SmallList[D].SPos.x + " / Y: " + GameSys.SmallList[D].SPos.y);
                                        //Debug.Log("Report6: " + GameSys.SmallList[D].SmallB.transform.localPosition);

                                        EraseSPos.y = EraseSPos.y + 50;
                                        break;
                                    }
                                }
                            }

                            //大ブロックの場合
                            else if (GameSys.ColorList[x, Y] == 4 || GameSys.ColorList[x, Y] == 5 || GameSys.ColorList[x, Y] == 6)
                            {
                                Debug.Log("TESTBIG!!");
                                Debug.Log("Color[x + 1, Y + 1]: " + GameSys.ColorList[x + 1, Y + 1]);

                                //落下できるか
                                if (GameSys.ColorList[x + 1, Y + 1] == 0)
                                {
                                    Debug.Log("TESTBIG!!!!");

                                    //4つの色が同じ落下判断処理へ
                                    if (GameSys.ColorList[x, Y] == GameSys.ColorList[x + 1, Y] &&
                                        GameSys.ColorList[x, Y - 1] == GameSys.ColorList[x + 1, Y - 1])
                                    {
                                        //落下できるならば、大ブロックを検索
                                        for (int B = 0; B < GameSys.BigList.Count; B++)
                                        {
                                            Debug.Log("TESTBIG!!!!!!");
                                            
                                            //リストの中身が1つしか大ブロックがない場合
                                            if(GameSys.BigList.Count == 1)
                                            {
                                                //
                                                GameSys.ColorList[x, Y + 1] = GameSys.ColorList[x, Y];
                                                GameSys.ColorList[x + 1, Y + 1] = GameSys.ColorList[x + 1, Y];

                                                GameSys.ColorList[x, Y - 1] = 0;
                                                GameSys.ColorList[x + 1, Y - 1] = 0;

                                                Debug.Log("BIGONE!!");

                                                //上の座標に下の座標を入れる
                                                GameSys.BigList[B].BPos = new Vector2(GameSys.BPosX[x, Y], GameSys.BPosY[x, Y]);

                                                EraseBPos = new Vector2(EraseBPos.x, EraseBPos.y + 50);
                                                Debug.Log("EraseBPos: " + EraseBPos);

                                                //Vector2.Lerp(始点,終点,0.0f～1.0f);
                                                MoveP = Vector2.Lerp(GameSys.BigList[B].BPos, EraseBPos, GameSys.tmpTime);
                                                GameSys.BigList[B].BigB.transform.localPosition = new Vector2(MoveP.x, MoveP.y);

                                                GameSys.BigList[B].BigB.transform.SetParent(GS.Panel.transform, false);

                                                EraseBPos.y = EraseBPos.y + 100;
                                                CheckP5 = true;
                                                break;
                                            }

                                            //リストの中身が2つ以上、大ブロックがある場合
                                            //落下しない
                                            Debug.Log("B X: " + GameSys.BigList[B].BPos.x + " / Y: " + GameSys.BigList[B].BPos.y +
                                                      " / BPosX: " + GameSys.BPosX[x + 1, Y - 1] + " / BPosY: " + GameSys.BPosY[x + 1, Y - 1]);

                                            if (GameSys.BigList[B].BPos.x == GameSys.BPosX[x + 1, Y - 1] && GameSys.BigList[B].BPos.y == GameSys.BPosY[x + 1, Y - 1])
                                            {
                                                CheckP5 = true;
                                                break;
                                            }
                                        }

                                        if (CheckP5 == true)
                                        {
                                            CheckP5 = false;
                                            break;
                                        }

                                        else
                                        {
                                            for(int E = 0; E < GameSys.BigList.Count; E++)
                                            {
                                                Debug.Log("BIG!!");
                                                Debug.Log("E X: " + GameSys.BigList[E].BPos.x + " / Y: " + GameSys.BigList[E].BPos.y +
                                                          " / BPosX: " + GameSys.BPosX[x, Y - 1] + " / BPosY: " + GameSys.BPosY[x, Y - 1]);
                                                Debug.Log("EraseBPos: " + EraseBPos);

                                                if (GameSys.BigList[E].BPos.x == EraseBPos.x && GameSys.BigList[E].BPos.y == EraseBPos.y + 100)
                                                //if (GameSys.BigList[E].BPos.x == GameSys.BPosX[x, Y - 1] && GameSys.BigList[E].BPos.y == GameSys.BPosY[x, Y - 1])
                                                {
                                                    //
                                                    GameSys.ColorList[x, Y + 1] = GameSys.ColorList[x, Y];
                                                    GameSys.ColorList[x + 1, Y + 1] = GameSys.ColorList[x + 1, Y];

                                                    GameSys.ColorList[x, Y - 1] = 0;
                                                    GameSys.ColorList[x + 1, Y - 1] = 0;

                                                    Debug.Log("BPos1: " + GameSys.BigList[E].BPos);

                                                    //上の座標に下の座標を入れる
                                                    GameSys.BigList[E].BPos = new Vector2(GameSys.BPosX[x, Y], GameSys.BPosY[x, Y]);

                                                    Debug.Log("BPos2: " + GameSys.BigList[E].BPos + " / EraseBPos: " + EraseBPos);

                                                    EraseBPos = new Vector2(EraseBPos.x, EraseBPos.y + 50);

                                                    //Vector2.Lerp(始点,終点,0.0f～1.0f);
                                                    MoveP = Vector2.Lerp(GameSys.BigList[E].BPos, EraseBPos, GameSys.tmpTime);
                                                    GameSys.BigList[E].BigB.transform.localPosition = new Vector2(MoveP.x, MoveP.y);

                                                    GameSys.BigList[E].BigB.transform.SetParent(GS.Panel.transform, false);

                                                    EraseBPos.y = EraseBPos.y + 100;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        else
                        {
                            Debug.Log("ZeRo");
                        }
                    }
                }
            }
        }
    }
    */
}