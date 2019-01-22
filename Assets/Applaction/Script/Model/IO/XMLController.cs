using UnityEngine;

public class XMLController : MonoBehaviour{
    [SerializeField]
    private TextAsset m_xmlAsset = null;
    [SerializeField]
    private string m_xmlFilePath = null;


    private void Start()
    {
        if (m_xmlAsset != null)
        {
            LoadFromText(m_xmlAsset.text);
        }

        if (!string.IsNullOrEmpty(m_xmlFilePath))
        {
            LoadFromPath(m_xmlFilePath);
        }
    }

    /// <summary>
    /// xmlのテキストデータを読み込む場合。
    /// </summary>
    private void LoadFromText(string i_xmlText)
    {
        try
        {
            System.Xml.Linq.XDocument xml = System.Xml.Linq.XDocument.Parse(i_xmlText);
            System.Xml.Linq.XElement root = xml.Root;

            // xmlデータをログに表示するよ。
            ShowData(root);

            // 名前を指定して検索する場合は、この関数を使おう！
            var titles = root.Descendants("title");
            if (titles != null)
            {
                foreach (var title in titles)
                {
                    string nameText = title.Name.LocalName;
                    string valueText = title.Value;
                    Debug.LogFormat("name:{0}, value:{1}", nameText, valueText);
                }
            }
        }
        catch (System.Exception i_exception)
        {
            Debug.LogErrorFormat("エラー : {0}", i_exception);
        }
    }

    /// <summary>
    /// xmlのファイルパスから読み込む場合。
    /// </summary>
    private void LoadFromPath(string i_path)
    {
        try
        {
            System.Xml.Linq.XDocument xml = System.Xml.Linq.XDocument.Load(i_path);
            System.Xml.Linq.XElement root = xml.Root;

            // xmlデータをログに表示するよ。
            ShowData(root);

            // 名前を指定して検索する場合は、この関数を使おう！
            var titles = root.Descendants("title");
            if (titles != null)
            {
                foreach (var title in titles)
                {
                    string nameText = title.Name.LocalName;
                    string valueText = title.Value;
                    Debug.LogFormat("name:{0}, value:{1}", nameText, valueText);
                }
            }
        }
        catch (System.Exception i_exception)
        {
            Debug.LogErrorFormat("エラー : {0}", i_exception);
        }
    }

    private void ShowData(System.Xml.Linq.XElement i_element)
    {
        string nameText = i_element.Name.LocalName;
        string valueText = i_element.Value;
        string atrText = string.Empty;

        // アトリビュートがある場合。
        if (i_element.HasAttributes)
        {
            foreach (var attribute in i_element.Attributes())
            {
                atrText += string.Format("\t\t{0}={1}\n", attribute.Name, attribute.Value);
            }
        }

        Debug.LogFormat("name:{0}\n\tvalue:{1}\n\tattribute:\n{2}", nameText, valueText, atrText);

        // 子ノードも探そう。
        foreach (var child in i_element.Elements())
        {
            ShowData(child);
        }
    }

} // class XMLController