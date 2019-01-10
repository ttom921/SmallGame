using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppTestGuess
{
    public class GuessNumber
    {
        // 所有的答案組合
        int[] answerSet = new int[10 * 9 * 8 * 7];
        public GuessNumber()
        {
            Init();
        }
        void Init()
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
        }
        public void guess()
        {
            for (int i = 0; i < answerSet.Length; i++)
            {
                if (answerSet[i] == -1) continue;
                input(answerSet[i]);
            }
            Console.WriteLine("你騙人!! 根本沒這數字, ***!");
        }
        private void input(int number)
        {
            Console.Write(transform(number) + ", ?A?B = ");
            int a = 0, b = 0;
            try
            {
                String str = Console.ReadLine();
                while (str.Length != 4)
                {
                    Console.WriteLine("輸入錯誤, 格式為 ?A?B ");
                    Console.Write(transform(number) + ", ?A?B = ");
                    str = Console.ReadLine();
                }
                a = str[0] - '0';
                b = str[2] - '0';
            }
            catch (Exception e)
            {
                Console.WriteLine("輸入時發生不可預期的錯誤...");
                Environment.Exit(0);
            }
            if (a == 4)
            {
                Console.WriteLine("The answer is " + transform(number));
                return;
            }
            reduce(number, a, b);
        }
        private void reduce(int number, int a, int b)
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
                Console.Write(transform(answerSet[i]) + " ");
            }
            Console.WriteLine();
        }
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
