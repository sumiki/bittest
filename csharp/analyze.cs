using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
 
public class Analyze
{

    private static ArrayList transactions = new ArrayList();
    private static int max_block_size = 1000000;

    private static int result_max_total = 0;
    private static ArrayList result_max_ids = new ArrayList();
    private static double result_total_fee = 0.0;

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
        SetCsvData();

        int countNum = transactions.Count;
        int[] numberArray = new int[countNum];
        for (int i = 0; i < countNum; i++)
        {
            numberArray[i] = i;
        }

        //foreach(var item in numberArray)
        //{
        //    Console.WriteLine(item.ToString());
        //}

        GetPer(numberArray);


        Console.WriteLine( "result_max_total:" + result_max_total.ToString() );

        foreach(var item in result_max_ids)
        {
            IDictionary dictionary = (IDictionary)transactions[(int)item];
            Console.WriteLine( (string)dictionary["id"] );
            //result_total_fee = result_total_fee + float.Parse((string)dictionary["fee"], System.Globalization.CultureInfo.InvariantCulture);
        }
        Console.WriteLine( "result_total_fee:" + string.Format("{0:#.0000}", result_total_fee ) + " + 12.5 = " + string.Format("{0:#.0000}", ( result_total_fee + 12.5 ) ) );
    }
    

    private static void Calc( int[] list )
    {
            int totalsize = 0;
            double totalfee = 0.0;
            ArrayList ids = new ArrayList();
            int i = 0;

            foreach(var item in list)
            {
                IDictionary dictionary = (IDictionary)transactions[item];
                int tmp_total = totalsize + Int32.Parse((string)dictionary["size"]);
                double tmp_fee = totalfee + float.Parse((string)dictionary["fee"], System.Globalization.CultureInfo.InvariantCulture);
                if( tmp_total <= max_block_size ){
                    ids.Add( item );
                    totalsize = tmp_total;
                    totalfee = tmp_fee;
                    i += 1;
                } else {
                    break;
                }
            }
            if( result_total_fee <= totalfee ){
                result_max_total = totalsize;
                result_max_ids = ids;
                result_total_fee = totalfee;                
            }

    }

    private IEnumerable<DictionaryEntry> CastDict(IDictionary dictionary)
    {
        foreach (DictionaryEntry entry in dictionary)
        {
            yield return entry;
        }
    }

    static private void SetCsvData ()
    {
        using (StreamReader sr = new StreamReader(@"./data.csv")) 
        {
            while (sr.Peek() >= 0) 
            {
                string line = sr.ReadLine();
                string[] lineitems = line.Split(',');
                Dictionary<string, string> d = new Dictionary<string, string>();
                d.Add( "id", lineitems[0] );
                d.Add( "size", lineitems[1] );
                d.Add( "fee", lineitems[2] );

                transactions.Add( d );
            }
        }
    }
}




