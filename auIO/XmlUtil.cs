using System.Xml;

namespace au.util.io {
  /// <summary>
  /// Static class which provides functions for working with XML
  /// </summary>
  public static class XmlUtil {
		/// <summary>
		/// Encode a string to work with HTML or XML.
		/// </summary>
		/// <param name="s">String to encode</param>
		/// <returns>Encoded string</returns>
		public static string XmlEncode(this string s) {
			return s.Replace("&", "&amp;").Replace("\"", "&quot;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("  ", "&nbsp; ");
		}
		
		/// <summary>
		/// Add a new XML element as a child of another node.
		/// </summary>
		/// <param name="parent">Node to add new element under</param>
		/// <param name="name">Name of new element</param>
		/// <param name="innerText">Text to add with new element</param>
		/// <returns>New element</returns>
		public static XmlElement AddElement(this XmlNode parent, string name, object innerText) {
			return AddElement(parent, name, innerText.ToString());
		}

		/// <summary>
		/// Add a new XML element as a child of another node.
		/// </summary>
		/// <param name="parent">Node to add new element under</param>
		/// <param name="name">Name of new element</param>
		/// <param name="innerText">Text to add with new element</param>
		/// <returns>New element</returns>
		public static XmlElement AddElement(this XmlNode parent, string name, string innerText) {
		  XmlElement e = parent.OwnerDocument.CreateElement(name);
			if(innerText != null)
				e.InnerText = innerText;
			parent.AppendChild(e);
			return e;
		}

		/// <summary>
		/// Add a new XML element as a child of another node.
		/// </summary>
		/// <param name="parent">Node to add new element under</param>
		/// <param name="name">Name of new element</param>
		/// <returns>New element</returns>
		public static XmlElement AddElement(this XmlNode parent, string name) {
			return AddElement(parent, name, null);
		}

		/// <summary>
		/// Add an attribute to an XML element
		/// </summary>
		/// <param name="el">XML Element to add an attribute to</param>
		/// <param name="name">Name of attribute to add</param>
		/// <param name="val">Value of attribute to add</param>
		/// <returns>New attribute</returns>
		public static XmlAttribute AddAttribute(this XmlElement el, string name, object val) {
			return AddAttribute(el, name, val.ToString());
		}

		/// <summary>
		/// Add an attribute to an XML element
		/// </summary>
		/// <param name="el">XML Element to add an attribute to</param>
		/// <param name="name">Name of attribute to add</param>
		/// <param name="val">Value of attribute to add</param>
		/// <returns>New attribute</returns>
		public static XmlAttribute AddAttribute(this XmlElement el, string name, string val) {
			XmlAttribute a = el.OwnerDocument.CreateAttribute(name);
			a.Value = val;
			el.Attributes.Append(a);
			return a;
		}

    /// <summary>
    /// Gets the trimmed content of an XML element.
    /// </summary>
    /// <param name="element">Base element</param>
    /// <param name="elementName">Name of subelement to read value from (multiple subelements can be used separated by forward slash)</param>
    /// <returns>Trimmed content of an XML element</returns>
    public static string ElementValue(this XmlElement element, string elementName) {
      return ElementValue(element, elementName, null);
    }
    /// <summary>
    /// Gets the trimmed content of an XML element.
    /// </summary>
    /// <param name="element">Base element</param>
    /// <param name="elementName">Name of subelement to read value from (multiple subelements can be used separated by forward slash)</param>
    /// <param name="defaultValue">Returned if the element does not exist or is empty</param>
    /// <returns>Trimmed content of an XML element</returns>
    public static string ElementValue(this XmlElement element, string elementName, string defaultValue) {
      XmlElement e = element;
      foreach(string name in elementName.Split('/')) {
        e = e[name];
        if(e == null || !e.HasChildNodes)
          return defaultValue;
      }
      return e.FirstChild.Value.Trim();
    }
  }
}
