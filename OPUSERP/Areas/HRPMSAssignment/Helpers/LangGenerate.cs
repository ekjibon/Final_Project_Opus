using Newtonsoft.Json;
using System.IO;

namespace OPUSERP.Areas.HRPMSAssignment.Helpers
{
    public class LangGenerate<T>
    {
        private T genericClassObj;
        private readonly string rootpath;

        public LangGenerate(string rootpath)
        {
            this.rootpath = rootpath;
        }

        public T PerseLang(string filename)
        {
            using (StreamReader r = new StreamReader(rootpath + "/wwwroot/Lang/" + filename))
            {
                string json = r.ReadToEnd();
                genericClassObj = JsonConvert.DeserializeObject<T>(json);
            }
            return genericClassObj;
        }
    }
}
