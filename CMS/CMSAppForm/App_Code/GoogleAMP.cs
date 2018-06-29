using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for GoogleAMP
/// </summary>
public class GoogleAmp
{
    private readonly string _source;
    private static string _result;
    private static HtmlDocument _htmlDoc = new HtmlDocument();
    private List<HtmlNode> _deleteNodes = new List<HtmlNode>();
    private readonly List<string> _notToRemove = new List<string> { "br" };
    private readonly IDictionary<string, string[]> _whitelist = new Dictionary<string, string[]> 
        {
            {
                "img", new[] { "src","alt","title" ,"class","layout", "width","height"}
            },
            { "span", new[] {"class" } },
            { "p", new[] {"class" } },
            { "ul", new[] {"class" } },
            { "ol", new[] {"class" } },
            { "li", new[] {"class" } },
            { "div", new[] { "class" } },
            { "u", new[] {"class" } },
            { "table", new[] {"class", "align" } },
            { "tr", new[] {"class" } },
            { "td", new[] {"class" } },
            { "th", new[] {"class" } }
        };

    private readonly IDictionary<string, string[]> _blacklist = new Dictionary<string, string[]> 
        {
            {"font", null},
            {"meta", null},
            {"style", null},
            {"u1:p", null},
            {"st1:date", null},
            {"st1:country-region", null},
            {"st1:place", null},
            {"st1:placename", null}
        };

    private GoogleAmp(string source)
    {
        this._source = source;
    }

    public static string Convert(string source)
    {
        var converter = new GoogleAmp(source);
        converter.Convert();
        return _result;
    }

    private void Convert()
    {
        _htmlDoc = GetHtmlDocument(this._source);
        CleanNode(_htmlDoc.DocumentNode);
        RemoveInlineStyles();
        UpdateAmpImages();
    }

    private void UpdateAmpImages()
    {
        IEnumerable<HtmlNode> imageList = _htmlDoc.DocumentNode.Descendants("img");
        int wSize = 300, hSize = 240;
        string urlImage = string.Empty;
        if (imageList!=null && imageList.Any())
        {
            if (!HtmlNode.ElementsFlags.ContainsKey("amp-img"))
            {
                HtmlNode.ElementsFlags.Add("amp-img", HtmlElementFlag.Closed);
            }
            foreach (var imgTag in imageList)
            {
                var original = imgTag.OuterHtml;
                var replacement = imgTag.Clone();
                replacement.Name = "amp-img";
                HtmlAttribute srcAttribute = imgTag.Attributes.FirstOrDefault(i => i.Name.Equals("src"));
                if (srcAttribute != null)
                {
                    urlImage = srcAttribute.Value;
                }
                if (replacement.Attributes["width"] == null)
                {
                    getSizeOfImage(urlImage, ref wSize, ref hSize);
                    hSize = (int)(hSize / ((Double)wSize / 320));
                    replacement.SetAttributeValue("width", "320");
                }
                if (replacement.Attributes["height"] == null)
                {
                    replacement.SetAttributeValue("height", hSize.ToString());
                }
                replacement.Attributes.Add("class", "-amp-element -amp-layout-responsive -amp-layout-size-defined -amp-layout");
                replacement.SetAttributeValue("layout", "responsive");
                _result = _result.Replace(original, replacement.OuterHtml);
            }
        }
    }

    private void RemoveInlineStyles()
    {
        var elements = _htmlDoc.DocumentNode.Descendants();
        foreach (var htmlNode in elements)
        {
            if (htmlNode.Attributes["style"] == null)
            {
                continue;
            }
            htmlNode.Attributes.Remove(htmlNode.Attributes["style"]);
        }
        var builder = new StringBuilder();
        var writer = new StringWriter(builder);
        _htmlDoc.Save(writer);
        _result = builder.ToString();
    }

    private static HtmlDocument GetHtmlDocument(string source)
    {
        HtmlDocument htmlDoc = new HtmlDocument
        {
            OptionFixNestedTags = true,
            OptionAutoCloseOnEnd = true,
            OptionDefaultStreamEncoding = Encoding.UTF8
        };
        htmlDoc.LoadHtml(source);
        return htmlDoc;
    }

    private void getSizeOfImage(string url, ref int width, ref int height)
    {
        width = 320;
        height = 240;
        try
        {
            byte[] imageData = new WebClient().DownloadData(url);
            MemoryStream imgStream = new MemoryStream(imageData);
            Image img = Image.FromStream(imgStream);

            width = img.Width;
            height = img.Height;
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
        }

    }

    private void CleanNode(HtmlNode node)
    {
        CleanAttribuesUnSupport(node);
        for (int i = _deleteNodes.Count - 1; i >= 0; i--)
        {
            HtmlNode nodeToDelete = _deleteNodes[i];
            nodeToDelete.ParentNode.RemoveChild(nodeToDelete, true);
        }
    }

    private void CleanAttribuesUnSupport(HtmlNode node)
    {
        if (node.NodeType == HtmlNodeType.Element)
        {
            if (_blacklist.ContainsKey(node.Name))
            {
                _deleteNodes.Add(node);
            }
            else if (node.HasAttributes)
            {
                for (int i = node.Attributes.Count - 1; i >= 0; i--)
                {
                    HtmlAttribute currentAttribute = node.Attributes[i];
                    if (_whitelist.ContainsKey(node.Name))
                    {
                        string[] allowedAttributes = _whitelist[node.Name];
                        if (allowedAttributes != null)
                        {
                            //xóa thuộc tính không hỗ trợ
                            if (!allowedAttributes.Contains(currentAttribute.Name))
                                node.Attributes.Remove(currentAttribute);
                            //Xóa ảnh cũ
                            if (currentAttribute.Name.Equals("src") && currentAttribute.Value.IndexOf("blank.gif", StringComparison.Ordinal) >= 0)
                            {
                                _deleteNodes.Add(node);
                            }
                        }
                        else
                        {
                            node.Attributes.Remove(currentAttribute);
                        }
                    }
                }
            }
        }
        // xóa node comment và parrent của nó
        if (node.NodeType == HtmlNodeType.Comment)
        {
            var parentNodeRemove = node.ParentNode;
            node.Remove();
            if (parentNodeRemove != null && parentNodeRemove.Attributes.Count == 0 && !_notToRemove.Contains(parentNodeRemove.Name) && string.IsNullOrEmpty(parentNodeRemove.InnerText))
            {
                parentNodeRemove.ParentNode.RemoveChild(parentNodeRemove);
            }
        }
        // đệ quy 
        if (node.HasChildNodes)
        {
            node.ChildNodes.ToList().ForEach(v => CleanAttribuesUnSupport(v));
        }
    }

}
