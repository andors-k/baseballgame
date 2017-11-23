using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Baseball_Win
{

    struct B_List                      //  한번의 숫자와 S B   숫자정보를 저장하는 단위   ex) 123 1S 1B
    {
        public  int NUM;

        public int S;
        public int B;

        public B_List(int num, int s, int b)
        {
            NUM = num;
            S = s;
            B = b;
        }
    }



    class I_BB                        // 내숫자 : 내가(I) 숫자열을 정하고 상대방(Y)가 추측하는 숫자 데이타와 관련된 Class  
    {
        public const int BSize =4  ;   // 게임의 자리수 43으로 시작하자..... 1번째


        public List<B_List> num_history = new List<B_List>();

        /*
        public List<int> num_history = new List<int>();
        public List<int> S_history = new List<int>();
        public List<int> B_history = new List<int>();
        */

        public  int SNUM
        {
            get; set;
        }

        public I_BB()     // 생성자
        {
            SNUM = set_SNUM();  
        }
        
        public int[] int2num(int a)           // 숫자열을 배열로 만드는 함수    123 --> num[0] = 1 , num[1]=2 , num[2] =3
        {
            int[] num = new int[BSize];

            for (int i = 0; i < BSize; ++i)
            {
                num[BSize - 1 - i] = a % 10;
                a = a / 10;
            }
            return num;
        }
        public static  int[] int2num(int a, int Size)           // 정적  숫자열을 배열로 만드는 함수    123 --> num[0] = 1 , num[1]=2 , num[2] =3
        {
            int[] num = new int[Size];

            for (int i = 0; i < Size; ++i)
            {
                num[Size - 1 - i] = a % 10;
                a = a / 10;
            }
            return num;
        }
        
        public  int num2int(int[] num)      // // 배열을 숫자열로 만드는 함수     num[0] = 1 , num[1]=2 , num[2] =3  --->123
        {
            int temp = 0;
            for (int i = 0; i < BSize; ++i)
            {
                temp += (num[BSize - i - 1] * (int)(Math.Pow(10, i)));
            }
            return temp;
        }

        public static int num2int(int[] num, int Size)      // // 배열을 숫자열로 만드는 함수     num[0] = 1 , num[1]=2 , num[2] =3  --->123
        {
            int temp = 0;
            for (int i = 0; i < Size; ++i)
            {
                temp += (num[Size - i - 1] * (int)(Math.Pow(10, i)));
            }
            return temp;
        }

        public  bool chk_num(int n)
        {
            int[] num = int2num(n); 
            for (int i = 0; i < BSize; ++i)
            {
                for (int j = 0; j < i; ++j)
                {
                    if (num[i] == num[j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool chk_num(int n,int Size)
        {
            int[] num = int2num(n,Size);
            for (int i = 0; i < Size; ++i)
            {
                for (int j = 0; j < i; ++j)
                {
                    if (num[i] == num[j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        /*
        public void  add_list(int num)    // 새로운 수를 받아서 SNUM과 비교해서 S,B를 설정  하고 List에 추가 반환은 리스트 길이 + 1 (index)
        {
            int i, j;
            int s_count = 0, b_count = 0;
            B_List b_list;

            p_list(SNUM, num, out s_count, out b_count);      // 내가정한 숫자(SNUM)와 추측하는 숫자(num)를 비교하여 s , b 를 구한다
                        
            b_list = new B_List(num, s_count, b_count); 

            num_history.Add(b_list);
                       
        }
        */

        public int p_list(int m1, int m2, out int s_count, out int b_count)     // 실제로 S B 판정  return s+b
        {
            int i, j;
            int s = 0, b = 0;
            int[] n1 = int2num(m1);
            int[] n2 = int2num(m2);


            for (i = 0; i < BSize; ++i)
            {
                for (j = 0; j < BSize; ++j)
                {
                    if (n1[i] == n2[j])
                    {
                        if (i == j) s++;
                        else b++;
                    }

                }
            }
            s_count = s;
            b_count = b;
            return s + b;
        }



        //  SNUM(최초숫자)를 설정 
        private int set_SNUM()
        {
            int[] ArraySNUM = new int[BSize];           // 생성될 때처음 숫자를 랜덤하게 정한다   내(I)가 처음 정한 수열

            Random rand = new Random();
            bool goodset;

            for (int i = 0; i < BSize; ++i)
            {
                if (i == 0) ArraySNUM[i] = rand.Next(1, 9);
                else
                {
                    do
                    {
                        goodset = true;
                        ArraySNUM[i] = rand.Next(0, 9);

                        for (int j = 0; j < i; ++j)
                        {
                            if (ArraySNUM[i] == ArraySNUM[j]) goodset = false;
                        }

                    } while (goodset == false);
                }
            }
            return num2int(ArraySNUM);
        }       
        // I_BB class 끝지점

    }


    class Y_BB : I_BB
    {
        int[] Tn = new int[10];

        public Y_BB()         //  4 4 4 4 4  현재로서는 런타임으로 자리수를 바꿀 방법을 찾을 수 없다  
        {
            set_Tn();            
        }

        public int guess_num()   // 추측값   추측이 불가능하면 000
        {
            int i = 0 , j = 0;
            int s=0, b=0;
            int[] tn = new int[BSize];
            int[]  num = new int[BSize];
            int n; 
            bool p = true;

            
            for (i = (int)Math.Pow(10,BSize-1) ; i < (int)Math.Pow(10, BSize)-1; i++)
            {
                tn = int2num(i);
                for (j = 0; j < BSize; j++) num[j] = Tn[tn[j]];
                n = num2int(num);

                if (chk_num(n) == false) continue;
                else
                {
                    p = true;
                    for (int m = 0; m < num_history.Count; ++m)
                    {                        
                        p_list(n, num_history[m].NUM, out s, out b);

                        if (num_history[m].S != s || num_history[m].B != b)
                        {
                            p = false;
                            break;
                        }
                    }

                    if (p == true) return n;
                }
            }
                                   
            return 0;
        }


        private void set_Tn()
        {
            Random rand = new Random();
            int i = 0, j = 0;
            // --> 다양성을 증가하기 위한 Tn 배열을 만든다   추측하는 수 476 은 T[4]Tn[7]Tn[6] 으로 바뀐다.
            // for (i = 0; i < 10; i++) Tn[i] = i;

            Tn[0] = 0;

            for (i = 1; i < 10; i++)
            {
                Tn[i] = rand.Next(1, 9);
                do
                {
                    for (j = 1; j < i; j++)
                    {
                        if (Tn[i] == Tn[j])
                        {
                            if (Tn[i] == 9) Tn[i] = 1;
                            else Tn[i]++;

                            break;
                        }
                    }
                } while (j < i);  // j < i 인 경우 Tn[i] 값이 변경되고 break에 의해 for문을 탈출했다는 뜻                    

            }
        }












    }    
}
