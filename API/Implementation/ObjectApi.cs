using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using API.Model;
using System.Data;

namespace API.Implementation
{
    class ObjectApi
    {
        DataBaseAccess dba = new DataBaseAccess();
        List<string> par = new List<string>();
        List<string> val = new List<string>();
        List<string> col = new List<string>();
        JObject obj;
        JArray jarray;
        string ObjectType = "", query = "";

        #region SaveDefination 
        public SaveResults[] Save(string DataContent)
        {
            obj = JObject.Parse(DataContent);
            ObjectType = obj["ItemType"] + "";
            jarray = JArray.Parse(obj["Details"] + "");
            return Save();
        }

        public SaveResults[] Save()
        {
            List<SaveResults> results = new List<SaveResults>();
            try
            {
                FormQuery();
                for (int i = 0; i < jarray.Count; i++)
                {
                    SaveResults result = new SaveResults();
                    val = AddVal(ref i);
                    result.IsSuccess = dba.Insert_update_delete_param_query(query, par.ToArray(), val.ToArray());
                    if (result.IsSuccess != 0)
                        result.Message = dba.error;
                    else
                        result.Message = "Success";
                    results.Add(result);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return results.ToArray();
        }
        public void FormQuery()
        {
            foreach (var pair in (JObject)jarray[0])
            {
                col.Add(pair.Key);
                par.Add("@" + pair.Key);
            }
            if (string.IsNullOrEmpty(jarray[0][ObjectType + "ID"] + ""))
            {
                col.Remove(ObjectType + "ID");
                par.Remove("@" + ObjectType + "ID");
                query = "Insert into " + ObjectType + "(" + string.Join(",", col) + ",InsertDate) values (" + string.Join(",", par) + ",getDate())";
            }
            else
                query = "update " + ObjectType + " set " + createPare((JObject)jarray[0]) + ",LastModifiedDate=GetDate() where " + ObjectType + "ID" + "=@" + ObjectType + "ID";
            //Console.WriteLine(query);
        }

        public List<string> AddVal(ref int index)
        {
            val = new List<string>();
            foreach (var pair in (JObject)jarray[index])
                val.Add(pair.Value + "");
            return val;
        }

        public string createPare(JObject ja)
        {
            string name = "";
            foreach (var x in ja)
            {
                if (!x.Key.Equals(ObjectType + "ID"))
                    name += x.Key + "=@" + x.Key + ",";
            }
            name = name.Substring(0, name.Length - 1);
            return name;
        }
        #endregion

        #region QueryDefination
        public DataSet Query(string DataContent)
        {
            obj = JObject.Parse(DataContent);
            ObjectType = obj["ItemType"] + "";
            jarray = JArray.Parse(obj["Details"] + "");
            query = "select * from " + ObjectType + " where " + ObjectType + "ID=@p1";
            par.Add("@p1");
            val.Add(jarray[0][ObjectType + "ID"] + "");
            return dba.get_dataset_by_param_query(query, par.ToArray(), val.ToArray());
        }
        #endregion

        #region QueryNextDefination
        public DataSet QueryNext(string ObjectType, int PageNo ,int PageSize)
        {
            int From, Too;
            Too = PageSize * PageNo;
            From = Too - (PageSize - 1);
            query = "WITH tmp_db AS( select row_number() over(order by "+ ObjectType + "ID desc) AS 'RowNumber',* from " + ObjectType + " )SELECT * FROM tmp_db WHERE  RowNumber BETWEEN " + From + " AND " + Too + "  order by RowNumber ";
            return dba.get_dataset_by_query(query);
        }
        #endregion

        #region QueryAll
        public DataSet QueryAll(string ObjectType)
        {
            query = "WITH tmp_db AS( select row_number() over(order by " + ObjectType + "ID desc) AS 'RowNumber',* from " + ObjectType + " )SELECT * FROM tmp_db order by RowNumber desc";
            return dba.get_dataset_by_query(query);
        }

        #endregion
    }
}
