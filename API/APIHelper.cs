using API.Implementation;
using API.Model;
using System.Data;

namespace API
{
    class APIHelper
    {
        public SaveResults[] Save(string DataContent)
        {
            return new ObjectApi().Save(DataContent);
        }

        public DataSet Query(string DataContent)
        {
            return new ObjectApi().Query(DataContent);
        }

        public DataSet QueryNext(string ObjectType,int PageNo, int PageSize)
        {
            return new ObjectApi().QueryNext(ObjectType, PageNo, PageSize);
        }

        public DataSet QueryAll(string ObjectType)
        {
            return new ObjectApi().QueryAll(ObjectType);
        }
    }
}
