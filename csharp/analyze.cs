using System;
using System.IO;
using System.Collections;

 
public class Analyze
{
    static public void Main ()
    {

            ArrayList transactions = new ArrayList();

            using (StreamReader sr = new StreamReader(@"./data_test.csv")) 
            {
                while (sr.Peek() >= 0) 
                {
                    string line = sr.ReadLine();
                    string[] lineitems = line.Split(',');
                    Hashtable ht = new Hashtable();
                    ht.Add( "id", lineitems[0] );
                    ht.Add( "size", lineitems[1] );
                    ht.Add( "fee", lineitems[2] );

                    transactions.Add( ht );
                }
            }

            Console.WriteLine( transactions.Count );


    }
}




