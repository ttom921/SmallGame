using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallGameLib
{
    public class GuessNumber
    {
        public delegate void WriteLine(string s);
        public WriteLine MessageLog;
        // 所有的答案組合
        int[] answerSet = new int[10 * 9 * 8 * 7];
        int gessindex = -1;
        /// <summary>
        /// 產生所有的答案組合
        /// </summary>
        public void GenAnswerNum()
        {
            int index = 0;
            for (int n1 = 0; n1 < 10; n1++)
            {
                for (int n2 = 0; n2 < 10; n2++)
                {
                    if (n2 == n1) continue;
                    for (int n3 = 0; n3 < 10; n3++)
                    {
                        if (n3 == n2 || n3 == n1) continue;
                        for (int n4 = 0; n4 < 10; n4++)
                        {
                            if (n4 == n1 || n4 == n2 || n4 == n3) continue;
                            answerSet[index++] = n1 * 1000 + n2 * 100 + n3 * 10 + n4;
                        }
                    }
                }
            }
            if(MessageLog!=null)
            {
                MessageLog("產生所有的答案組合");
            }
            gessindex = 0;
        }
        public string GetGestNumber()
        {
            return transform(answerSet[gessindex]);
        }
        
        public void GetNextGuessNumber()
        {
            for (int i = 0; i < answerSet.Length; i++)
            {
                if (answerSet[i] != -1)
                {
                    gessindex = i;
                    break;
                }
            }
        }
        public void reduce(int number, int a, int b)
        {
            for (int i = 0; i < answerSet.Length; i++)
            {
                if (answerSet[i] == -1) continue;
                if (getA(number, answerSet[i]) != a || getB(number, answerSet[i]) != b)
                    answerSet[i] = -1;
            }
            for (int i = 0; i < answerSet.Length; i++)
            {
                if (answerSet[i] == -1) continue;
                MessageLog(transform(answerSet[i]) + " ");
            }
        }
        //取得和此數字有相同位置
        int getA(int n1, int n2)
        {
            int a = 0;
            String str1 = transform(n1);
            String str2 = transform(n2);
            for (int i = 0; i < 4; i++)
            {
                if (str1[i] == str2[i])
                    a++;
            }
            return a;
        }
        //取得此睥字有相同數字的
        private int getB(int n1, int n2)
        {
            int b = 0;
            String str1 = transform(n1);
            String str2 = transform(n2);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (i == j) continue;
                    if (str1[i] == str2[j])
                        b++;
                }
            }
            return b;
        }
        String transform(int n)
        {
            if (n < 1000)
                return "0" + n;
            else
                return "" + n;
        }
    }
}
