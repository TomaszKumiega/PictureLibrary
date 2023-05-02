using PictureLibrary.Tools.XamlSerializer;
using PictureLibraryModel.Model;
using System.Xml;

namespace PictureLibrary.Tools.LibraryXml
{
    public class LibraryXmlService : ILibraryXmlService
    {
        private readonly IXmlSerializer _xmlSerializer;

        public LibraryXmlService(
            IXmlSerializer xmlSerializer)
        {
            _xmlSerializer = xmlSerializer;   
        }

        public string AddImageFileNode<TImageFile>(string xml, TImageFile imageFile) 
            where TImageFile : ImageFile, new()
        {
            var xmlDocument = LoadXmlDocument(xml);

            var imageFilesNode = SelectImageFilesNode(xmlDocument);
            imageFilesNode = CreateImageFilesNodeIfItDoesntExist(xmlDocument, imageFilesNode);

            var serializedImageFile = _xmlSerializer.SerializeToString(imageFile);

            XmlDocument imageFileXmlDocument = LoadXmlDocument(serializedImageFile);
            XmlNode imageFileNode = imageFileXmlDocument.SelectSingleNode("imageFile") ?? throw new ArgumentException(nameof(imageFile));

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
            XmlNode tagNode = tagXmlDocument.SelectSingleNode("tag") ?? throw new ArgumentException(nameof(tag));

            _ = AppendNodeToANode(xmlDocument, tagsNode, tagNode);

            return xmlDocument.OuterXml;
        }

        public IEnumerable<TImageFile> GetImageFiles<TImageFile>(string xml)
            where TImageFile : ImageFile, new()
        {
            XmlDocument xmlDocument = LoadXmlDocument(xml);
            XmlNodeList? imageFileNodes = xmlDocument.SelectNodes("imageFile");

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
            XmlNodeList? tagNodes = xmlDocument.SelectNodes("tag");

            if (tagNodes == null)
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
            XmlNodeList? imageFileNodes = xmlDocument.SelectNodes("imageFile");

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
            XmlNodeList? tagNodes = xmlDocument.SelectNodes("tag");

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

        public string UpdateLibraryNode<TLibrary>(string xml, TLibrary library) 
            where TLibrary : Library, new()
        {
            XmlDocument xmlDocument = LoadXmlDocument(xml);
            XmlDocument updatedlibraryXmlDocument = LoadXmlDocument(_xmlSerializer.SerializeToString(library));

            XmlNode libraryNode = SelectLibraryNode(xmlDocument) ?? throw new ArgumentException(nameof(xml));
            XmlNode updatedLibraryNode = SelectLibraryNode(updatedlibraryXmlDocument) ?? throw new ArgumentException(nameof(library));

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

        #region Pomocnicze
        private XmlDocument LoadXmlDocument(string xml)
        {
            XmlDocument xmlDocument = new();
            xmlDocument.LoadXml(xml);

            return xmlDocument;
        }

        private XmlNode? SelectImageFilesNode(XmlDocument xmlDocument)
        {
            return xmlDocument.SelectSingleNode("/library/imageFiles");
        }

        private XmlNode CreateImageFilesNodeIfItDoesntExist(XmlDocument xmlDocument, XmlNode? imageFilesNode)
        {
            if (imageFilesNode == null)
            {
                XmlNode libraryNode = SelectLibraryNode(xmlDocument) ?? throw new ArgumentException();

                imageFilesNode = xmlDocument.CreateElement("imageFiles");

                libraryNode.PrependChild(imageFilesNode);
            }

            return imageFilesNode;
        }

        private XmlNode? SelectLibraryNode(XmlDocument xmlDocument)
        {
            return xmlDocument.SelectSingleNode("/library");
        }

        private XmlNode AppendNodeToANode(XmlDocument xmlDocument, XmlNode baseNode, XmlNode nodeToAppend)
        {
            var importedNode = xmlDocument.ImportNode(nodeToAppend, false);
            baseNode.AppendChild(importedNode);

            return importedNode;
        }

        private XmlNode? SelectTagsNode(XmlDocument xmlDocument)
        {
            return xmlDocument.SelectSingleNode("/library/tags");
        }

        private XmlNode CreateTagsNodeIfItDoesntExist(XmlDocument xmlDocument, XmlNode? tagsNode)
        {
            if (tagsNode == null)
            {
                XmlNode libraryNode = SelectLibraryNode(xmlDocument) ?? throw new ArgumentException();

                tagsNode = xmlDocument.CreateElement("tags");

                libraryNode.PrependChild(tagsNode);
            }

            return tagsNode;
        }
        #endregion
    }
}
