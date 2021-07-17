using System;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace BitcoinFastSearch.Balance
{
    public class Balance
    {
        public class BalnceData
        {
            public string AllBalance{ get; set; }
            public  string ExistBalance{ get; set; }

        }
        
        private class PBData
        {
            
            public   Int64 funded_txo_count{ get; set; }
            public  Int64 funded_txo_sum{ get; set; }
            public   Int64 spent_txo_count{ get; set; }
            public   Int64 spent_txo_sum{ get; set; }
            public  Int64 tx_count{ get; set; }
            
        }
        
        
        public static BalnceData Get(string address)
        {
           
            string res = default;
            using (WebClient webClient = new WebClient())
            {
                res= webClient.DownloadString(
                    $"https://blockstream.info/api/address/{address}");
                       
            }

            string value = Regex.Match(res, @":{""f([\w \W A-Z a-z \: 0-9]+)},""me").Groups[1].Value;

            string value2 = Regex.Match(res, @"""mempool_stats"":{([\w \W A-Z a-z \: 0-9]+)}}").Groups[1].Value;
            
            var parsingJsonDataAllBalance= JsonSerializer.Deserialize<PBData>(@"{""f"+value+"}");
            var parsingJsonDataExistBalance= JsonSerializer.Deserialize<PBData>(@"{"+value2+"}");

            
            
            BalnceData balnceData = new BalnceData()
            {
                AllBalance = parsingJsonDataAllBalance.funded_txo_sum.ToString(),
                ExistBalance = parsingJsonDataExistBalance.funded_txo_sum.ToString()
                
            };

            return balnceData;
        }


    }
}