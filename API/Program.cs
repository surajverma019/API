using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Web;
using API.Implementation;
using System.Data;
namespace API
{
    class Program
    {

        static void Main(string[] args)
        {
            string data = "{\"ItemType\": \"Department\",\"Details\": [{\"DepartmentID\": \"2\"},{\"DepartmentID\": \"2\"}]}";
            //string data = "{\"ItemType\": \"Department\",\"Details\": [{\"DepartmentName\": \"IT\"},{\"DepartmentName\": \"Business\"}]}";
            //string data = "{\"ItemType\": \"Department\",\"Details\": [{\"DepartmentName\": \"IT\",\"DepartmentID\":\"1\"},{\"DepartmentName\": \"Business\",\"DepartmentID\":\"1\"}]}";
            //string data = "{\"ItemType\": \"Department\",\"Details\": {\"DepartmentName\": \"Business\"}}";
            JObject obj = JObject.Parse(data);
            ObjectApi api = new ObjectApi();
            Console.WriteLine(DateTime.Now.ToString("MM/dd/yy H:mm:ss zzz"));
            //SaveResults [] results = api.Save(obj + "");
            Console.WriteLine(DateTime.Now.ToString("MM/dd/yy H:mm:ss zzz"));
            //Console.WriteLine(JsonConvert.SerializeObject(results, Formatting.Indented));
            // DataSet ds = api.Query(data);
            //Console.WriteLine(JsonConvert.SerializeObject(ds, Formatting.Indented));

            //int From, Too, PageSize, PageNo;
            //PageSize = 15;
            //PageNo = 5;
            //Too = PageSize * PageNo;
            //From = Too - (PageSize - 1);
            //Console.WriteLine("From: " + From + " Too: " + Too);

            //DataSet ds = api.QueryNext("Department", 1, 2);
            //Console.WriteLine(JsonConvert.SerializeObject(ds, Formatting.Indented));

            DataSet ds = api.QueryAll("Department");
            Console.WriteLine(JsonConvert.SerializeObject(ds, Formatting.Indented));

            Console.ReadKey();
        }
    }
}
