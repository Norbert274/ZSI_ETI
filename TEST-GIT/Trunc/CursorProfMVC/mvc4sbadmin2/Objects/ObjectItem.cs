using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nclprospekt.Objects
{
    public class ObjectItem
    {
        private long id;
        private string description;
        private string stringId;

        public long ID
        {
            get { return id; }
        }

        public string StringId
        {
            get { return stringId; }
            set { this.stringId = value; }
        }

        public string Description
        {
            get { return description; }
        }

        public ObjectItem(long id, string description)
        {
            this.id = id;
            this.description = description;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return this.description;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is ObjectItem))
            {
                return false;
            }
            if ((obj as ObjectItem).id != this.id)
            {
                return false;
            }

            return true;
        }
    }
}