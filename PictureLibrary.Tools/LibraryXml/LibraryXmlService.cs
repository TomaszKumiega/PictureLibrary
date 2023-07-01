using PictureLibrary.Tools.XamlSerializer;
using PictureLibraryModel.Model;
using System.Xml;

namespace PictureLibrary.Tools.LibraryXml
{
    public class LibraryXmlService<TLibrary> : ILibraryXmlService<TLibrary>
        where TLibrary : Library, new()
    {
        #region Private fields
        private readonly IXmlSerializer _xmlSerializer;
        #endregion

        public LibraryXmlService(
            IXmlSerializer xmlSerializer)
        {
            _xmlSerializer = xmlSerializer;   
        }

        private string LibraryTypeName => typeof(TLibrary).Name;

        #region Public methods
        public string AddImageFileNode<TImageFile>(string xml, TImageFile imageFile) 
            where TImageFile : ImageFile, new()
        {
            var xmlDocument = LoadXmlDocument(xml);

            var imageFilesNode = SelectImageFilesNode(xmlDocument);
            imageFilesNode = CreateImageFilesNodeIfItDoesntExist(xmlDocument, imageFilesNode);

            var serializedImageFile = _xmlSerializer.SerializeToString(imageFile);

            XmlDocument imageFileXmlDocument = LoadXmlDocument(serializedImageFile);
            XmlNode imageFileNode = imageFileXmlDocument.SelectSingleNode($"/{typeof(TImageFile)}") ?? throw new ArgumentException("Image file could not be serialized.", nameof(imageFile));

            _ = AppendNodeToANode(xmlDocument, imageFilesNode, imageFileNode);

            return xmlDocument.OuterXml;
        }

        public string AddTagNode(string xml, Tag tag)
        {
            var xmlDocument = LoadXmlDocument(xml);

            var tagsNode = SelectTagsNode(xmlDocument);
            tagsNode = CreateTagsNodeIfItDoesntExist(xmlDocument, tagsNode);

            var serializedTag = _xmlSerializer.SerializeToString(tag);

            XmlDocument tagXmlDocument = LoadXmlDocument(serializedTag);
            XmlNode tagNode = tagXmlDocument.ChildNodes?[1] ?? throw new ArgumentException(nameof(tag));

            _ = AppendNodeToANode(xmlDocument, tagsNode, tagNode);

            return xmlDocument.OuterXml;
        }

        public IEnumerable<TImageFile> GetImageFiles<TImageFile>(string xml)
            where TImageFile : ImageFile, new()
        {
            XmlDocument xmlDocument = LoadXmlDocument(xml);
            XmlNodeList? imageFileNodes = xmlDocument.SelectNodes($"/{typeof(TImageFile).Name}");

            if (imageFileNodes == null)
                return Enumerable.Empty<TImageFile>();

            var imageFiles = new List<TImageFile>();

            foreach (var node in imageFileNodes.Cast<XmlNode>())
            {
                var imageFile = _xmlSerializer.DeserializeFromString<TImageFile>(node.OuterXml);
                
                if (imageFile == null)
                    continue;

                imageFiles.Add(imageFile);
            }

            return imageFiles;
        }

        public IEnumerable<Tag> GetTags(string xml)
        {
            XmlDocument xmlDocument = LoadXmlDocument(xml);
            XmlNodeList? tagNodes = xmlDocument.SelectNodes($"{typeof(TLibrary).Name}/tags/{nameof(Tag)}");

            if (tagNodes == null || tagNodes.Count == 0)
                return Enumerable.Empty<Tag>();

            var tags = new List<Tag>();

            foreach (var node in tagNodes.Cast<XmlNode>())
            {
                var tag = _xmlSerializer.DeserializeFromString<Tag>(node.OuterXml);

                if (tag == null)
                    continue;

                tags.Add(tag);
            }

            return tags;
        }

        public string RemoveImageFileNode<TImageFile>(string xml, TImageFile imageFile) 
            where TImageFile : ImageFile, new()
        {
            XmlDocument xmlDocument = LoadXmlDocument(xml);
            XmlNodeList? imageFileNodes = xmlDocument.SelectNodes($"/{typeof(TImageFile).Name}");

            if (imageFileNodes == null)
                return xml;

            var imageFileNode = imageFileNodes
                .Cast<XmlNode>()
                .FirstOrDefault(x => x.Attributes?["id"]?.Value == imageFile.Id.ToString());

            if (imageFileNode == null)
                return xml;

            var imageFilesNode = imageFileNode.ParentNode!;

            imageFilesNode.RemoveChild(imageFileNode);

            return xmlDocument.OuterXml;
        }

        public string RemoveTagNode(string xml, Tag tag)
        {
            XmlDocument xmlDocument = LoadXmlDocument(xml);
            XmlNodeList? tagNodes = xmlDocument.SelectNodes($"{nameof(Tag)}");

            if (tagNodes == null)
                return xml;

            var tagNode = tagNodes
                .Cast<XmlNode>()
                .FirstOrDefault(x => x.Attributes?["id"]?.Value == tag.Id.ToString());

            if (tagNode == null)
                return xml;

            var tagsNode = tagNode.ParentNode!;

            tagsNode.RemoveChild(tagNode);

            return xmlDocument.OuterXml;
        }

        public string UpdateImageFileNode<TImageFile>(string xml, TImageFile imageFile) 
            where TImageFile : ImageFile, new()
        {
            var xmlAfterRemove = RemoveImageFileNode(xml, imageFile);
            return AddImageFileNode(xmlAfterRemove, imageFile);
        }

        public string UpdateLibraryNode(string xml, TLibrary library)
        {
            XmlDocument xmlDocument = LoadXmlDocument(xml);
            XmlDocument updatedlibraryXmlDocument = LoadXmlDocument(_xmlSerializer.SerializeToString(library));

            XmlNode libraryNode = SelectLibraryNode(xmlDocument) ?? throw new ArgumentException("Invalid xml document.", nameof(xml));
            XmlNode updatedLibraryNode = SelectLibraryNode(updatedlibraryXmlDocument) ?? throw new ArgumentException("Invalid xml document.", nameof(library));

            if (libraryNode.Attributes == null
                || updatedLibraryNode.Attributes == null
                || libraryNode.Attributes.Count != updatedLibraryNode.Attributes.Count)
            {
                return xml;
            }

            foreach (var attribute in libraryNode.Attributes.Cast<XmlAttribute>())
            {
                XmlAttribute updatedAttribute = updatedLibraryNode.Attributes[attribute.Name] ?? throw new InvalidOperationException();

                attribute.Value = updatedAttribute.Value;
            }

            return xmlDocument.OuterXml;
        }

        public string UpdateTagNode(string xml, Tag tag)
        {
            var xmlAfterRemove = RemoveTagNode(xml, tag);
            return AddTagNode(xmlAfterRemove, tag);
        }
        #endregion

        #region Private methods
        private XmlDocument LoadXmlDocument(string xml)
        {
            XmlDocument xmlDocument = new();
            xmlDocument.LoadXml(xml);

            return xmlDocument;
        }

        private XmlNode? SelectImageFilesNode(XmlDocument xmlDocument)
        {
            return xmlDocument.SelectSingleNode($"/{LibraryTypeName}/imageFiles");
        }

        private XmlNode CreateImageFilesNodeIfItDoesntExist(XmlDocument xmlDocument, XmlNode? imageFilesNode)
        {
            if (imageFilesNode == null)
            {
                XmlNode libraryNode = SelectLibraryNode(xmlDocument)
                    ?? throw new ArgumentException("Invalid xml document.", nameof(xmlDocument));

                imageFilesNode = xmlDocument.CreateElement("imageFiles");

                libraryNode.PrependChild(imageFilesNode);
            }

            return imageFilesNode;
        }

        private XmlNode? SelectLibraryNode(XmlDocument xmlDocument)
        {
            return xmlDocument.SelectSingleNode($"/{LibraryTypeName}");
        }

        private XmlNode AppendNodeToANode(XmlDocument xmlDocument, XmlNode baseNode, XmlNode nodeToAppend)
        {
            var importedNode = xmlDocument.ImportNode(nodeToAppend, true);
            baseNode.AppendChild(importedNode);

            return importedNode;
        }

        private XmlNode? SelectTagsNode(XmlDocument xmlDocument)
        {
            return xmlDocument.SelectSingleNode($"//tags");
        }

        private XmlNode CreateTagsNodeIfItDoesntExist(XmlDocument xmlDocument, XmlNode? tagsNode)
        {
            if (tagsNode == null)
            {
                XmlNode libraryNode = SelectLibraryNode(xmlDocument)
                    ?? throw new ArgumentException("Invalid xml document.", nameof(xmlDocument));

                tagsNode = xmlDocument.CreateElement("tags");

                libraryNode.PrependChild(tagsNode);
            }

            return tagsNode;
        }
        #endregion
    }
}
