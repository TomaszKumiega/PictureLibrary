﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Model
{
    public class Library : IExplorableElement
    {
        public string FullPath { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Tag> Tags { get; set; }
        public List<ImageFile> Images { get; set; }
        public List<Guid> Owners { get; set; }
        public Origin Origin { get; set; }
    }
}
