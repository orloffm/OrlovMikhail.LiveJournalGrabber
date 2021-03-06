﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace OrlovMikhail.LJ.Grabber.Entities
{
    [Serializable]
    [XmlRoot("comments")]
    public class Replies
    {
        public Replies()
        {
            Comments = new List<Comment>();
        }

        [XmlElement("comment")]
        public List<Comment> Comments { get; set; }

        public override string ToString()
        {
            if (Comments == null || Comments.Count == 0)
            {
                return "Empty list of comments.";
            }

            string ret = string.Format(
                "{0} comments, parent is {1}"
                , Comments.Count
                , Comments[0]
                    .ParentUrl
            );
            return ret;
        }
    }
}