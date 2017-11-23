using Android.App;
using Android.Widget;
using Android.OS;
using Baseball_Win;
using System;
using System.Text;

namespace App6
{
    [Activity(Label = "숫자야구 Ver 0.2", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        I_BB i_bb = new I_BB();         //   4  44 44 44 4  
        Y_BB y_bb = new Y_BB();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            Button Me = FindViewById<Button>(Resource.Id.Me_button);
            Button You = FindViewById<Button>(Resource.Id.You_button);
            Button Me_End = FindViewById<Button>(Resource.Id.Me_End);
            Button You_End = FindViewById<Button>(Resource.Id.You_End);

            EditText Me_guess = FindViewById<EditText>(Resource.Id.Me_editText);
            EditText You_guess = FindViewById<EditText>(Resource.Id.You_editText);
            Button S = FindViewById<Button>(Resource.Id.S_button);
            Button B = FindViewById<Button>(Resource.Id.B_button);

            EditText Me_List = FindViewById<EditText>(Resource.Id.MeList_editText);
            EditText You_List = FindViewById<EditText>(Resource.Id.YouList_editText);
            EditText Me_Memo = FindViewById<EditText>(Resource.Id.Me_Memo_editText);
            EditText You_Memo = FindViewById<EditText>(Resource.Id.You_Memo_editText);

            TextView Me_ERR = FindViewById<TextView>(Resource.Id.Me_ERR_textView);

            // TextView You_ERR = FindViewById<TextView>(Resource.Id.You_ERR_textView);

            Me.Click += delegate
            {
                int n , s=0,b=0;
                B_List b_list; 

                if (int.TryParse(Me_guess.Text, out n)  && n > 0 && (int)Math.Pow(10, I_BB.BSize) > n && i_bb.chk_num(n))    
                   // 정수형으로 변환 가능 & 0보다 크고 & 10^5(4자수리 수중 가장큼= 4자리 정수란 뚯 )  & 자리수에 숫자가 중복 안됨
                {
                    // num = I_BB.int2num(n);


                    if (i_bb.num_history.FindIndex(x => x.NUM==n) == -1)  
                    {
                        i_bb.p_list(i_bb.SNUM, n,out s ,out b);
                        b_list = new B_List(n, s, b);
                        i_bb.num_history.Add(b_list);                        
                        Me_List.Text = Me_guess.Text + "   : " + s + "S " + b + "B" + System.Environment.NewLine + Me_List.Text;
                        Me_ERR.Text = s + "S" + b + "B";

                        if(s ==4 ) Me_List.Text = (i_bb.num_history.Count).ToString() + System.Environment.NewLine + Me_List.Text;
                    }
                    else
                    {
                        Me_ERR.Text = "이미 입력한 질의입니다";
                    }                    
                }
                else
                {
                    Me_ERR.Text =  "잘못된 입력입니다";
                }

            };

            You.Click += delegate
            {
                if(You.Text == "네가 맞춰봐")
                {
                    if(y_bb.guess_num() == 0)
                    {
                        You_guess.Text = "답 없다.오류입력 있음.";
                    }
                    else
                    {
                        You_guess.Text = y_bb.guess_num().ToString();
                        You.Text = "네추측의 결과";
                    }
                    
                }
                else
                {
                    int n = Convert.ToInt32(You_guess.Text);

                    if (y_bb.num_history.FindIndex(x => x.NUM == n) == -1)
                    {
                        B_List b_List = new B_List(n, Convert.ToInt32((S.Text).Substring(0, 1)), Convert.ToInt32((B.Text).Substring(0, 1)));
                        y_bb.num_history.Add(b_List);
                        You_List.Text = You_guess.Text + "   : " + S.Text + "  " + B.Text + System.Environment.NewLine + You_List.Text;
                        You.Text = "네가 맞춰봐";
                        if (S.Text == "4 S") You_List.Text = (y_bb.num_history.Count).ToString() + "회만에 정답" + System.Environment.NewLine + You_List.Text;
                    }                    
                }
                
            };

            

            Me_End.Click += delegate
            {
                if ("(야신 선택한 수)/ 다시하기" == Me_End.Text)
                {
                    Me_End.Text = "야산이 선택한 숫자:" + (i_bb.SNUM).ToString() + System.Environment.NewLine + "클릭하면 다시하기";
                }
                else
                {
                    i_bb = new I_BB();
                    Me_guess.Text = "";
                    Me_List.Text = "";                
                    Me_End.Text = "(야신 선택한 수)/ 다시하기";
                }

                
            };


            You_End.Click += delegate
            {
                y_bb = new Y_BB();
                You_guess.Text = "";
                You_List.Text = "";
                You.Text = "네가 맞춰봐";

            };

            Me_guess.Click += delegate
            {
                Me_guess.Text = "";
            };

            S.Click += delegate
            {
                switch (S.Text)
                {
                    case "0 S" :
                        S.Text = "1 S";
                        break;
                    case "1 S":
                        S.Text = "2 S";
                        break;
                    case "2 S":
                        S.Text = "3 S";
                        break;
                    case "3 S":
                        S.Text = "4 S";
                        break;
                    case "4 S":
                        S.Text = "0 S";
                        break;
                    default:
                        S.Text = "0 S";
                        break;
                }                
            };

            B.Click += delegate
            {
                switch (B.Text)
                {
                    case "0 B":
                        B.Text = "1 B";
                        break;
                    case "1 B":
                        B.Text = "2 B";
                        break;
                    case "2 B":
                        B.Text = "3 B";
                        break;
                    case "3 B":
                        B.Text = "4 B";
                        break;
                    case "4 B":
                        B.Text = "0 B";
                        break;
                    default:
                        B.Text = "0 B";
                        break;
                }
            };
            Me_Memo.Click += delegate
            {
                Me_Memo.Text = "";
            };
            You_Memo.Click += delegate
            {
                You_Memo.Text = "";
            };
        }
    }
}

