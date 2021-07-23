using System;
using BitcoinFastSearch.Balance;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace BitcoinFastSearch.AddressData
{
    
    public class Address
    {
       public class Data
        {
            
            public string Address{ get; set; } 
            public  string PrivateKey{ get; set; }

        }



       public static void Write(string path,List<Data> addressData)
       {
           using (FileStream streamWriter = new FileStream(path + "/res.txt", FileMode.Append, FileAccess.Write))
           {
               for (int i = 0; i < addressData.Count; i++)
               {
                   streamWriter.Write(Encoding.Default.GetBytes($"Address:{addressData[i].Address} PrivateKey:{addressData[i].PrivateKey} \n"));
               }
          
           }
           
       }


       public static List<Data> Read(string patch)
        {
            List<Data> allAddressData = new List<Data>();
            StreamReader streamReader = new StreamReader(patch);
            while (!streamReader.EndOfStream)
            {
                
               var stringData = JsonSerializer.Deserialize<List<string>>(streamReader.ReadLine());
               Data data = new Data(){Address = stringData[0],PrivateKey = stringData[1]}; 
               allAddressData.Add(data);
            }





            return allAddressData;
        }


    }
}