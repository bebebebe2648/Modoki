using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSys : MonoBehaviour
{
    public class SBlock
    {
        public GameObject SmallB;
        public Vector2 SPos;
        public int SColor;
        public int SID;

        public SBlock(GameObject SmallB, Vector2 SPos, int SColor)
        {
            this.SmallB = SmallB;
            this.SPos = SPos;
            this.SColor = SColor;
        }
    };

    public class BBlock
    {
        public GameObject BigB;
        public Vector2 BPos;
        public int BColor;
        public int BID;

        public BBlock(GameObject BigB, Vector2 BPos, int BColor)
        {
            this.BigB = BigB;
            this.BPos = BPos;
            this.BColor = BColor;
        }
    };

    //EraseClick EC;

    //各ブロック
    [SerializeField]
    public GameObject BluB;  //青ブロック
    [SerializeField]
    public GameObject GreB;  //緑ブロック
    [SerializeField]
    public GameObject RedB;  //赤ブロック
    [SerializeField]
    public GameObject BlaB;  //灰ブロック
    [SerializeField]
    public GameObject YelB;  //黄ブロック

    //各大ブロック
    [SerializeField]
    public GameObject BIGBluB;  //青ブロック
    [SerializeField]
    public GameObject BIGGreB;  //緑ブロック
    [SerializeField]
    public GameObject BIGRedB;  //赤ブロック
    [SerializeField]
    public GameObject BIGBlaB;  //灰ブロック
    [SerializeField]
    public GameObject BIGYelB;  //黄ブロック

    //データ上での色
    int BlockC = 0;
    //消した座標のX軸格納
    public int EraseX = -1;
    public int EraseY = -1;

    [SerializeField]
    public GameObject Panel;

    public Vector2 EraseSPos;
    public Vector2 EraseBPos;

    Vector2 MoveXY;

    public Vector2 MoveP;

    //データ上の配置・大ブロック判定用2次元配列（色配置）
    public int[,] ColorList = new int[9, 9]{
    {0,0,0,0,0,0,0,0,-1},
    {0,0,0,0,0,0,0,0,-1},
    {0,0,0,0,0,0,0,0,-1},
    {0,0,0,0,0,0,0,0,-1},
    {0,0,0,0,0,0,0,0,-1},
    {0,0,0,0,0,0,0,0,-1},
    {0,0,0,0,0,0,0,0,-1},
    {0,0,0,0,0,0,0,0,-1},
    {-1,-1,-1,-1,-1,-1,-1,-1,-1}
    };

    //全体の座標
    public float[,] SPosX = new float[8, 8];
    public float[,] SPosY = new float[8, 8];
    public float[,] BPosX = new float[8, 8];
    public float[,] BPosY = new float[8, 8];

    public List<SBlock> SmallList = new List<SBlock>();
    public List<BBlock> BigList = new List<BBlock>();

    //float intarval = 1.0f;
    public float tmpTime = 1.0f;

    bool MoveC = false;
    public bool CheckP = false;
    public bool CheckP2 = false;
    public bool CheckP3 = false;
    public bool CheckP4 = false;
    public bool CheckP5 = false;
    public bool CheckP6 = false;
    public bool CheckP7 = false;
    public bool CheckP8 = false;
    public bool CheckP9 = false;
    public bool CheckP10 = false;

    public bool AddCP = false;

    void Awake()
    {
        //EC = GameObject.Find("GameSys").GetComponent<EraseClick>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //8*8のブロック配置
        Gene();
        Debug.Log(ColorList[1, 0]);
        BIGGene();
        MoveC = true;
    }


    void FixedUpdate()
    {
        /*
        if (MoveC == true)
        {
            tmpTime += Time.deltaTime;

            if (tmpTime >= intarval)
            {
                
				if (EraseSPos.y <= 175 || EraseBPos.y <= 150)
				{
					Move();
				}

				if (EraseSPos.y > 175 || EraseBPos.y > 150)
				{
					//Move2();
				}
                

                Move();
                AddBlock();
                tmpTime = 0;
            }
        }
        */

        if (MoveC == true)
        {
            Move();

            if (EraseX != -1 || CheckP != false || CheckP2 != false || CheckP3 != false || CheckP4 != false || CheckP5 != false ||
                CheckP6 != false || CheckP7 != false || CheckP8 != false || CheckP9 != false || CheckP10 != false)
            {
                //Debug.Log("Time!!");
                //tmpTime += Time.deltaTime;

            }

            if (EraseX == -1 || CheckP == false && CheckP2 == false && CheckP3 == false && CheckP4 == false && CheckP5 == false &&
                CheckP6 == false && CheckP7 == false && CheckP8 == false && CheckP9 == false && CheckP10 == false)
            {
                //Debug.Log("ADD!!");
                //tmpTime = 0;
                AddBlock();
            }

        }

        //Move();
    }


    //10*10のブロックをランダム生成
    public void Gene()
    {
        //見かけ上の位置
        float Y = 175;
        //データ上での位置
        int Q = 0;

        for (int i = 0; i < 8; i++)
        {
            //見かけ上の位置
            float X = -175;
            //データ上での位置
            int P = 0;

            for (int j = 0; j < 8; j++)
            {
                //データ上の色の初期化
                BlockC = 0;

                int Set = Random.Range(1, 4);
                switch (Set)
                {
                    case 1:
                        SmallList.Add(new SBlock(Instantiate(BluB), new Vector2(X, Y), 1));
                        SmallList[SmallList.Count - 1].SmallB.transform.position = new Vector2(SmallList[SmallList.Count - 1].SPos.x, SmallList[SmallList.Count - 1].SPos.y);
                        SmallList[SmallList.Count - 1].SmallB.transform.SetParent(Panel.transform, false);

                        SmallList[SmallList.Count - 1].SID = SmallList[SmallList.Count - 1].SmallB.GetInstanceID();

                        SPosX[P, Q] = SmallList[SmallList.Count - 1].SPos.x;
                        SPosY[P, Q] = SmallList[SmallList.Count - 1].SPos.y;

                        X = X + 50;
                        BlockC = 1;
                        ColorList[P, Q] = BlockC;

                        //データ上での位置移動
                        P = P + 1;
                        break;

                    case 2:
                        SmallList.Add(new SBlock(Instantiate(GreB), new Vector2(X, Y), 2));
                        SmallList[SmallList.Count - 1].SmallB.transform.position = new Vector2(SmallList[SmallList.Count - 1].SPos.x, SmallList[SmallList.Count - 1].SPos.y);
                        SmallList[SmallList.Count - 1].SmallB.transform.SetParent(Panel.transform, false);

                        SmallList[SmallList.Count - 1].SID = SmallList[SmallList.Count - 1].SmallB.GetInstanceID();

                        SPosX[P, Q] = SmallList[SmallList.Count - 1].SPos.x;
                        SPosY[P, Q] = SmallList[SmallList.Count - 1].SPos.y;

                        X = X + 50;
                        BlockC = 2;
                        ColorList[P, Q] = BlockC;

                        //データ上での位置移動
                        P = P + 1;
                        break;


                    case 3:
                        SmallList.Add(new SBlock(Instantiate(RedB), new Vector2(X, Y), 3));
                        SmallList[SmallList.Count - 1].SmallB.transform.position = new Vector2(SmallList[SmallList.Count - 1].SPos.x, SmallList[SmallList.Count - 1].SPos.y);
                        SmallList[SmallList.Count - 1].SmallB.transform.SetParent(Panel.transform, false);

                        SmallList[SmallList.Count - 1].SID = SmallList[SmallList.Count - 1].SmallB.GetInstanceID();

                        SPosX[P, Q] = SmallList[SmallList.Count - 1].SPos.x;
                        SPosY[P, Q] = SmallList[SmallList.Count - 1].SPos.y;

                        X = X + 50;
                        BlockC = 3;
                        ColorList[P, Q] = BlockC;

                        //データ上での位置移動
                        P = P + 1;
                        break;
                }
            }

            Y = Y - 50;

            //データ上での位置移動
            Q = Q + 1;
        }
    }

    //小ブロックを確認して、大ブロックを生成
    public void BIGGene()
    {
        //大ブロックと小ブロックの座標を初期化
        float J = 150;

        for (int y = 0; y < 8; y++)
        {
            //大ブロックと小ブロックの座標を初期化
            float I = -150;

            for (int x = 0; x < 8; x++)
            {
                BPosX[x, y] = I;
                BPosY[x, y] = J;
                //Debug.Log("BPosX: " + BPosX[x, y] + " / BPosY: " + BPosY[x, y]);

                if (x < 7 && y < 7)
                {
                    //4つの色が同じであるか
                    if (ColorList[x, y] == ColorList[x + 1, y] && ColorList[x, y + 1] == ColorList[x + 1, y + 1] &&
                        ColorList[x, y] == ColorList[x, y + 1] && ColorList[x + 1, y] == ColorList[x + 1, y + 1] &&
                        ColorList[x, y] == ColorList[x + 1, y + 1] && ColorList[x + 1, y] == ColorList[x, y + 1])
                    {
                        for (int A = 0; A < SmallList.Count; A++)
                        {
                            //小ブロックを削除
                            if (SPosX[x, y] == SmallList[A].SPos.x && SPosY[x, y] == SmallList[A].SPos.y)
                            {
                                DestroyImmediate(SmallList[A].SmallB);
                                SmallList.RemoveAt(A);
                                break;
                            }
                        }

                        for (int B = 0; B < SmallList.Count; B++)
                        {
                            //小ブロックを削除
                            if (SPosX[x + 1, y] == SmallList[B].SPos.x && SPosY[x + 1, y] == SmallList[B].SPos.y)
                            {
                                DestroyImmediate(SmallList[B].SmallB);
                                SmallList.RemoveAt(B);
                                break;
                            }
                        }

                        for (int C = 0; C < SmallList.Count; C++)
                        {
                            //小ブロックを削除
                            if (SPosX[x, y + 1] == SmallList[C].SPos.x && SPosY[x, y + 1] == SmallList[C].SPos.y)
                            {
                                DestroyImmediate(SmallList[C].SmallB);
                                SmallList.RemoveAt(C);
                                break;
                            }
                        }

                        for (int D = 0; D < SmallList.Count; D++)
                        {
                            //小ブロックを削除
                            if (SPosX[x + 1, y + 1] == SmallList[D].SPos.x && SPosY[x + 1, y + 1] == SmallList[D].SPos.y)
                            {
                                DestroyImmediate(SmallList[D].SmallB);
                                SmallList.RemoveAt(D);
                                break;
                            }
                        }

                        //何色か
                        if (ColorList[x, y] == 1)
                        {
                            //大ブロックを生成
                            BigList.Add(new BBlock(Instantiate(BIGBluB), new Vector2(I, J), 4));
                            BigList[BigList.Count - 1].BigB.transform.position = new Vector2(BigList[BigList.Count - 1].BPos.x, BigList[BigList.Count - 1].BPos.y);
                            BigList[BigList.Count - 1].BigB.transform.SetParent(Panel.transform, false);

                            BigList[BigList.Count - 1].BID = BigList[BigList.Count - 1].BigB.GetInstanceID();

                            BlockC = 4;
                            ColorList[x, y] = BlockC;
                            ColorList[x + 1, y] = BlockC;
                            ColorList[x, y + 1] = BlockC;
                            ColorList[x + 1, y + 1] = BlockC;
                            Debug.Log("[x, y]: " + ColorList[x, y] + " / [x + 1, y]: " + ColorList[x + 1, y] +
                                      " / [x, y + 1]: " + ColorList[x, y + 1] + " / [x + 1, y + 1]: " + ColorList[x + 1, y + 1]);
                        }

                        if (ColorList[x, y] == 2)
                        {
                            //大ブロックを生成
                            BigList.Add(new BBlock(Instantiate(BIGGreB), new Vector2(I, J), 5));
                            BigList[BigList.Count - 1].BigB.transform.position = new Vector2(BigList[BigList.Count - 1].BPos.x, BigList[BigList.Count - 1].BPos.y);
                            BigList[BigList.Count - 1].BigB.transform.SetParent(Panel.transform, false);

                            BigList[BigList.Count - 1].BID = BigList[BigList.Count - 1].BigB.GetInstanceID();

                            BlockC = 5;
                            ColorList[x, y] = BlockC;
                            ColorList[x + 1, y] = BlockC;
                            ColorList[x, y + 1] = BlockC;
                            ColorList[x + 1, y + 1] = BlockC;
                            Debug.Log("[x, y]: " + ColorList[x, y] + " / [x + 1, y]: " + ColorList[x + 1, y] +
                                      " / [x, y + 1]: " + ColorList[x, y + 1] + " / [x + 1, y + 1]: " + ColorList[x + 1, y + 1]);
                        }

                        if (ColorList[x, y] == 3)
                        {
                            //大ブロックを生成
                            BigList.Add(new BBlock(Instantiate(BIGRedB), new Vector2(I, J), 6));
                            BigList[BigList.Count - 1].BigB.transform.position = new Vector2(BigList[BigList.Count - 1].BPos.x, BigList[BigList.Count - 1].BPos.y);
                            BigList[BigList.Count - 1].BigB.transform.SetParent(Panel.transform, false);

                            BigList[BigList.Count - 1].BID = BigList[BigList.Count - 1].BigB.GetInstanceID();

                            BlockC = 6;
                            ColorList[x, y] = BlockC;
                            ColorList[x + 1, y] = BlockC;
                            ColorList[x, y + 1] = BlockC;
                            ColorList[x + 1, y + 1] = BlockC;
                            Debug.Log("[x, y]: " + ColorList[x, y] + " / [x + 1, y]: " + ColorList[x + 1, y] +
                                      " / [x, y + 1]: " + ColorList[x, y + 1] + " / [x + 1, y + 1]: " + ColorList[x + 1, y + 1]);
                        }

                        I = I + 50;
                    }

                    else
                    {
                        I = I + 50;
                    }
                }

                else
                {
                    I = I + 50;
                }
            }

            J = J - 50;
        }
    }

    public void AddBlock()
    {
        int P = -175;

        for (int x = 0; x < 8; x++)
        {
            //Debug.Log("TEST: A");
            int Q = 175;
            //Debug.Log("一番上の色: " + ColorList[x, 0] + " / x: " + x + " / P: " + P);

            //一番上の段が【空】ブロックであるか
            if (ColorList[x, 0] == 0)
            {
                //上から調べて、下から埋めていく
                for (int y = 0; y < 8; y++)
                {
                    //行き止まりを見つける
                    if (ColorList[x, y] != 0)
                    {
                        Q = Q + 50;

                        for (int Y = y - 1; Y >= 0; Y--)
                        {
                            if (ColorList[x, Y] == 0)
                            {
                                //Debug.Log("TEST: D");
                                Debug.Log("Debug 4: " + SPosX[x, Y] + " / " + SPosY[x, Y] + " / x: " + x + " / P: " + P + " / Q: " + Q);

                                int Set = Random.Range(1, 5);
                                switch (Set)
                                {
                                    case 1:
                                        SmallList.Add(new SBlock(Instantiate(BluB), new Vector2(P, Q), 1));
                                        SmallList[SmallList.Count - 1].SmallB.transform.position = new Vector2(SmallList[SmallList.Count - 1].SPos.x, SmallList[SmallList.Count - 1].SPos.y);
                                        SmallList[SmallList.Count - 1].SmallB.transform.SetParent(Panel.transform, false);

                                        SmallList[SmallList.Count - 1].SID = SmallList[SmallList.Count - 1].SmallB.GetInstanceID();

                                        SPosX[x, Y] = SmallList[SmallList.Count - 1].SPos.x;
                                        SPosY[x, Y] = SmallList[SmallList.Count - 1].SPos.y;

                                        BlockC = 1;
                                        ColorList[x, Y] = BlockC;
                                        Q += 50;
                                        break;

                                    case 2:
                                        SmallList.Add(new SBlock(Instantiate(GreB), new Vector2(P, Q), 2));
                                        SmallList[SmallList.Count - 1].SmallB.transform.position = new Vector2(SmallList[SmallList.Count - 1].SPos.x, SmallList[SmallList.Count - 1].SPos.y);
                                        SmallList[SmallList.Count - 1].SmallB.transform.SetParent(Panel.transform, false);

                                        SmallList[SmallList.Count - 1].SID = SmallList[SmallList.Count - 1].SmallB.GetInstanceID();

                                        SPosX[x, Y] = SmallList[SmallList.Count - 1].SPos.x;
                                        SPosY[x, Y] = SmallList[SmallList.Count - 1].SPos.y;

                                        BlockC = 2;
                                        ColorList[x, Y] = BlockC;
                                        Q += 50;
                                        break;

                                    case 3:
                                        SmallList.Add(new SBlock(Instantiate(RedB), new Vector2(P, Q), 3));
                                        SmallList[SmallList.Count - 1].SmallB.transform.position = new Vector2(SmallList[SmallList.Count - 1].SPos.x, SmallList[SmallList.Count - 1].SPos.y);
                                        SmallList[SmallList.Count - 1].SmallB.transform.SetParent(Panel.transform, false);

                                        SmallList[SmallList.Count - 1].SID = SmallList[SmallList.Count - 1].SmallB.GetInstanceID();

                                        SPosX[x, Y] = SmallList[SmallList.Count - 1].SPos.x;
                                        SPosY[x, Y] = SmallList[SmallList.Count - 1].SPos.y;

                                        BlockC = 3;
                                        ColorList[x, Y] = BlockC;
                                        Q += 50;
                                        break;

                                    case 4:
                                        SmallList.Add(new SBlock(Instantiate(YelB), new Vector2(P, Q), 7));
                                        SmallList[SmallList.Count - 1].SmallB.transform.position = new Vector2(SmallList[SmallList.Count - 1].SPos.x, SmallList[SmallList.Count - 1].SPos.y);
                                        SmallList[SmallList.Count - 1].SmallB.transform.SetParent(Panel.transform, false);

                                        SmallList[SmallList.Count - 1].SID = SmallList[SmallList.Count - 1].SmallB.GetInstanceID();

                                        SPosX[x, Y] = SmallList[SmallList.Count - 1].SPos.x;
                                        SPosY[x, Y] = SmallList[SmallList.Count - 1].SPos.y;

                                        BlockC = 7;
                                        ColorList[x, Y] = BlockC;
                                        Q += 50;
                                        break;
                                }
                            }

                            else
                            {
                                Debug.Log("TEST: E");
                                AddCP = true;
                                break;
                            }
                        }
                    }

                    else
                    {
                        Debug.Log("Debug 3");
                        Q = Q - 50;
                    }

                    if (AddCP == true)
                    {
                        AddCP = false;

                        //ループに戻って、xを加算
                        P = P + 50;
                        break;
                    }
                }
            }

            else
            {
                //ループに戻って、xを加算
                P = P + 50;
            }

        }
    }


    public void Move()
    {
        //Debug.Log("MOVE!");
        for (int x = 0; x < 8; x++)
        {
            for (int y = 7; y > 0; y--)
            {
                //落下座標
                if (ColorList[x, y] == 0)
                {






                    /*
                    //大ブロックを消した場合の処理
                    if (EraseX != -1)
                    {
                        if (EraseY != -1)
                        {
                            //空白の上のブロックが小ブロックの場合
                            if (ColorList[EraseX, EraseY - 3] == 1 || ColorList[EraseX, EraseY - 3] == 2 || ColorList[EraseX, EraseY - 3] == 3 ||
                                ColorList[EraseX, EraseY - 3] == 7)
                            {

                            }
                        }
                    }

                    else
                    {
                        if (EraseY != -1)
                        {
                            //空白の上のブロックが小ブロックの場合
                            if (ColorList[x, EraseY - 3] == 1 || ColorList[x, EraseY - 3] == 2 || ColorList[x, EraseY - 3] == 3 ||
                                ColorList[x, EraseY - 3] == 7)
                            {

                            }
                        }
                    }
                    */























                    //空白の上のブロックが小ブロックの場合
                    if (ColorList[x, y - 1] == 1 || ColorList[x, y - 1] == 2 || ColorList[x, y - 1] == 3 ||
                        ColorList[x, y - 1] == 7)
                    {
                        if (EraseX != -1)
                        {
                            if (EraseY != -1)
                            {
                                Debug.Log("EraseBIG & SMALL!!");

                                if (ColorList[EraseX, EraseY + 1] == 0)
                                {
                                    for (int Q = 0; Q < SmallList.Count; Q++)
                                    {
                                        if (SmallList[Q].SPos.x == SPosX[EraseX, EraseY - 1] && SmallList[Q].SPos.y == SPosY[EraseX, EraseY - 1])
                                        {
                                            ColorList[EraseX, EraseY + 1] = ColorList[EraseX, EraseY - 1];
                                            ColorList[EraseX, EraseY - 1] = 0;

                                            MoveP.y = Mathf.SmoothStep(SPosY[EraseX, EraseY - 1], SPosY[EraseX, EraseY + 1], tmpTime);
                                            Debug.Log("MoveP.y" + MoveP.y);

                                            SmallList[Q].SPos = new Vector2(SPosX[EraseX, EraseY + 1], MoveP.y);
                                            Debug.Log("SPos: " + SmallList[Q].SPos);
                                            SmallList[Q].SmallB.transform.localPosition = SmallList[Q].SPos;
                                            SmallList[Q].SmallB.transform.SetParent(Panel.transform, false);

                                            if (MoveP.y != 0)
                                            {
                                                CheckP = true;
                                            }

                                            if (MoveP.y == SPosY[EraseX, EraseY + 1])
                                            {
                                                Debug.Log("EraseTest");
                                                EraseX = -1;
                                                //EraseY = -1;
                                                CheckP = false;
                                                MoveP.y = 0;
                                                //tmpTime = 0;
                                            }

                                            break;
                                        }
                                    }
                                }
                            }

                            else
                            {
                                if (ColorList[EraseX, y] == 0)
                                {
                                    Debug.Log("EraseSMALL!!");

                                    for (int P = 0; P < SmallList.Count; P++)
                                    {
                                        if (SmallList[P].SPos.x == SPosX[EraseX, y - 1] && SmallList[P].SPos.y == SPosY[EraseX, y - 1])
                                        {
                                            ColorList[EraseX, y] = ColorList[EraseX, y - 1];
                                            ColorList[EraseX, y - 1] = 0;

                                            MoveP.y = Mathf.SmoothStep(SPosY[EraseX, y - 1], SPosY[EraseX, y], tmpTime);
                                            Debug.Log("MoveP.y" + MoveP.y);

                                            SmallList[P].SPos = new Vector2(SPosX[EraseX, y], MoveP.y);
                                            SmallList[P].SmallB.transform.localPosition = SmallList[P].SPos;
                                            SmallList[P].SmallB.transform.SetParent(Panel.transform, false);

                                            if (MoveP.y != 0)
                                            {
                                                CheckP = true;
                                            }

                                            if (MoveP.y == SPosY[EraseX, y])
                                            {
                                                //Debug.Log("ZeroTest");
                                                EraseX = -1;
                                                CheckP = false;
                                                MoveP.y = 0;
                                                //tmpTime = 0;
                                            }

                                            break;
                                        }
                                    }
                                }
                            }
                        }

                        else
                        {
                            if (EraseY != -1)
                            {
                                if (ColorList[x, EraseY + 1] == 0)
                                {
                                    Debug.Log("BIG & SMALL!!");

                                    for (int JJ = 0; JJ < SmallList.Count; JJ++)
                                    {
                                        //落下するブロックを決定
                                        if (SmallList[JJ].SPos.x == SPosX[x, EraseY - 1] && SmallList[JJ].SPos.y == SPosY[x, EraseY - 1])
                                        {
                                            //データ上の移動
                                            ColorList[x, EraseY + 1] = ColorList[x, EraseY - 1];
                                            ColorList[x, EraseY - 1] = 0;

                                            Debug.Log("Report2 X:" + SmallList[JJ].SPos.x + " / Y: " + SmallList[JJ].SPos.y);
                                            Debug.Log("Report3 X:" + SPosX[x, EraseY - 1] + " / Y: " + SPosY[x, EraseY - 1]);

                                            MoveP.y = Mathf.SmoothStep(SPosY[x, EraseY - 1], SPosY[x, EraseY + 1], tmpTime);

                                            Debug.Log("MoveP" + MoveP);
                                            SmallList[JJ].SPos = new Vector2(SPosX[x, EraseY], MoveP.y);
                                            SmallList[JJ].SmallB.transform.localPosition = SmallList[JJ].SPos;
                                            SmallList[JJ].SmallB.transform.SetParent(Panel.transform, false);
                                            Debug.Log("Report5 X:" + SmallList[JJ].SPos.x + " / Y: " + SmallList[JJ].SPos.y);

                                            if (MoveP.y != 0)
                                            {
                                                CheckP2 = true;
                                            }

                                            if (MoveP.y == SPosY[x, EraseY + 1])
                                            {
                                                Debug.Log("EraseYTest");
                                                EraseY = -1;
                                                CheckP2 = false;
                                                MoveP.y = 0;
                                                //tmpTime = 0;
                                            }

                                            break;
                                        }
                                    }
                                }
                            }

                            else
                            {
                                if (ColorList[x, y] == 0)
                                {
                                    Debug.Log("SMALL!!");

                                    for (int J = 0; J < SmallList.Count; J++)
                                    {
                                        //落下するブロックを決定
                                        if (SmallList[J].SPos.x == SPosX[x, y - 1] && SmallList[J].SPos.y == SPosY[x, y - 1])
                                        {
                                            //データ上の移動
                                            ColorList[x, y] = ColorList[x, y - 1];
                                            ColorList[x, y - 1] = 0;

                                            Debug.Log("Report2 X:" + SmallList[J].SPos.x + " / Y: " + SmallList[J].SPos.y);
                                            Debug.Log("Report3 X:" + SPosX[x, y - 1] + " / Y: " + SPosY[x, y - 1]);

                                            MoveP.y = Mathf.SmoothStep(SPosY[x, y - 1], SPosY[x, y], tmpTime);

                                            Debug.Log("MoveP" + MoveP);
                                            SmallList[J].SPos = new Vector2(SPosX[x, y], MoveP.y);
                                            SmallList[J].SmallB.transform.localPosition = SmallList[J].SPos;
                                            SmallList[J].SmallB.transform.SetParent(Panel.transform, false);
                                            Debug.Log("Report5 X:" + SmallList[J].SPos.x + " / Y: " + SmallList[J].SPos.y);

                                            if (MoveP.y != 0)
                                            {
                                                CheckP2 = true;
                                            }

                                            if (MoveP.y == SPosY[x, y])
                                            {
                                                CheckP2 = false;
                                                MoveP.y = 0;
                                                //tmpTime = 0;
                                            }

                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //空白の上のブロックは大ブロックの場合
                    else if (ColorList[x, y - 1] == 4 || ColorList[x, y - 1] == 5 || ColorList[x, y - 1] == 6)
                    {
                        //Debug.Log("BIGTEST: " + ColorList[x, y]);

                        switch (x)
                        {
                            case 0:

                                if (EraseX != -1)
                                {
                                    if (x != EraseX)
                                    {
                                        break;
                                    }

                                    if (ColorList[x, y] == 0 && ColorList[EraseX + 1, y] == 0)
                                    {
                                        Debug.Log("BIGEraseZero!!");

                                        for (int K = 0; K < BigList.Count; K++)
                                        {
                                            if (BigList[K].BPos.x == BPosX[EraseX, y - 2] && BigList[K].BPos.y == BPosY[EraseX, y - 2])
                                            {
                                                //データ上の色の移動
                                                ColorList[EraseX, y] = ColorList[EraseX, y - 1];
                                                ColorList[EraseX + 1, y] = ColorList[EraseX + 1, y - 1];

                                                ColorList[EraseX, y - 2] = 0;
                                                ColorList[EraseX + 1, y - 2] = 0;

                                                Debug.Log("BigList[K].BPos: " + BigList[K].BPos);

                                                MoveP.y = Mathf.SmoothStep(BPosY[EraseX, y - 2], BPosY[EraseX, y - 1], tmpTime);
                                                Debug.Log("MoveP: " + MoveP);

                                                BigList[K].BPos = new Vector2(BPosX[EraseX, y - 1], MoveP.y);
                                                BigList[K].BigB.transform.localPosition = BigList[K].BPos;
                                                BigList[K].BigB.transform.SetParent(Panel.transform, false);

                                                if (MoveP.y != 0)
                                                {
                                                    CheckP3 = true;
                                                }

                                                if (MoveP.y == BPosY[EraseX, y - 1])
                                                {
                                                    EraseX = -1;
                                                    CheckP3 = false;
                                                    MoveP.y = 0;
                                                    //tmpTime = 0;
                                                }

                                                break;
                                            }
                                        }
                                    }

                                    else
                                    {
                                        EraseX = -1;
                                    }
                                }

                                else
                                {
                                    if (ColorList[x, y] == 0 && ColorList[x + 1, y] == 0)
                                    {
                                        Debug.Log("BIGZero!!");

                                        for (int K = 0; K < BigList.Count; K++)
                                        {
                                            if (BigList[K].BPos.x == BPosX[x, y - 2] && BigList[K].BPos.y == BPosY[x, y - 2])
                                            {
                                                //データ上の色の移動
                                                ColorList[x, y] = ColorList[x, y - 1];
                                                ColorList[x + 1, y] = ColorList[x + 1, y - 1];

                                                ColorList[x, y - 2] = 0;
                                                ColorList[x + 1, y - 2] = 0;

                                                Debug.Log("BigList[K].BPos: " + BigList[K].BPos);

                                                MoveP.y = Mathf.SmoothStep(BPosY[x, y - 2], BPosY[x, y - 1], tmpTime);
                                                Debug.Log("MoveP: " + MoveP);

                                                BigList[K].BPos = new Vector2(BPosX[x, y - 1], MoveP.y);
                                                BigList[K].BigB.transform.localPosition = BigList[K].BPos;
                                                BigList[K].BigB.transform.SetParent(Panel.transform, false);

                                                if (MoveP.y != 0)
                                                {
                                                    CheckP4 = true;
                                                }

                                                if (MoveP.y == BPosY[x, y - 1])
                                                {
                                                    CheckP4 = false;
                                                    MoveP.y = 0;
                                                    //tmpTime = 0;
                                                }

                                                break;
                                            }
                                        }
                                    }

                                    else
                                    {
                                        EraseX = -1;
                                    }
                                }

                                break;

                            case 7:

                                if (EraseX != -1)
                                {
                                    if (x != EraseX)
                                    {
                                        break;
                                    }

                                    if (ColorList[x, y] == 0 && ColorList[EraseX - 1, y] == 0)
                                    {
                                        Debug.Log("BIGEraseSeven!!");

                                        for (int L = 0; L < BigList.Count; L++)
                                        {
                                            if (BigList[L].BPos.x == BPosX[EraseX - 1, y - 2] && BigList[L].BPos.y == BPosY[EraseX - 1, y - 2])
                                            {
                                                //データ上の色の移動
                                                ColorList[EraseX, y] = ColorList[EraseX, y - 1];
                                                ColorList[EraseX - 1, y] = ColorList[EraseX - 1, y - 1];

                                                ColorList[EraseX, y - 2] = 0;
                                                ColorList[EraseX - 1, y - 2] = 0;

                                                MoveP.y = Mathf.SmoothStep(BPosY[EraseX - 1, y - 2], BPosY[EraseX - 1, y - 1], tmpTime);
                                                Debug.Log("MoveP: " + MoveP);

                                                BigList[L].BPos = new Vector2(BPosX[EraseX - 1, y - 1], MoveP.y);
                                                BigList[L].BigB.transform.localPosition = BigList[L].BPos;
                                                BigList[L].BigB.transform.SetParent(Panel.transform, false);

                                                if (MoveP.y != 0)
                                                {
                                                    CheckP5 = true;
                                                }

                                                if (MoveP.y == BPosY[EraseX - 1, y - 1])
                                                {
                                                    EraseX = -1;
                                                    CheckP5 = false;
                                                    MoveP.y = 0;
                                                    //tmpTime = 0;
                                                }

                                                break;
                                            }
                                        }
                                    }

                                    else
                                    {
                                        EraseX = -1;
                                    }
                                }

                                else
                                {
                                    if (ColorList[x, y] == 0 && ColorList[x - 1, y] == 0)
                                    {
                                        Debug.Log("BIGSeven!!");

                                        for (int L = 0; L < BigList.Count; L++)
                                        {
                                            if (BigList[L].BPos.x == BPosX[x - 1, y - 2] && BigList[L].BPos.y == BPosY[x - 1, y - 2])
                                            {
                                                //データ上の色の移動
                                                ColorList[x, y] = ColorList[x, y - 1];
                                                ColorList[x - 1, y] = ColorList[x - 1, y - 1];

                                                ColorList[x, y - 2] = 0;
                                                ColorList[x - 1, y - 2] = 0;

                                                MoveP.y = Mathf.SmoothStep(BPosY[x - 1, y - 2], BPosY[x - 1, y - 1], tmpTime);
                                                Debug.Log("MoveP: " + MoveP);

                                                BigList[L].BPos = new Vector2(BPosX[x - 1, y - 1], MoveP.y);
                                                BigList[L].BigB.transform.localPosition = BigList[L].BPos;
                                                BigList[L].BigB.transform.SetParent(Panel.transform, false);

                                                if (MoveP.y != 0)
                                                {
                                                    CheckP6 = true;
                                                }

                                                if (MoveP.y == BPosY[x - 1, y - 1])
                                                {
                                                    CheckP6 = false;
                                                    MoveP.y = 0;
                                                    //tmpTime = 0;
                                                }

                                                break;
                                            }
                                        }
                                    }

                                    else
                                    {
                                        EraseX = -1;
                                    }
                                }

                                break;

                            default:

                                if (EraseX != -1)
                                {
                                    if (x != EraseX)
                                    {
                                        break;
                                    }

                                    if (ColorList[x, y] == 0 && ColorList[EraseX - 1, y] == 0 || ColorList[x, y] == 0 && ColorList[EraseX + 1, y] == 0)
                                    {
                                        //右を消して落下した場合
                                        if (ColorList[x, y] == 0 && ColorList[EraseX - 1, y] == 0)
                                        {
                                            Debug.Log("BIGEraseRight!!");
                                            Debug.Log("[EraseX, y - 1]: " + ColorList[EraseX, y - 1] + "[EraseX - 1, y - 1]: " + ColorList[EraseX - 1, y - 1] +
                                                      "[EraseX, y - 2]: " + ColorList[EraseX, y - 2] + "[EraseX - 1, y - 2]: " + ColorList[EraseX - 1, y - 2]);

                                            //4つの色が同じ落下判断処理へ
                                            if (ColorList[EraseX, y - 1] == ColorList[EraseX - 1, y - 1] &&
                                                ColorList[EraseX, y - 2] == ColorList[EraseX - 1, y - 2])
                                            {
                                                for (int M = 0; M < BigList.Count; M++)
                                                {
                                                    if (BigList[M].BPos.x == BPosX[EraseX - 1, y - 2] && BigList[M].BPos.y == BPosY[EraseX - 1, y - 2])
                                                    {
                                                        //データ上の色の移動
                                                        ColorList[EraseX, y] = ColorList[EraseX, y - 1];
                                                        ColorList[EraseX - 1, y] = ColorList[EraseX - 1, y - 1];

                                                        ColorList[EraseX, y - 2] = 0;
                                                        ColorList[EraseX - 1, y - 2] = 0;

                                                        MoveP.y = Mathf.SmoothStep(BPosY[EraseX - 1, y - 2], BPosY[EraseX - 1, y - 1], tmpTime);
                                                        Debug.Log("MoveP: " + MoveP);

                                                        BigList[M].BPos = new Vector2(BPosX[EraseX - 1, y - 1], MoveP.y);
                                                        BigList[M].BigB.transform.localPosition = BigList[M].BPos;
                                                        BigList[M].BigB.transform.SetParent(Panel.transform, false);

                                                        if (MoveP.y != 0)
                                                        {
                                                            CheckP7 = true;
                                                        }

                                                        if (MoveP.y == BPosY[EraseX - 1, y - 1])
                                                        {
                                                            EraseX = -1;
                                                            CheckP7 = false;
                                                            MoveP.y = 0;
                                                            //tmpTime = 0;
                                                        }
                                                        break;
                                                    }

                                                }
                                            }

                                            //落下できない場合
                                            else
                                            {
                                                EraseX = -1;
                                            }
                                        }

                                        //左を消して落下した場合
                                        else if (ColorList[x, y] == 0 && ColorList[EraseX + 1, y] == 0)
                                        {
                                            Debug.Log("BIGEraseLeft!!");
                                            Debug.Log("[EraseX, y - 1]: " + ColorList[EraseX, y - 1] + "[EraseX + 1, y - 1]: " + ColorList[EraseX + 1, y - 1] +
                                                      "[EraseX, y - 2]: " + ColorList[EraseX, y - 2] + "[EraseX + 1, y - 2]: " + ColorList[EraseX + 1, y - 2]);

                                            //4つの色が同じ落下判断処理へ
                                            if (ColorList[EraseX, y - 1] == ColorList[EraseX + 1, y - 1] &&
                                                ColorList[EraseX, y - 2] == ColorList[EraseX + 1, y - 2])
                                            {
                                                for (int N = 0; N < BigList.Count; N++)
                                                {
                                                    if (BigList[N].BPos.x == BPosX[EraseX, y - 2] && BigList[N].BPos.y == BPosY[EraseX, y - 2])
                                                    {
                                                        //データ上の色の移動
                                                        ColorList[EraseX, y] = ColorList[EraseX, y - 1];
                                                        ColorList[EraseX + 1, y] = ColorList[EraseX + 1, y - 1];

                                                        ColorList[EraseX, y - 2] = 0;
                                                        ColorList[EraseX + 1, y - 2] = 0;

                                                        Debug.Log("BigList[N].BPos: " + BigList[N].BPos);

                                                        MoveP.y = Mathf.SmoothStep(BPosY[EraseX, y - 2], BPosY[EraseX, y - 1], tmpTime);

                                                        BigList[N].BPos = new Vector2(BPosX[EraseX, y - 1], MoveP.y);
                                                        BigList[N].BigB.transform.localPosition = BigList[N].BPos;
                                                        BigList[N].BigB.transform.SetParent(Panel.transform, false);

                                                        if (MoveP.y != 0)
                                                        {
                                                            CheckP8 = true;
                                                        }

                                                        if (MoveP.y == BPosY[EraseX, y - 1])
                                                        {
                                                            EraseX = -1;
                                                            CheckP8 = false;
                                                            MoveP.y = 0;
                                                            //tmpTime = 0;
                                                        }

                                                        break;
                                                    }
                                                }
                                            }

                                            //落下できない場合
                                            else
                                            {
                                                EraseX = -1;
                                            }
                                        }

                                        else
                                        {
                                            EraseX = -1;
                                        }
                                    }

                                    //落下できない場合
                                    else
                                    {
                                        EraseX = -1;
                                    }
                                }

                                else
                                {
                                    if (ColorList[x, y] == 0 && ColorList[x - 1, y] == 0 || ColorList[x, y] == 0 && ColorList[x + 1, y] == 0)
                                    {
                                        //右を消して落下した場合
                                        if (ColorList[x, y] == 0 && ColorList[x - 1, y] == 0)
                                        {
                                            Debug.Log("BIGRight!!");
                                            Debug.Log("[x, y - 1]: " + ColorList[x, y - 1] + "[x - 1, y - 1]: " + ColorList[x - 1, y - 1] +
                                                      "[x, y - 2]: " + ColorList[x, y - 2] + "[x - 1, y - 2]: " + ColorList[x - 1, y - 2]);

                                            //4つの色が同じ落下判断処理へ
                                            if (ColorList[x, y - 1] == ColorList[x - 1, y - 1] &&
                                                ColorList[x, y - 2] == ColorList[x - 1, y - 2])
                                            {
                                                for (int M = 0; M < BigList.Count; M++)
                                                {
                                                    if (BigList[M].BPos.x == BPosX[x - 1, y - 2] && BigList[M].BPos.y == BPosY[x - 1, y - 2])
                                                    {
                                                        //データ上の色の移動
                                                        ColorList[x, y] = ColorList[x, y - 1];
                                                        ColorList[x - 1, y] = ColorList[x - 1, y - 1];

                                                        ColorList[x, y - 2] = 0;
                                                        ColorList[x - 1, y - 2] = 0;

                                                        MoveP.y = Mathf.SmoothStep(BPosY[x - 1, y - 2], BPosY[x - 1, y - 1], tmpTime);
                                                        Debug.Log("MoveP: " + MoveP);

                                                        BigList[M].BPos = new Vector2(BPosX[x - 1, y - 1], MoveP.y);
                                                        BigList[M].BigB.transform.localPosition = BigList[M].BPos;
                                                        BigList[M].BigB.transform.SetParent(Panel.transform, false);

                                                        if (MoveP.y != 0)
                                                        {
                                                            CheckP9 = true;
                                                        }

                                                        if (MoveP.y == BPosY[x - 1, y - 1])
                                                        {
                                                            CheckP9 = false;
                                                            MoveP.y = 0;
                                                            //tmpTime = 0;
                                                        }

                                                        break;
                                                    }

                                                }
                                            }

                                            //落下できない場合
                                            else
                                            {
                                                EraseX = -1;
                                            }
                                        }

                                        //左を消して落下した場合
                                        else if (ColorList[x, y] == 0 && ColorList[x + 1, y] == 0)
                                        {
                                            Debug.Log("BIGLeft!!");
                                            Debug.Log("[x, y - 1]: " + ColorList[x, y - 1] + "[x + 1, y - 1]: " + ColorList[x + 1, y - 1] +
                                                      "[x, y - 2]: " + ColorList[x, y - 2] + "[x + 1, y - 2]: " + ColorList[x + 1, y - 2]);

                                            //4つの色が同じ落下判断処理へ
                                            if (ColorList[x, y - 1] == ColorList[x + 1, y - 1] &&
                                                ColorList[x, y - 2] == ColorList[x + 1, y - 2])
                                            {
                                                for (int N = 0; N < BigList.Count; N++)
                                                {
                                                    if (BigList[N].BPos.x == BPosX[x, y - 2] && BigList[N].BPos.y == BPosY[x, y - 2])
                                                    {
                                                        //データ上の色の移動
                                                        ColorList[x, y] = ColorList[x, y - 1];
                                                        ColorList[x + 1, y] = ColorList[x + 1, y - 1];

                                                        ColorList[x, y - 2] = 0;
                                                        ColorList[x + 1, y - 2] = 0;

                                                        Debug.Log("BigList[N].BPos: " + BigList[N].BPos);
                                                        MoveP.y = Mathf.SmoothStep(BPosY[x, y - 2], BPosY[x, y - 1], tmpTime);
                                                        //Debug.Log("MoveXY: " + MoveXY);

                                                        BigList[N].BPos = new Vector2(BPosX[x, y - 1], MoveP.y);
                                                        BigList[N].BigB.transform.localPosition = BigList[N].BPos;
                                                        BigList[N].BigB.transform.SetParent(Panel.transform, false);

                                                        if (MoveP.y != 0)
                                                        {
                                                            CheckP10 = true;
                                                        }

                                                        if (MoveP.y == BPosY[x, y - 1])
                                                        {
                                                            CheckP10 = false;
                                                            MoveP.y = 0;
                                                            //tmpTime = 0;
                                                        }

                                                        break;
                                                    }
                                                }
                                            }

                                            //落下できない場合
                                            else
                                            {
                                                EraseX = -1;
                                            }
                                        }

                                        else
                                        {
                                            EraseX = -1;
                                        }
                                    }

                                    //落下できない場合
                                    else
                                    {
                                        EraseX = -1;
                                    }
                                }

                                break;
                        }
                    }
                }
            }
        }
    }
}