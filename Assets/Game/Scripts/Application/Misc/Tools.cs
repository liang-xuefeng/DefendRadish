using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using Debug = System.Diagnostics.Debug;

public class Tools
{
    /// <summary>
    /// 返回所有的关卡配置文件
    /// </summary>
    public static List<FileInfo> GetLevelFileInfos()
    {
        string[] files = Directory.GetFiles(Consts.LevelDir, "*.xml");
        List<FileInfo> leveList = new List<FileInfo>();
        foreach (string file in files)
        {
            FileInfo fileInfo = new FileInfo(file);
            leveList.Add(fileInfo);
        }
        return leveList;
    }

    /// <summary>
    /// 填充一个level数据类
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="level"></param>
    public static void FillLevel(string fileName, ref Level level)
    {
        FileInfo file = new FileInfo(fileName);
        StreamReader sr = new StreamReader(file.OpenRead(), Encoding.UTF8);

        XmlDocument doc = new XmlDocument();
        doc.Load(sr);

        level.Name = doc.SelectSingleNode("/Level/Name").InnerText;
        level.CardImage = doc.SelectSingleNode("/Level/CardImage").InnerText;
        level.Background = doc.SelectSingleNode("/Level/Background").InnerText;
        level.Road = doc.SelectSingleNode("/Level/Road").InnerText;
        level.InitScore = int.Parse(doc.SelectSingleNode("/Level/InitScore").InnerText);

        XmlNodeList nodes;

        nodes = doc.SelectNodes("/Level/Holder/Point");
        for (int i = 0; i < nodes.Count; i++)
        {
            XmlNode node = nodes[i];
            Point p = new Point(
                int.Parse(node.Attributes["X"].Value),
                int.Parse(node.Attributes["Y"].Value));

            level.Holders.Add(p);
        }

        nodes = doc.SelectNodes("/Level/Path/Point");
        for (int i = 0; i < nodes.Count; i++)
        {
            XmlNode node = nodes[i];

            Point p = new Point(
                int.Parse(node.Attributes["X"].Value),
                int.Parse(node.Attributes["Y"].Value));

            level.Path.Add(p);
        }

        nodes = doc.SelectNodes("/Level/Rounds/Round");
        for (int i = 0; i < nodes.Count; i++)
        {
            XmlNode node = nodes[i];

            Round r = new Round(
                    int.Parse(node.Attributes["Monster"].Value),
                    int.Parse(node.Attributes["Count"].Value)
                );

            level.Rounds.Add(r);
        }

        sr.Close();
        sr.Dispose();
    }

    /// <summary>
    /// 保存关卡数据到文件
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="level"></param>
    public static void SaveLevel(string fileName, Level level)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
        sb.AppendLine("<Level>");

        sb.AppendLine(string.Format("<Name>{0}</Name>", level.Name));
        sb.AppendLine(string.Format("<CardImage>{0}</CardImage>", level.CardImage));
        sb.AppendLine(string.Format("<Background>{0}</Background>", level.Background));
        sb.AppendLine(string.Format("<Road>{0}</Road>", level.Road));
        sb.AppendLine(string.Format("<InitScore>{0}</InitScore>", level.InitScore));

        sb.AppendLine("<Holder>");
        for (int i = 0; i < level.Holders.Count; i++)
        {
            sb.AppendLine(string.Format("<Point X=\"{0}\" Y=\"{1}\"/>", level.Holders[i].X, level.Holders[i].Y));
        }
        sb.AppendLine("</Holder>");

        sb.AppendLine("<Path>");
        for (int i = 0; i < level.Path.Count; i++)
        {
            sb.AppendLine(string.Format("<Point X=\"{0}\" Y=\"{1}\"/>", level.Path[i].X, level.Path[i].Y));
        }
        sb.AppendLine("</Path>");

        sb.AppendLine("<Rounds>");
        for (int i = 0; i < level.Rounds.Count; i++)
        {
            sb.AppendLine(string.Format("<Round Monster=\"{0}\" Count=\"{1}\"/>", level.Rounds[i].Monster, level.Rounds[i].Count));
        }
        sb.AppendLine("</Rounds>");

        sb.AppendLine("</Level>");

        string content = sb.ToString();

        //xml文件修饰
        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Indent = true;
        settings.ConformanceLevel = ConformanceLevel.Auto;
        settings.IndentChars = "\t";
        settings.OmitXmlDeclaration = false;

        XmlWriter xw = XmlWriter.Create(fileName, settings);

        XmlDocument doc = new XmlDocument();
        doc.LoadXml(content);
        doc.WriteTo(xw);

        xw.Flush();
        xw.Close();
    }

    /// <summary>
    /// 加载图片
    /// </summary>
    /// <param name="url"></param>
    /// <param name="spriteRenderer"></param>
    /// <returns></returns>
    public static IEnumerator LoadImage(string url, SpriteRenderer spriteRenderer)
    {
        WWW www = new WWW(url);
        while (!www.isDone)
            yield return www;

        Texture2D texture2D = www.texture;
        Sprite sprite = Sprite.Create(texture2D,new Rect(0,0, texture2D.width, texture2D.height), new Vector2(0.5f,0.5f));
        spriteRenderer.sprite = sprite;
    }

    /// <summary>
    /// 加载图片
    /// </summary>
    /// <param name="url"></param>
    /// <param name="image"></param>
    /// <returns></returns>
    public static IEnumerator LoadImage(string url, Image image)
    {
        WWW www = new WWW(url);
        while (!www.isDone)
            yield return www;

        Texture2D texture2D = www.texture;
        Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
        image.sprite = sprite;
    }
}
