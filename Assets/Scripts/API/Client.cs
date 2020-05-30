
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace h8s.api
{
    public class Client
    {
        private static Client instance;
        public static Client Instance
        {
            get
            {
                if (null == instance)
                {
                    instance = new Client();
                }
                return instance;
            }
        }

        public IEnumerator LoadNodes()
        {
            string url = "http://my-json-server.typicode.com/stopa323/h8s-fake-api/nodes";


            using (UnityWebRequest www = UnityWebRequest.Get(url))
            {
                yield return www.SendWebRequest();
                if (www.isNetworkError)
                {
                    Debug.LogError(www.error);
                }
                else if (www.isDone)
                {
                    string jsonResult = Encoding.UTF8.GetString(www.downloadHandler.data);
                    NodeContainer nodeContainer = JsonUtility.FromJson<NodeContainer>(jsonResult);

                    foreach (var node in nodeContainer.nodes)
                    {
                        SchemeManager.Instance.InstantiateNode(node);
                    }
                }
            }
        }
    }
}
