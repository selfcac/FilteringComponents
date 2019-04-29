//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class JSONBaseClass
    {
        public DateTime created = DateTime.Now;

        public ConnectionHelpers.TaskInfo ToFile(string filename)
        {
            ConnectionHelpers.TaskInfo result = ConnectionHelpers.TaskInfo.Fail("init");
            try
            {
                //string json = JsonConvert.SerializeObject(this, Formatting.Indented);
                string json =
                    new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(this);

                if (File.Exists(filename))
                    File.Delete(filename); // O.W might write only in the beginning of the file.
                File.WriteAllText(filename, json);
                result = ConnectionHelpers.TaskInfo.Success("Json was saved");
            }
            catch (Exception ex)
            {
                result = ConnectionHelpers.TaskInfo.Fail(ex.Message);
            }

            return result;
        }

        public static ConnectionHelpers.TaskInfo FromFile<T>(string filename)
        {
            ConnectionHelpers.TaskInfo result = ConnectionHelpers.TaskInfo.Fail("init");
            try
            {
                if (File.Exists(filename))
                {
                    string json = File.ReadAllText(filename);
                    //T obj = JsonConvert.DeserializeObject<T>(json);
                    T obj =
                        new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<T>(json);
                    result = ConnectionHelpers.TaskInfoResult<T>.Result(obj);
                }
                else
                {
                    result = ConnectionHelpers.TaskInfo.Fail("Json file not found");
                }
            }
            catch (Exception ex)
            {
                result = ConnectionHelpers.TaskInfo.Fail(ex.Message);
            }

            return result;
        }
    }
}
