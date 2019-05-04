using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace HTTPProtocolFilter_GuiHelper
{
    public class LogClass
    {
        public string time;
        public string tag;
        public string url;
        public string referer;
        public string mimetype;
        public string reason;

        public override string ToString()
        {
            return url;
        }

        static JavaScriptSerializer serial = new JavaScriptSerializer();
        public static LogClass FromString(string line)
        {
            return serial.Deserialize<LogClass>(line);
        }

        public string getInfo()
        {
            return string.Format("[{0}]\nTag: {1}\nMIME: {2}\nReason: {3}\nURL: {4}\nReferer: {5}\n",
                time,tag, mimetype, reason, url, referer
                );
        }
    }
}
