using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Permutation
{

	private static int transactions;

    private static void Swap(ref int a, ref int b)
    {
        if (a == b) return;

        a ^= b;
        b ^= a;
        a ^= b;
    }

    public static void GetPer(int[] list)
    {
        int x = list.Length - 1;
        GetPer(list, 0, x);
    }

    private static void GetPer(int[] list, int k, int m)
    {
        if (k == m)
        {
        	Calc( list );

        }
        else
            for (int i = k; i <= m; i++)
            {
                   Swap(ref list[k], ref list[i]);
                   GetPer(list, k + 1, m);
                   Swap(ref list[k], ref list[i]);
            }
    }


    static public void Main ()
    {
        int countNum = 4;
        int[] numberArray = new int[countNum];
        for (int i = 0; i < countNum; i++)
		{
			numberArray[i] = i;
		}

		transactions = 5;

		//foreach(var item in numberArray)
		//{
		//    Console.WriteLine(item.ToString());
		//}

        GetPer(numberArray);
    }

    private static void Calc( int[] list )
    {
    	    Console.WriteLine("---");

			foreach(var item in list)
			{
			    Console.WriteLine(item.ToString());
			}

			Console.WriteLine(transactions);
    }

}

