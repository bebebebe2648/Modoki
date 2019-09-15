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

    [SerializeField]
    public GameObject Panel;

    //データ上の配置・大ブロック判定用2次元配列（色配置）
    public static int[,] ColorList= new int[8, 8]{
    {0,0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0,0}
    };

    //全体の座標
    public static float[,] SPosX = new float[8, 8];
    public static float[,] SPosY = new float[8, 8];
    public static float[,] BPosX = new float[8, 8];
    public static float[,] BPosY = new float[8, 8];

    public static List<SBlock> SmallList = new List<SBlock>();
    public static List<BBlock> BigList = new List<BBlock>();

    float intarval = 0.3f;
    public static float tmpTime = 0;

    bool MoveC = false;
    bool CheckP = false;

    void Awake()
    {
        //EC = GameObject.Find("GameSys").GetComponent<EraseClick>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //10*10のブロック配置
        Gene();
        Debug.Log(ColorList[1, 0]);
        BIGGene();
        MoveC = true;
    }

    
    void FixedUpdate()
    {
        if (MoveC == true)
        {
            tmpTime += Time.deltaTime;

            if (tmpTime >= intarval)
            {
                Move();
                //AddBlock();
                tmpTime = 0;
            }
        }
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

            for(int j = 0; j < 8; j++)
            {
                //データ上の色の初期化
                BlockC = 0;

                int Set = Random.Range(1, 4);
                switch (Set)
                {
                    case 1:
                        SmallList.Add(new SBlock (Instantiate(BluB), new Vector2(X, Y), 1));
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
        int Q = 175;

        for (int x = 0; x < 8; x++)
        {
            Debug.Log("TEST: A");
            Q = 175;
            Debug.Log("一番上の色: " + ColorList[x, 0]);

            //一番上の段が【空】ブロックであるか
            if (ColorList[x, 0] == 0)
            {
                //上から調べて、下から埋めていく
                for (int y = 0; y < 8; y++)
                {
                    Debug.Log("TEST: B");

                    //行き止まりを見つける
                    if (ColorList[x, y] != 0)
                    {
                        Q = Q + 50;

                        for (int Y = y - 1; Y >= 0; Y--)
                        {
                            if (ColorList[x, Y] == 0)
                            {
                                Debug.Log("TEST: D");
                                Debug.Log("Debug 4: " + SPosX[x, Y] + " / " + SPosY[x, Y]);

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
                                        break;
                                }
                            }

                            else
                            {
                                Debug.Log("TEST: E");
                                break;
                            }
                        }
                    }

                    else
                    {
                        Debug.Log("Debug 3");
                        Q = Q - 50;
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
        for (int y = 7; y > 0; y--)
        {
            for (int x = 0; x < 8; x++)
            {
                //落下座標
                if (ColorList[x, y] == 0)
                {
                    //消したブロックの1個上を比較対象に
                    for (int Y = y - 1; Y > 0; Y--)
                    {
                        Debug.Log("Color[x, Y]: " + ColorList[x, Y]);

                        //消したブロックの上のブロックがあるか
                        if (ColorList[x, Y] != 0)
                        {
                            Debug.Log("x: " + x + " / Y: " + Y);

                            //小ブロックの場合
                            if (ColorList[x, Y] == 1 || ColorList[x, Y] == 2 || ColorList[x, Y] == 3 ||
                                ColorList[x, Y] == 7)
                            {
                                //データ上の移動
                                ColorList[x, Y + 1] = ColorList[x, Y];
                                ColorList[x, Y] = 0;

                                for (int D = 0; D < SmallList.Count; D++)
                                {
                                    //落下するブロックを決定
                                    if (SmallList[D].SPos.x == EraseClick.EraseSPos.x && SmallList[D].SPos.y == EraseClick.EraseSPos.y + 50)
                                    {
                                        Debug.Log("Report2 同じ X:" + SmallList[D].SPos.x + " / Y: " + SmallList[D].SPos.y);
                                        Debug.Log("Report3 X:" + SPosX[x, Y + 1] + " / Y: " + SPosY[x, Y + 1]);
                                        SmallList[D].SPos = new Vector2(SPosX[x, Y + 1], SPosY[x, Y + 1]);
                                        //GameSys.SmallList[D].SPos = new Vector2(GameSys.SPosX[x, Y + 1], GameSys.SPosY[x, Y + 1]);

                                        //Vector2.Lerp(始点,終点,0.0f～1.0f);
                                        //Debug.Log("Time: " + GameSys.tmpTime + " / EraseSPos: " + EraseSPos);
                                        EraseClick.MoveP = Vector2.Lerp(SmallList[D].SPos, EraseClick.EraseSPos, tmpTime);

                                        //Debug.Log("MoveP" + MoveP);
                                        SmallList[D].SmallB.transform.localPosition = new Vector2(EraseClick.MoveP.x, EraseClick.MoveP.y);
                                        //Debug.Log("Report3: " + GameSys.SmallList[D].SmallB.transform.localPosition);

                                        //GameSys.SmallList[D].SmallB.transform.localPosition = new Vector2(GameSys.SmallList[D].SPos.x, GameSys.SmallList[D].SPos.y);
                                        SmallList[D].SmallB.transform.SetParent(Panel.transform, false);

                                        //Debug.Log("Report4 X:" + GameSys.SmallList[D].SmallB.transform.position.x + " / Y: " + GameSys.SmallList[D].SmallB.transform.position.y);
                                        Debug.Log("Report5 X:" + SmallList[D].SPos.x + " / Y: " + SmallList[D].SPos.y);
                                        //Debug.Log("Report6: " + GameSys.SmallList[D].SmallB.transform.localPosition);

                                        EraseClick.EraseSPos.y = EraseClick.EraseSPos.y + 50;
                                        break;
                                    }
                                }
                            }

                            //大ブロックの場合
                            else if (ColorList[x, Y] == 4 || ColorList[x, Y] == 5 || ColorList[x, Y] == 6)
                            {
                                Debug.Log("TESTBIG!!");
                                Debug.Log("Color[x + 1, Y + 1]: " + ColorList[x + 1, Y + 1]);

                                switch (x)
                                {
                                    case 0:

                                        break;

                                    case 7:

                                        break;
                                }

                                //落下できるか
                                if (ColorList[x + 1, Y + 1] == 0)
                                {
                                    Debug.Log("TESTBIG!!!!");

                                    //4つの色が同じ落下判断処理へ
                                    if (ColorList[x, Y] == ColorList[x + 1, Y] &&
                                        ColorList[x, Y - 1] == ColorList[x + 1, Y - 1])
                                    {
                                        //落下できるならば、大ブロックを検索
                                        for (int B = 0; B < BigList.Count; B++)
                                        {
                                            Debug.Log("TESTBIG!!!!!!");

                                            //リストの中身が1つしか大ブロックがない場合
                                            if (BigList.Count == 1)
                                            {
                                                //
                                                ColorList[x, Y + 1] = ColorList[x, Y];
                                                ColorList[x + 1, Y + 1] = ColorList[x + 1, Y];

                                                ColorList[x, Y - 1] = 0;
                                                ColorList[x + 1, Y - 1] = 0;

                                                Debug.Log("BIGONE!!");

                                                //上の座標に下の座標を入れる
                                                BigList[B].BPos = new Vector2(BPosX[x, Y], BPosY[x, Y]);

                                                EraseClick.EraseBPos = new Vector2(EraseClick.EraseBPos.x, EraseClick.EraseBPos.y + 50);
                                                Debug.Log("EraseBPos: " + EraseClick.EraseBPos);

                                                //Vector2.Lerp(始点,終点,0.0f～1.0f);
                                                EraseClick.MoveP = Vector2.Lerp(BigList[B].BPos, EraseClick.EraseBPos, tmpTime);
                                                BigList[B].BigB.transform.localPosition = new Vector2(EraseClick.MoveP.x, EraseClick.MoveP.y);

                                                BigList[B].BigB.transform.SetParent(Panel.transform, false);

                                                EraseClick.EraseBPos.y = EraseClick.EraseBPos.y + 100;
                                                CheckP = true;
                                                break;
                                            }

                                            //リストの中身が2つ以上、大ブロックがある場合
                                            //落下しない
                                            Debug.Log("B X: " + BigList[B].BPos.x + " / Y: " +BigList[B].BPos.y +
                                                      " / BPosX: " + BPosX[x + 1, Y - 1] + " / BPosY: " + BPosY[x + 1, Y - 1]);

                                            if (BigList[B].BPos.x == BPosX[x + 1, Y - 1] && BigList[B].BPos.y == BPosY[x + 1, Y - 1])
                                            {
                                                CheckP = true;
                                                break;
                                            }
                                        }

                                        if (CheckP == true)
                                        {
                                            CheckP = false;
                                            break;
                                        }

                                        else
                                        {
                                            for (int E = 0; E < BigList.Count; E++)
                                            {
                                                Debug.Log("BIG!!");
                                                Debug.Log("E X: " + BigList[E].BPos.x + " / Y: " + BigList[E].BPos.y +
                                                          " / BPosX: " + BPosX[x, Y - 1] + " / BPosY: " + BPosY[x, Y - 1]);
                                                Debug.Log("EraseBPos: " + EraseClick.EraseBPos);

                                                if (BigList[E].BPos.x == EraseClick.EraseBPos.x && BigList[E].BPos.y == EraseClick.EraseBPos.y + 100)
                                                //if (GameSys.BigList[E].BPos.x == GameSys.BPosX[x, Y - 1] && GameSys.BigList[E].BPos.y == GameSys.BPosY[x, Y - 1])
                                                {
                                                    //
                                                    ColorList[x, Y + 1] = ColorList[x, Y];
                                                    ColorList[x + 1, Y + 1] = ColorList[x + 1, Y];

                                                    ColorList[x, Y - 1] = 0;
                                                    ColorList[x + 1, Y - 1] = 0;

                                                    Debug.Log("BPos1: " + BigList[E].BPos);

                                                    //上の座標に下の座標を入れる
                                                    BigList[E].BPos = new Vector2(BPosX[x, Y], BPosY[x, Y]);

                                                    Debug.Log("BPos2: " + BigList[E].BPos + " / EraseBPos: " + EraseClick.EraseBPos);

                                                    EraseClick.EraseBPos = new Vector2(EraseClick.EraseBPos.x, EraseClick.EraseBPos.y + 50);

                                                    //Vector2.Lerp(始点,終点,0.0f～1.0f);
                                                    EraseClick.MoveP = Vector2.Lerp(BigList[E].BPos, EraseClick.EraseBPos, tmpTime);
                                                    BigList[E].BigB.transform.localPosition = new Vector2(EraseClick.MoveP.x, EraseClick.MoveP.y);

                                                    BigList[E].BigB.transform.SetParent(Panel.transform, false);

                                                    EraseClick.EraseBPos.y = EraseClick.EraseBPos.y + 100;
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
}
